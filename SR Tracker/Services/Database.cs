using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Pekalicious.SrTracker.Models;
using SQLite;

namespace Pekalicious.SrTracker
{
    public class Database
    {
        public class AppStateWrapper
        {
            private readonly Database _database;

            public AppStateWrapper(Database db)
            {
                _database = db;
            }

            public async Task<GameSeason> LastUsedSeason()
            {
                var stateValue = await _database.GetAppStateValue(Models.AppState.LAST_USED_SEASON);
                if (stateValue == null)
                    return null; //TODO: Create Maybe class and get rid of nulls

                return await _database.GetSeasonById(int.Parse(stateValue.Value));
            }

            public async Task SetLastUsedSeason(GameSeason season)
            {
                await _database.SetAppStateValue(Models.AppState.LAST_USED_SEASON, season.Id.ToString());
            }
        }

        public AppStateWrapper AppState { get; private set; }

        private readonly SQLiteAsyncConnection _database;

        public Database()
        {
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sr-tracker.db3");
            //bool initializeNewDatabase = !File.Exists(path);
            bool initializeNewDatabase = true;
            if (File.Exists(path))
                File.Delete(path);

            _database = new SQLiteAsyncConnection(path);
            if (initializeNewDatabase)
            {
                _database.CreateTableAsync<GameSeason>().Wait();
                _database.CreateTableAsync<AppState>().Wait();
                _database.CreateTableAsync<PlaySession>().Wait();
            }

            AppState = new AppStateWrapper(this);
        }

        public Task<List<GameSeason>> GetAllSeasons()
        {
            return _database.Table<GameSeason>().ToListAsync();
        }

        public Task<int> SaveSeasonAsync(GameSeason season)
        {
            return _database.InsertAsync(season);
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
    }
}
