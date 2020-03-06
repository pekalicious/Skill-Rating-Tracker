using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pekalicious.SrTracker.Models;

namespace Pekalicious.SrTracker.Services
{
    public class MockDataStore : IDataStore<PlaySession>
    {
        readonly List<PlaySession> items;

        public MockDataStore()
        {
            items = new List<PlaySession>()
            {
                new PlaySession { Id = 0 },
                new PlaySession { Id = 1 },
                new PlaySession { Id = 2 },
                new PlaySession { Id = 3 },
                new PlaySession { Id = 4 },
                new PlaySession { Id = 5 }
            };
        }

        public async Task<bool> AddItemAsync(PlaySession playSession)
        {
            items.Add(playSession);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(PlaySession playSession)
        {
            var oldItem = items.Where((PlaySession arg) => arg.Id == playSession.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(playSession);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = items.Where((PlaySession arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<PlaySession> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<PlaySession>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}