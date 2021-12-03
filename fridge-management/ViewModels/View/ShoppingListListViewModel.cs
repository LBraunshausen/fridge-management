using fridge_management.Models;
using fridge_management.Services;
using fridge_management.Views;
using MvvmHelpers;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace fridge_management.ViewModels
{
    public class ShoppingListListViewModel : BaseViewModel
    {
        public ObservableRangeCollection<ShoppingList> Items { get; set; }
        public Command AddCommand { get; }
        public Command DoubleClickCommand { get; }
        public Command RemoveCommand { get; }
        public Command EditCommand { get; }


        private ShoppingList selectedItem;
        public ShoppingList SelectedItem
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

        public int ShoppingListId { get; set; }

        public ShoppingListListViewModel()
        {
            Title = "Einkaufsliste";
            AddCommand = new Command(Add);
            DoubleClickCommand = new Command(DoubleClick);
            RemoveCommand = new Command(Remove);
            EditCommand = new Command(Edit);
            Items = new ObservableRangeCollection<ShoppingList>();
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
            await Shell.Current.GoToAsync(nameof(NewShoppingListListPage));
        }

        private async void DoubleClick()
        {
            //await Shell.Current.GoToAsync(nameof(ShoppingListPage));
            await Shell.Current.GoToAsync($"{nameof(ShoppingListPage)}?ShoppingListId={selectedItem.ShoppingListId}");
        }

        private async void Remove()
        {
            await BaseService<ShoppingList>.Delete(selectedItem.Id);
            Load();
        }

        private async void Edit()
        {
            if (SelectedItem == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(EditShoppingListListViewModel)}?ItemId={selectedItem.Id}");
        }

        public async Task Load()
        {
            IsBusy = true;
            Items.Clear();
            var items = await BaseService<ShoppingList>.GetItems();
            Items.AddRange(items);
            IsBusy = false;
        }
    }
}