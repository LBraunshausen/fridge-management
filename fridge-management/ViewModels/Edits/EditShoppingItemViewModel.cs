using fridge_management.Models;
using fridge_management.Services;
using MvvmHelpers;
using System;
using Xamarin.Forms;

namespace fridge_management.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    class EditShoppingListItemViewModel : BindableObject
    {
        public string Title { get; set; }
        public Command EditCommand { get; }
        public Item curItem { get; set; }
        public ObservableRangeCollection<Item> Items { get; set; }
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

        public EditShoppingListItemViewModel()
        {
            Title = "Inhalt bearbeiten";
            EditCommand = new Command(Edit);
            curItem = new Item();
        }

        public async void Edit()
        {
            await BaseService<Item>.Edit(curItem);
            MessagingCenter.Send<object, string>("MyApp", "Update", "List");
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}
