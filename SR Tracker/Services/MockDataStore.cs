using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SR_Tracker.Models;

namespace SR_Tracker.Services
{
    public class MockDataStore : IDataStore<PlaySession>
    {
        readonly List<PlaySession> items;

        public MockDataStore()
        {
            items = new List<PlaySession>()
            {
                new PlaySession { Id = Guid.NewGuid().ToString(), Text = "First playSession", Description="This is an playSession description." },
                new PlaySession { Id = Guid.NewGuid().ToString(), Text = "Second playSession", Description="This is an playSession description." },
                new PlaySession { Id = Guid.NewGuid().ToString(), Text = "Third playSession", Description="This is an playSession description." },
                new PlaySession { Id = Guid.NewGuid().ToString(), Text = "Fourth playSession", Description="This is an playSession description." },
                new PlaySession { Id = Guid.NewGuid().ToString(), Text = "Fifth playSession", Description="This is an playSession description." },
                new PlaySession { Id = Guid.NewGuid().ToString(), Text = "Sixth playSession", Description="This is an playSession description." }
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

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((PlaySession arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<PlaySession> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<PlaySession>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}