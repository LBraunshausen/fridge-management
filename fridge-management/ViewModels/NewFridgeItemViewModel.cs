using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using fridge_management.Services;
using fridge_management.Models;
using System.ComponentModel;
using MvvmHelpers;
using System.Threading.Tasks;

namespace fridge_management.ViewModels
{
    class NewFridgeItemViewModel : BindableObject
    {
        public Command AddCommand { get; }
        public FridgeItem FridgeItem { get; set; }
        public ObservableRangeCollection<FridgeItem> FridgeItems { get; set; }
        public string Title { get; set; }

        public NewFridgeItemViewModel()
        {
            Title = "Inhalt hinzufügen";
            AddCommand = new Command(Add);
            FridgeItem = new FridgeItem();
        }
        
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

        public async void Add()
        {
            await FridgeItemService.AddFridgeItem(FridgeItem.Text, FridgeItem.ExpirationDate);
            MessagingCenter.Send<object, string>("MyApp", "Update", "List");
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
