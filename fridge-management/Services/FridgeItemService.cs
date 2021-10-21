using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using fridge_management.Models;

namespace fridge_management.Services
{
    public class FridgeItemService
    {
        static SQLiteAsyncConnection db;
        static async Task Init()
        {
            if (db != null)
                return;

            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Fridge.db");

            db = new SQLiteAsyncConnection(databasePath);            
            await db.CreateTableAsync<FridgeItem>();            
        }

        public static async Task AddFridgeItem(string text, DateTime expirationDate)
        {
            await Init();
            var item = new FridgeItem
            {                
                Text = text,
                ExpirationDate = expirationDate
            };

            await db.InsertAsync(item);
        }

        public static async Task DeleteFridgeItem(int id)
        {
            await Init();

            await db.DeleteAsync<FridgeItem>(id);
        }

        public static async Task<IEnumerable<FridgeItem>> GetFridgeItem()
        {
            await Init();

            var item = await db.Table<FridgeItem>().ToListAsync();
            return item;
        }
    }
}
