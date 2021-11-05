using fridge_management.Models;
using fridge_management.Services;
using MvvmHelpers;
using SQLite;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace fridge_management.ViewModels
{
    [QueryProperty(nameof(FridgeItemId), nameof(FridgeItemId))]
    class EditFridgeItemViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public Command EditCommand { get; }
        private FridgeItem curItem;
        //public ObservableRangeCollection<FridgeItem> FridgeItems { get; set; }
        int fridgeItemId;
        public int FridgeItemId {
            get => fridgeItemId;
            set {
                SetProperty(ref fridgeItemId, value);
                GetItem();                
            }
        }
        public string Text
        {
            get => curItem.Text;
            set
            {
                if (value == curItem.Text)
                    return;
                curItem.Text = value;
                OnPropertyChanged();
            }
        }

        public DateTime ExpirationDate
        {
            get => curItem.ExpirationDate;
            set
            {
                if (value == curItem.ExpirationDate)
                    return;
                curItem.ExpirationDate = value;
                OnPropertyChanged();
            }
        }

        public int Amount
        {
            get => curItem.Amount;
            set
            {
                if (value == curItem.Amount)
                    return;
                curItem.Amount = value;
                OnPropertyChanged();
            }
        }

        public FridgeItem CurItem
        {
            get => curItem;
            set
            {
                if (value == curItem)
                    return;
                curItem = value;
                OnPropertyChanged();
            }
        }

        public EditFridgeItemViewModel()
        {
            Title = "Inhalt bearbeiten";
            EditCommand = new Command(Edit);
            curItem = new FridgeItem();
        }

        public async Task GetItem()
        {
            var fridgeItem = await BaseService<FridgeItem>.GetById(FridgeItemId);
            CurItem = fridgeItem;
        }

        public async void Edit()
        {
            await BaseService<FridgeItem>.Edit(curItem);
            MessagingCenter.Send<object, string>("MyApp", "Update", "List");
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
