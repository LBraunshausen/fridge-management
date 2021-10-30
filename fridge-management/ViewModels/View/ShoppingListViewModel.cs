using fridge_management.Models;
using fridge_management.Services;
using fridge_management.Views;
using MvvmHelpers;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace fridge_management.ViewModels
{
    public class ShoppingListViewModel : BaseViewModel
    {
        public ObservableRangeCollection<Item> Items { get; set; }
        public Command AddCommand { get; }
        public Command RemoveCommand { get; }
        public Command EditCommand { get; }


        private Item selectedItem;
        public Item SelectedItem
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
            Title = "Einkaufsliste";
            AddCommand = new Command(Add);
            RemoveCommand = new Command(Remove);
            EditCommand = new Command(Edit);
            Items = new ObservableRangeCollection<Item>();
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
            await BaseService<Item>.Delete(selectedItem.Id);
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
            var items = await BaseService<Item>.GetItems();
            Items.AddRange(items);
            IsBusy = false;
        }
    }
}