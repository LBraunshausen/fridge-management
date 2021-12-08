using System.Collections.Generic;
using System.Threading.Tasks;

namespace fridge_management.Services
{
    /// <summary>
    /// Describes basic methods, which should be implemented in the BaseViewModel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
