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
        public Command RemoveCommand { get; }


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

        public FridgeItemsViewModel()
        {
            Title = "Kühlschrankinhalt";
            AddCommand = new Command(Add);
            RemoveCommand = new Command(Remove);
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

        private async void Remove()
        {
            await FridgeItemService.DeleteFridgeItem(selectedItem.Id);
            Load();
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