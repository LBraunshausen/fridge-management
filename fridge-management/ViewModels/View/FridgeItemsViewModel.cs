using fridge_management.Models;
using fridge_management.Services;
using fridge_management.Views;
using MvvmHelpers;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace fridge_management.ViewModels
{
    public class FridgeItemsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<FridgeItem> FridgeItems { get; set; }
        public Command AddCommand { get; }
        public Command RemoveCommand { get; }
        public Command EditCommand { get; }


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
            EditCommand = new Command(Edit);
            FridgeItems = new ObservableRangeCollection<FridgeItem>();
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
            await BaseService<FridgeItem>.Delete(selectedItem.Id);
            Load();
        }

        private async void Edit()
        {
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