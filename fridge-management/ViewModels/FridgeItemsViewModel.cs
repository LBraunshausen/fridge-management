using fridge_management.Models;
using fridge_management.Services;
using fridge_management.Views;
using MvvmHelpers;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace fridge_management.ViewModels
{
    public class FridgeItemsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<FridgeItem> FridgeItem { get; set; }
        public Command AddCommand { get; }                

        public FridgeItemsViewModel()
        {
            Title = "Kühlschrankinhalt";
            AddCommand = new Command(Add);            
            FridgeItem = new ObservableRangeCollection<FridgeItem>();
            Load();

            MessagingCenter.Subscribe<object, string>("MyApp", "Update",
              (sender, arg) =>
                  {
                      Load();
                  }
              );
        }

        private async void Add()
        {
            await Shell.Current.GoToAsync(nameof(NewFridgeItemPage));
        }

        public async Task Load()
        {            
            IsBusy = true;
            FridgeItem.Clear();
            var fridgeItems = await FridgeItemService.GetFridgeItem();
            FridgeItem.AddRange(fridgeItems);
            IsBusy = false;
        }
    }
}