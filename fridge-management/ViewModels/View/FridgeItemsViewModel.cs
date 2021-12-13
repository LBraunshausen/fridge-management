using fridge_management.Models;
using fridge_management.Services;
using fridge_management.Views;
using MvvmHelpers;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing;

namespace fridge_management.ViewModels
{
    /// <summary>
    ///     ViewModel which manages the connection between FridgeItemsPage, NewFridgeItemPage, EditFridgeItemPage
    /// </summary>
    [QueryProperty(nameof(FridgeItemId), nameof(FridgeItemId))]            
    public class FridgeItemsViewModel : BaseViewModel
    {
        /// <summary>
        /// Contains a list of FridgeItems which are listed in the listview
        /// </summary>
        public ObservableRangeCollection<FridgeItem> FridgeItems { get; set; }
        /// <summary>
        /// Command which manages the opening of an AddPage
        /// </summary>
        public Command OpenAddPageCommand { get; }
        /// <summary>
        /// Command which manages the opening of a ScannerPage
        /// </summary>
        public Command OpenScannerPageCommand { get; }

        /// <summary>
        /// Contains the code which is returned by the barcode scanner
        /// </summary>
        private Result code;
        public Result Code 
        {
            get => code;
            set
            {
                if (value == code)
                    return;
                code = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// This property is used to activate/inactivate the scanning of the barcode scanner
        /// </summary>        
        private bool isScanning;
        public bool IsScanning
        {
            get => isScanning;
            set
            {
                if (value == isScanning)
                    return;
                isScanning = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Command which manages the scanning an retrieving of the ean code
        /// </summary>
        public Command ScanResultCommand
        {
            get
            {
                return new Command(() =>
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        // deactivate scanning
                        IsScanning = false;
                        // retrieve product info from ean code
                        var result = HttpRequestService.getItemByEan(Code.Text);
                        Text = result["products"][0]["title"].ToString();
                        await Application.Current.MainPage.Navigation.PopAsync();
                        IsScanning = true;
                    });
                });
            }
        }

        /// <summary>
        /// Command which manages the adding of a FridgeItem
        /// </summary>
        public Command AddCommand { get; }
        /// <summary>
        /// Command which manages the removing of a FridgeItem
        /// </summary>
        public Command RemoveCommand { get; }
        /// <summary>
        /// Command which manages the opening of a EditPage
        /// </summary>
        public Command OpenEditPageCommand { get; }
        /// <summary>
        /// Command which manages the editing of a FridgeItem
        /// </summary>
        public Command EditCommand { get; }

        /// <summary>
        /// Contains the FridgeItemId which is passed by Queryproperties
        /// </summary>
        string fridgeItemId;
        public string FridgeItemId
        {
            get => fridgeItemId;
            set
            {
                SetProperty(ref fridgeItemId, value);
                GetItem();
            }
        }

        /// <summary>
        /// Contains the current selected FridgeItem
        /// </summary>
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
        /// The Constructor instantiates all commands, sets default values for the FridgeItem-properties and loads all FridgeItems
        /// </summary>
        public FridgeItemsViewModel()
        {
            Title = "Kühlschrankinhalt";
            // instantiate commands
            OpenAddPageCommand = new Command(OpenAddPage);
            OpenScannerPageCommand = new Command(OpenScannerPage);
            AddCommand = new Command(Add);            
            RemoveCommand = new Command(Remove);
            OpenEditPageCommand = new Command(OpenEditPage);
            EditCommand = new Command(Edit);

            FridgeItems = new ObservableRangeCollection<FridgeItem>();
            selectedItem = new FridgeItem();
            
            // set default values
            ExpirationDate = DateTime.Now;
            Amount = 1;
            isScanning = true;

            Load();                        

            MessagingCenter.Subscribe<object, string>("MyApp", "Update",
              (sender, arg) =>
                  {
                      Load();
                  }
              );
        }

        /// <summary>
        /// The OpenAddPage method opens a NewFridgeItemPage.
        /// </summary>
        private async void OpenAddPage()
        {
            Title = "Produkt hinzufügen";
            await Shell.Current.GoToAsync(nameof(NewFridgeItemPage));            
        }

        /// <summary>
        /// The OpenScannerPage method opens a BarcodeScannerPage
        /// </summary>
        private async void OpenScannerPage()
        {
            Title = "Code einscannen";
            await Shell.Current.GoToAsync(nameof(BarcodeScannerPage));            
        }

        /// <summary>
        /// The Add method is called by a command from NewFridgeItemPage and adds a new FridgeItem.
        /// </summary>
        private async void Add()
        {
            // generate new guid
            selectedItem.Id = new Guid();
            // add item to database
            await DBService<FridgeItem>.Add(selectedItem);
            // update listview
            MessagingCenter.Send<object, string>("MyApp", "Update", "List");
            await Application.Current.MainPage.Navigation.PopAsync();
            
        }
        
        /// <summary>
        /// The Remove method is called by FridgeItemsPage to delete a selected item.
        /// </summary>
        private async void Remove()
        {
            await DBService<FridgeItem>.Delete(selectedItem.Id);
            Load();
        }

        /// <summary>
        /// The OpenEditPage method opens a new EditFridgeItemPage if an item is selected.
        /// </summary>
        private async void OpenEditPage()
        {
            if (SelectedItem == null)
                return;
            
            await Shell.Current.GoToAsync($"{nameof(EditFridgeItemPage)}?FridgeItemId={selectedItem.Id}");
        }

        /// <summary>
        /// Method which gets a single Item by a given ID
        /// </summary>
        private async void GetItem()
        {
            var fridgeItem = await DBService<FridgeItem>.GetById(Guid.Parse(FridgeItemId));

            SelectedItem = fridgeItem;            
        }

        /// <summary>
        /// The Edit method edits an existing FridgeItem
        /// </summary>
        private async void Edit()
        {
            await DBService<FridgeItem>.Edit(selectedItem);
            MessagingCenter.Send<object, string>("MyApp", "Update", "List");
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        /// <summary>
        /// The Load method loads all FridgeItems and adds them to the view
        /// </summary>
        private async void Load()
        {
            IsBusy = true;
            // clear listview
            FridgeItems.Clear();
            // get list of all FridgeItems
            var fridgeItems = await DBService<FridgeItem>.GetItems();
            FridgeItems.AddRange(fridgeItems);
            IsBusy = false;
        }
    }
}