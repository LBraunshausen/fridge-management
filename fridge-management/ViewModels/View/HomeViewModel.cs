using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using fridge_management.Models;
using fridge_management.Services;
using fridge_management.Views;
using MvvmHelpers;
using Xamarin.Forms;

namespace fridge_management.ViewModels
{
    /// <summary>
    ///     ViewModel which manages the connection of FridgeItem and HomePage
    /// </summary>
    public class HomeViewModel : BaseViewModel
    {
        /// <summary>
        /// Contains a list of FridgeItems
        /// </summary>
        public ObservableRangeCollection<FridgeItem> FridgeItems { get; set; }        

        /// <summary>
        /// Instantiates FridgeItems and loads the data
        /// </summary>
        public HomeViewModel()
        {
            Title = "Startseite";
            FridgeItems = new ObservableRangeCollection<FridgeItem>();
            Load();
        }

        /// <summary>
        /// Loads the expired Fridgeitems and orders them by expiration date
        /// </summary>
        /// <returns></returns>
        public async Task Load()
        {
            IsBusy = true;
            FridgeItems.Clear();
            // get all FridgeItems
            var fridgeItems = await BaseService<FridgeItem>.GetItems();

            DateTime today = DateTime.Today;
            IEnumerable<FridgeItem> expiredItems = null;
            expiredItems = fridgeItems.Where<FridgeItem>(i => i.ExpirationDate - today <= new TimeSpan(2, 0, 0, 0)).OrderBy(i => i.ExpirationDate);
            FridgeItems.AddRange(expiredItems);
            IsBusy = false;
        }
    }
}
