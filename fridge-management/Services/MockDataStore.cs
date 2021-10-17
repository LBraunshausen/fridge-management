using fridge_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fridge_management.Services
{
    public class MockDataStore : IDataStore<FridgeItem>
    {
        readonly List<FridgeItem> items;

        public MockDataStore()
        {
            items = new List<FridgeItem>()
            {
                new FridgeItem { Id = Guid.NewGuid().ToString(), Text = "First item" },
                new FridgeItem { Id = Guid.NewGuid().ToString(), Text = "Second item"},
                new FridgeItem { Id = Guid.NewGuid().ToString(), Text = "Third item"},
                new FridgeItem { Id = Guid.NewGuid().ToString(), Text = "Fourth item"},
                new FridgeItem { Id = Guid.NewGuid().ToString(), Text = "Fifth item"},
                new FridgeItem { Id = Guid.NewGuid().ToString(), Text = "Sixth item"}
            };
        }

        public async Task<bool> AddItemAsync(FridgeItem item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(FridgeItem item)
        {
            var oldItem = items.Where((FridgeItem arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((FridgeItem arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<FridgeItem> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<FridgeItem>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}