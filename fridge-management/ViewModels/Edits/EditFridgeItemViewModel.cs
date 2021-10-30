using fridge_management.Models;
using fridge_management.Services;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace fridge_management.ViewModels
{
    [QueryProperty(nameof(FridgeItemId), nameof(FridgeItemId))]
    class EditFridgeItemViewModel : BindableObject
    {
        public string Title { get; set; }
        public Command EditCommand { get; }
        public Item FridgeItem { get; set; }
        public ObservableRangeCollection<Item> FridgeItems { get; set; }
        public int FridgeItemId { get; set; }
        public string Text
        {
            get => FridgeItem.Text;
            set
            {
                if (value == FridgeItem.Text)
                    return;
                FridgeItem.Text = value;
                OnPropertyChanged();
            }
        }

        public DateTime ExpirationDate
        {
            get => FridgeItem.ExpirationDate;
            set
            {
                if (value == FridgeItem.ExpirationDate)
                    return;
                FridgeItem.ExpirationDate = value;
                OnPropertyChanged();
            }
        }

        public int Amount
        {
            get => FridgeItem.Amount;
            set
            {
                if (value == FridgeItem.Amount)
                    return;
                FridgeItem.Amount = value;
                OnPropertyChanged();
            }
        }

        public EditFridgeItemViewModel()
        {
            Title = "Inhalt bearbeiten";
            EditCommand = new Command(Edit);
            FridgeItem = new Item();
        }

        public async void Edit()
        {
            await FridgeItemService.EditFridgeItem((string)FridgeItem.Text, (DateTime)FridgeItem.ExpirationDate, (int)FridgeItem.Amount);
            MessagingCenter.Send<object, string>("MyApp", "Update", "List");
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
