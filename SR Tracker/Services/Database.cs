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

            public UserData(Database db)
            {
                _database = db;
            }

            public async Task<Maybe<GameSeason>> LastUsedSeason()
            {
                var stateValue = await _database.GetAppStateValue(Models.AppState.LAST_USED_SEASON);
                if (stateValue == null)
                {
                    return new Maybe<GameSeason>();
                }

                GameSeason season = await _database.GetSeasonById(int.Parse(stateValue.Value));
                return new Maybe<GameSeason>(season);
            }

            public async Task SetLastUsedSeason(GameSeason season)
            {
                await _database.SetAppStateValue(Models.AppState.LAST_USED_SEASON, season.Id.ToString());
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
                _database.CreateTableAsync<AppState>().Wait();
                _database.CreateTableAsync<PlaySession>().Wait();
            }

            User = new UserData(this);
        }

        private static bool DEBUG_FORCE_CREATE_DB = true;
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
            return _database.Table<GameSeason>().ToListAsync();
        }

        public Task<GameSeason> GetSeasonById(int id)
        {
            return _database.GetAsync<GameSeason>(id);
        }

        public Task<AppState> GetAppStateValue(string key)
        {
            return _database.FindAsync<AppState>(key);
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
                await _database.InsertAsync(new AppState() {Key = key, Value = value});
            }
        }

        public Task<int> AddGameSeason(GameSeason newSeason)
        {
            return _database.InsertAsync(newSeason);
        }

        public Task<int> UpdateGameSeason(GameSeason editingGameSeason)
        {
            return _database.UpdateAsync(editingGameSeason);
        }

        public Task<int> AddPlaySession(PlaySession playSession)
        {
            return _database.InsertAsync(playSession);
        }
    }
}
