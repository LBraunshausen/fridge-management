using fridge_management.Models;
using fridge_management.Services;
using MvvmHelpers;
using System;
using Xamarin.Forms;

namespace fridge_management.ViewModels
{
    class NewFridgeItemViewModel : BindableObject
    {
        public Command AddCommand { get; }
        public FridgeItem curItem { get; set; }
        public ObservableRangeCollection<FridgeItem> FridgeItems { get; set; }
        public string Title { get; set; }

        public NewFridgeItemViewModel()
        {
            Title = "Inhalt hinzufügen";
            AddCommand = new Command(Add);
            curItem = new FridgeItem();
            ExpirationDate = DateTime.Now;
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

        public async void Add()
        {
            await BaseService<FridgeItem>.Add(curItem);
            MessagingCenter.Send<object, string>("MyApp", "Update", "List");
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
