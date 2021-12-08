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
    public class HomeViewModel : BaseViewModel
    {
        public ObservableRangeCollection<FridgeItem> FridgeItems { get; set; }

        private FridgeItem selectedItem;
        public FridgeItem SelectedItem
        {
            get => selectedItem;
            set
            {
                if (value == selectedItem)
                    return;
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        public HomeViewModel()
        {
            Title = "Startseite";
            FridgeItems = new ObservableRangeCollection<FridgeItem>();
            Load();
        }


        public async Task Load()
        {
            IsBusy = true;
            FridgeItems.Clear();
            var fridgeItems = await BaseService<FridgeItem>.GetItems();

            DateTime today = DateTime.Today;
            IEnumerable<FridgeItem> expiredItems = null;

            expiredItems = fridgeItems.Where<FridgeItem>(i => i.ExpirationDate - today <= new TimeSpan(2, 0, 0, 0)).OrderBy(i => i.ExpirationDate);

            FridgeItems.AddRange(expiredItems);
            IsBusy = false;
        }
    }
}
