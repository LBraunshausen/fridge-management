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
    ///     ViewModel which manages the connection between FridgeItemsPage, NewFridgeItemPage, EditFridgeItemPage
    /// </summary>
    [QueryProperty(nameof(FridgeItemId), nameof(FridgeItemId))]            
    public class FridgeItemsViewModel : BaseViewModel
    {
        public ObservableRangeCollection<FridgeItem> FridgeItems { get; set; }
        public Command OpenAddPageCommand { get; }
        public Command AddCommand { get; }
        public Command RemoveCommand { get; }
        public Command OpenEditPageCommand { get; }
        public Command EditCommand { get; }

        Guid fridgeItemId;
        public Guid FridgeItemId
        {
            get => fridgeItemId;
            set
            {
                SetProperty(ref fridgeItemId, value);                
                GetItem();
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

        /// <summary>
        ///     The Constructor instantiates all commands, sets default values for the FridgeItem-properties and loads all FridgeItems
        /// </summary>
        public FridgeItemsViewModel()
        {
            Title = "Kühlschrankinhalt";
            OpenAddPageCommand = new Command(OpenAddPage);
            AddCommand = new Command(Add);
            RemoveCommand = new Command(Remove);
            OpenEditPageCommand = new Command(OpenEditPage);
            EditCommand = new Command(Edit);
            FridgeItems = new ObservableRangeCollection<FridgeItem>();
            selectedItem = new FridgeItem();
            ExpirationDate = DateTime.Now;
            Amount = 1;
            Load();

            MessagingCenter.Subscribe<object, string>("MyApp", "Update",
              (sender, arg) =>
                  {
                      Load();
                  }
              );
        }

        /// <summary>
        ///     The OpenAddPage method opens a NewFridgeItemPage.
        /// </summary>
        private async void OpenAddPage()
        {
            await Shell.Current.GoToAsync(nameof(NewFridgeItemPage));
        }

        /// <summary>
        ///     The Add method is called by a command from NewFridgeItemPage and adds a new FridgeItem.
        /// </summary>
        private async void Add()
        {
            await BaseService<FridgeItem>.Add(selectedItem);
            MessagingCenter.Send<object, string>("MyApp", "Update", "List");
            await Application.Current.MainPage.Navigation.PopAsync();
            
        }

        /// <summary>
        ///     The Remove method is called by FridgeItemsPage to delete a selected item.
        /// </summary>
        private async void Remove()
        {
            await BaseService<FridgeItem>.Delete(selectedItem.Id);
            Load();
        }

        /// <summary>
        ///     The OpenEditPage method opens a new EditFridgeItemPage if an item is selected.
        /// </summary>
        private async void OpenEditPage()
        {
            if (SelectedItem == null)
                return;           
            
            await Shell.Current.GoToAsync($"{nameof(EditFridgeItemPage)}?FridgeItemId={selectedItem.Id}");
        }

        private async void GetItem()
        {
            var fridgeItem = await BaseService<FridgeItem>.GetById(FridgeItemId);
            SelectedItem = fridgeItem;
        }

        /// <summary>
        ///     The Edit method edits an existing FridgeItem
        /// </summary>
        private async void Edit()
        {
            await BaseService<FridgeItem>.Edit(selectedItem);
            MessagingCenter.Send<object, string>("MyApp", "Update", "List");
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        /// <summary>
        ///     The Load method loads all FridgeItems and adds them to the view
        /// </summary>
        private async void Load()
        {
            IsBusy = true;
            FridgeItems.Clear();
            var fridgeItems = await BaseService<FridgeItem>.GetItems();
            FridgeItems.AddRange(fridgeItems);
            IsBusy = false;
        }
    }
}