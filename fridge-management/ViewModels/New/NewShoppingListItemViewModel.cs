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
    class NewShoppingListItemViewModel : BindableObject
    {
        public Command AddCommand { get; }
        public Item curItem { get; set; }
        public ObservableRangeCollection<Item> ShoppingListItems { get; set; }
        public string Title { get; set; }

        public NewShoppingListItemViewModel()
        {
            Title = "Inhalt hinzufügen";
            AddCommand = new Command(Add);
            curItem = new Item();
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
            await BaseService<Item>.Add(curItem);
            MessagingCenter.Send<object, string>("MyApp", "Update", "List");
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
