using fridge_management.Models;
using fridge_management.Services;
using fridge_management.Views;
using MvvmHelpers;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace fridge_management.ViewModels
{
    public class FridgeItemsViewModel : BaseViewModel
    {
        
        public Command OpenAddItemCommand { get; }
        public Command RemoveCommand { get; }
        public Command OpenEditItemCommand { get; }
        public Command AddCommand { get; }

        private ObservableRangeCollection<FridgeItem> fridgeItems;
        public ObservableRangeCollection<FridgeItem> FridgeItems
        {
            get => fridgeItems;
            set
            {
                if (value == fridgeItems)
                    return;
                fridgeItems = value;
                OnPropertyChanged();
            }
        }

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

        public string Text
        {
            get => selectedItem.Text;
            set
            {
                if (value == selectedItem.Text)
                    return;
                selectedItem.Text = value;
                OnPropertyChanged();
            }
        }

        public DateTime ExpirationDate
        {
            get => selectedItem.ExpirationDate;
            set
            {
                if (value == selectedItem.ExpirationDate)
                    return;
                selectedItem.ExpirationDate = value;
                OnPropertyChanged();
            }
        }

        public int Amount
        {
            get => selectedItem.Amount;
            set
            {
                if (value == selectedItem.Amount)
                    return;
                selectedItem.Amount = value;
                OnPropertyChanged();
            }
        }


        public FridgeItemsViewModel()
        {
            Title = "Kühlschrankinhalt";
            OpenAddItemCommand = new Command(OpenAddItem);
            RemoveCommand = new Command(Remove);
            OpenEditItemCommand = new Command(OpenEditItem);
            AddCommand = new Command(Add);
            FridgeItems = new ObservableRangeCollection<FridgeItem>();
            selectedItem = new FridgeItem();
            Load();
        }

        private async void OpenAddItem()
        {
            Title = "Inhalt hinzufügen";
            await Shell.Current.GoToAsync(nameof(NewFridgeItemPage));
            SelectedItem.Text = "";
            SelectedItem.ExpirationDate = DateTime.Now;
            SelectedItem.Amount = 1;
        }

        public async void Add()
        {
            await BaseService<FridgeItem>.Add(selectedItem);            
            await Application.Current.MainPage.Navigation.PopAsync();
            await Load();
        }

        private async void Remove()
        {
            await BaseService<FridgeItem>.Delete(selectedItem.Id);
            Load();
        }

        private async void OpenEditItem()
        {
            Title = "Inhalt bearbeiten";
            if (SelectedItem == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(EditFridgeItemPage)}?FridgeItemId={selectedItem.Id}");
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