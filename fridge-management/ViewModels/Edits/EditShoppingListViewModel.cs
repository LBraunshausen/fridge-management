using fridge_management.Models;
using fridge_management.Services;
using MvvmHelpers;
using System;
using Xamarin.Forms;

namespace fridge_management.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    class EditShoppingListListViewModel : BindableObject
    {
        public string Title { get; set; }
        public Command EditCommand { get; }
        public ShoppingList curItem { get; set; }
        public ObservableRangeCollection<ShoppingList> Items { get; set; }
        public int ItemId { get; set; }
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


        public EditShoppingListListViewModel()
        {
            Title = "Inhalt bearbeiten";
            EditCommand = new Command(Edit);
            curItem = new ShoppingList();
        }

        public async void Edit()
        {
            await BaseService<ShoppingList>.Edit(curItem);
            MessagingCenter.Send<object, string>("MyApp", "Update", "List");
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
