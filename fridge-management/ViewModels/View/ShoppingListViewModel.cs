using fridge_management.Models;
using fridge_management.Services;
using fridge_management.Views;
using MvvmHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace fridge_management.ViewModels
{
    [QueryProperty(nameof(ShoppingListId), nameof(ShoppingListId))]
    public class ShoppingListViewModel : BaseViewModel
    {
        public ObservableRangeCollection<ShoppingListItem> Items { get; set; }
        public Command AddCommand { get; }
        public Command RemoveCommand { get; }
        public Command EditCommand { get; }

        private int shoppingListId;
        public int ShoppingListId
        {
            get => shoppingListId;
            set
            {
                if (value == shoppingListId)
                    return;
                shoppingListId = value;
                OnPropertyChanged();
            }
        }


        private ShoppingListItem selectedItem;
        public ShoppingListItem SelectedItem
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

        public ShoppingListViewModel()
        {
            Title = "Einkaufslistenitems";
            AddCommand = new Command(Add);
            RemoveCommand = new Command(Remove);
            EditCommand = new Command(Edit);
            Items = new ObservableRangeCollection<ShoppingListItem>();
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
            await Shell.Current.GoToAsync(nameof(NewShoppingListItemPage));
        }

        private async void Remove()
        {
            await BaseService<ShoppingListItem>.Delete(selectedItem.Id);
            Load();
        }

        private async void Edit()
        {
            if (SelectedItem == null)
                return;
            //await Shell.Current.GoToAsync($"{nameof(EditShoppingListItemPage)}?ItemId={selectedItem.Id}");
        }

        public async Task Load()
        {
            IsBusy = true;
            Items.Clear();
            IEnumerable<ShoppingListItem> selectedItems = null;   
            var items = await BaseService<ShoppingListItem>.GetItems();
            selectedItems = items.Where(i => i.ShoppingListId == ShoppingListId);
            Items.AddRange(selectedItems);
            IsBusy = false;
        }
    }
}