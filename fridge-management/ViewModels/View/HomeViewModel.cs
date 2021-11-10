using System;
using System.Collections.Generic;
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
            Title = "Kühlschrankinhalt";
            FridgeItems = new ObservableRangeCollection<FridgeItem>();
            Load();
        }


        public async Task Load()
        {
            IsBusy = true;
            FridgeItems.Clear();
            var fridgeItems = await BaseService<FridgeItem>.GetItems();
            FridgeItems.AddRange(fridgeItems);
            IsBusy = false;
        }
    }
}
