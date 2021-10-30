using SQLite;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace fridge_management.Services
{
    public class BaseService<T> where T : new()
    {
        static SQLiteAsyncConnection db;
        static async Task Init()
        {
            if (db != null)
                return;

            // Get an absolute path to the database file
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Fridge.db");

            db = new SQLiteAsyncConnection(databasePath);
            await db.CreateTableAsync<T>();
        }

        public static async Task Add(T data)
        {
            await Init();

            await db.InsertAsync(data);
        }

        public static async Task Delete(int id)
        {
            await Init();

            await db.DeleteAsync<T>(id);
        }

        public static async Task Edit(T data)
        {
            await Init();

            await db.UpdateAsync(data);
        }

        public static async Task<IEnumerable<T>> GetItems()
        {
            await Init();

            var item = await db.Table<T>().ToListAsync();
            return item;
        }

        public static async Task<T> Get(int id)
        {
            await Init();
            var item = await db.Table<T>().ElementAtAsync(id);
            return item;
        }
    }
}
