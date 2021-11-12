using fridge_management.Models;
using fridge_management.Services;
using MvvmHelpers;
using System;
using Xamarin.Forms;

namespace fridge_management.ViewModels
{
    class NewShoppingListListViewModel : BindableObject
    {
        public Command AddCommand { get; }
        public ShoppingList curItem { get; set; }
        public ObservableRangeCollection<ShoppingList> ShoppingListItems { get; set; }
        public string Title { get; set; }

        public NewShoppingListListViewModel()
        {
            Title = "Neue Einkaufsliste erstellen";
            AddCommand = new Command(Add);
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

        public async void Add()
        {
            await BaseService<ShoppingList>.Add(curItem);
            MessagingCenter.Send<object, string>("MyApp", "Update", "List");
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
