using fridge_management.Models;
using fridge_management.Services;
using fridge_management.Views;
using MvvmHelpers;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace fridge_management.ViewModels
{
    /// <summary>
    ///     ViewModel which manages the connection between ShoppingListPage, NewShoppingListPage, EditShoppingListPage
    /// </summary>
    [QueryProperty(nameof(ShoppingListId), nameof(ShoppingListId))]
    public class ShoppingListListViewModel : BaseViewModel
    {
        public ObservableRangeCollection<ShoppingList> Items { get; set; }
        public Command OpenAddPageCommand { get; }
        public Command AddCommand { get; }
        public Command RemoveCommand { get; }
        public Command OpenEditPageCommand { get; }
        public Command EditCommand { get; }

        int shoppingListId;
        public int ShoppingListId
        {
            get => shoppingListId;
            set
            {
                SetProperty(ref shoppingListId, value);
                GetItem();
            }
        }


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


        public ShoppingListListViewModel()
        {
            Title = "Einkaufslistenitems";
            OpenAddPageCommand = new Command(OpenAddPage);
            AddCommand = new Command(Add);
            RemoveCommand = new Command(Remove);
            OpenEditPageCommand = new Command(OpenEditPage);
            EditCommand = new Command(Edit);
            Items = new ObservableRangeCollection<ShoppingList>();
            selectedItem = new ShoppingList();
            Load();

            MessagingCenter.Subscribe<object, string>("MyApp", "Update",
              (sender, arg) =>
              {
                  Load();
              }
              );
        }

        /// <summary>
        ///     The OpenAddPage method opens a NewShoppingListItemPage.
        /// </summary>
        private async void OpenAddPage()
        {
            await Shell.Current.GoToAsync(nameof(NewShoppingListItemPage));
        }

        /// <summary>
        ///     The Add method is called by a command from NewShoppingListItemPage and adds a new ShoppingListItem.
        /// </summary>
        private async void Add()
        {
            selectedItem.Id = new Guid();
            await BaseService<ShoppingList>.Add(selectedItem);
            MessagingCenter.Send<object, string>("MyApp", "Update", "List");
            await Application.Current.MainPage.Navigation.PopAsync();

        }

        /// <summary>
        ///     The Remove method is called by ShoppingListPage to delete a selected item.
        /// </summary>
        private async void Remove()
        {
            await BaseService<ShoppingList>.Delete(selectedItem.Id);
            Load();
        }

        /// <summary>
        ///     The OpenEditPage method opens a new EditShoppingListPage if an item is selected.
        /// </summary>
        private async void OpenEditPage()
        {
            if (SelectedItem == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(EditShoppingListPage)}?FridgeItemId={selectedItem.Id}");
        }

        /// <summary>
        ///     Returns the record for the passed Id
        /// </summary>
        private async void GetItem()
        {
            var item = await BaseService<ShoppingList>.GetById(ShoppingListId);

            SelectedItem = item;
        }

        /// <summary>
        ///     The Edit method edits an existing ShoppingListItem
        /// </summary>
        private async void Edit()
        {
            await BaseService<ShoppingList>.Edit(selectedItem);
            MessagingCenter.Send<object, string>("MyApp", "Update", "List");
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        /// <summary>
        ///     The Load method loads all ShoppingListItems and adds them to the view
        /// </summary>
        public async Task Load()
        {
            IsBusy = true;
            Items.Clear();
            var items = await BaseService<ShoppingList>.GetItems();
            Items.AddRange(items);
            IsBusy = false;
        }


        /*public ShoppingListListViewModel()
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
            var c = await BaseService<ShoppingList>.DropTable();

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
        }*/
    }
}