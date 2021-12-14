using fridge_management.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace fridge_management.Services
{
    /// <summary>
    ///     The BaseService implements the methods for the sqlite connection. This service is a generic class, so it can be called to connect to every database table.
    /// </summary>
    /// <typeparam name="T">is the generic identifier. With "T" it is possible to call this service for every database table</typeparam>
    public class DBService<T> where T : IModel, new()
    {

        static SQLiteAsyncConnection db;
        
        /// <summary>
        ///     The Init method initializes the sqlite connection and creates a table if it not exists
        /// </summary>
        /// <returns></returns>
        static async Task Init()
        {
            if (db != null)
                return;

            // Get an absolute path to the database file            
            var databasePath = "";
            if (DeviceInfo.Platform != DevicePlatform.Unknown)
            {
                databasePath = Path.Combine(FileSystem.AppDataDirectory, "Fridge.db");
            }
            else
            {
                databasePath = Path.Combine("Fridge.db");
            }


            //var databasePath = Path.Combine(FileSystem.AppDataDirectory, "Fridge.db");

            // create database connection
            db = new SQLiteAsyncConnection(databasePath);
            // create table
            await db.CreateTableAsync<T>();            
        }


        /// <summary>
        ///     The DropTable method drops a table of type T
        /// </summary>
        /// <returns></returns>
        public static async Task DropTable()
        {
            await db.DropTableAsync<T>();
        }

        /// <summary>
        ///     The Add method adds new items to a database table
        /// </summary>
        /// <param name="data">contains the object which has to be added to the specific table</param>
        /// <returns></returns>
        public static async Task Add(T data)
        {
            // initialize the table
            await Init();

            // insert data
            await db.InsertAsync(data);
        }
        
        /// <summary>
        ///     The Delete method deletes a row from the database based on the given id.
        /// </summary>
        /// <param name="id">contains the id of one specific row which has to be deleted</param>
        /// <returns></returns>
        public static async Task Delete(Guid id)
        {
            // initialize the table
            await Init();

            await db.DeleteAsync<T>(id);
        }

        public static async Task DeleteAll()
        {
            await Init();

            await db.DeleteAllAsync<T>();
        }

        /// <summary>
        ///     The Edit method edits a specific object, which is passed.
        /// </summary>
        /// <param name="data">contains the object which has to be edited</param>
        /// <returns></returns>
        public static async Task Edit(T data)
        {
            // initialize the table
            await Init();

            // update the database item
            await db.UpdateAsync(data);
        }
        /// <summary>
        ///     The GetItems method gets all Items of a table
        /// </summary>
        /// <returns>A list of all Items in the database table</returns>

        public static async Task<IEnumerable<T>> GetItems()
        {
            // initialize the table
            await Init();

            // get all items in a list 
            var item = await db.Table<T>().ToListAsync();
            return item;
        }

        /// <summary>
        ///     The GetById method gets a specific item based on the passed id
        /// </summary>
        /// <param name="id">contains a specific id to find the specific table item</param>
        /// <returns>an specific item of the desired table</returns>
        public static async Task<T> GetById(Guid id)
        {
            // initialize the table
            Init();

            // query the table for specific item
            var item = await db.GetAsync<T>(id);
            return item;
        }
    }
}
