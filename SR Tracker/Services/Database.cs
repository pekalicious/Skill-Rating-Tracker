using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Pekalicious.SrTracker.Core;
using Pekalicious.SrTracker.Models;
using SQLite;
using SQLiteNetExtensionsAsync.Extensions;

namespace Pekalicious.SrTracker
{
    public class Database
    {
        public class UserData
        {
            private readonly Database _database;

            private GameSeason _currentGameSeason;

            public UserData(Database db)
            {
                _database = db;
            }

            public async Task<Maybe<GameSeason>> LastUsedSeason()
            {
                if (_currentGameSeason == null)
                {
                    var stateValue = await _database.GetAppStateValue(UserDataTable.LAST_USED_SEASON);
                    if (stateValue != null)
                    {
                        _currentGameSeason = await _database.GetSeasonById(int.Parse(stateValue.Value));
                    }
                }

                if (_currentGameSeason == null) return new Maybe<GameSeason>();

                return new Maybe<GameSeason>(_currentGameSeason);
            }

            public async Task SetLastUsedSeason(GameSeason season)
            {
                _currentGameSeason = season;
                await _database.SetAppStateValue(UserDataTable.LAST_USED_SEASON, season.Id.ToString());
            }
        }

        public UserData User { get; }

        private readonly SQLiteAsyncConnection _database;

        public Database()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sr-tracker.db3");
            bool initializeNewDatabase = ShouldInitializeDatabase(path);

            _database = new SQLiteAsyncConnection(path);
            if (initializeNewDatabase)
            {
                _database.CreateTableAsync<GameSeason>().Wait();
                _database.CreateTableAsync<UserDataTable>().Wait();
                _database.CreateTableAsync<PlaySession>().Wait();
            }

            User = new UserData(this);
        }

        private static bool DEBUG_FORCE_CREATE_DB = false;
        private bool ShouldInitializeDatabase(string path)
        {
            if (DEBUG_FORCE_CREATE_DB)
            {
                if (File.Exists(path))
                    File.Delete(path);
                return true;
            }
            return !File.Exists(path);
        }

        public Task<List<GameSeason>> GetAllSeasons()
        {
            return _database.GetAllWithChildrenAsync<GameSeason>();
        }

        public Task<GameSeason> GetSeasonById(int id)
        {
            return _database.GetWithChildrenAsync<GameSeason>(id);
        }

        public Task<Maybe<PlaySession>> GetSessionById(int id)
        {
            Task<PlaySession> session = _database.GetWithChildrenAsync<PlaySession>(id);
            return Task.FromResult(new Maybe<PlaySession>(session.Result));
        }

        public Task<UserDataTable> GetAppStateValue(string key)
        {
            return _database.FindWithChildrenAsync<UserDataTable>(key);
        }

        public async Task SetAppStateValue(string key, string value)
        {
            var existingState = await GetAppStateValue(key);
            if (existingState != null)
            {
                existingState.Value = value;
            }
            else
            {
                await _database.InsertWithChildrenAsync(new UserDataTable() {Key = key, Value = value});
            }
        }

        public Task AddGameSeason(GameSeason newSeason)
        {
            return _database.InsertWithChildrenAsync(newSeason);
        }

        public Task UpdateGameSeason(GameSeason editingGameSeason)
        {
            return _database.UpdateWithChildrenAsync(editingGameSeason);
        }

        public Task AddPlaySession(GameSeason season, PlaySession playSession)
        {
            season.SessionHistory.Add(playSession);
            _database.InsertWithChildrenAsync(playSession);
            return UpdateGameSeason(season);
        }
    }
}
