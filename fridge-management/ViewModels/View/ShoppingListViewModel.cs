﻿using fridge_management.Models;
using fridge_management.Services;
using fridge_management.Views;
using MvvmHelpers;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace fridge_management.ViewModels
{    
     /// <summary>
     ///     ViewModel which manages the connection between ShoppingListPage, NewShoppingListPage, EditShoppingListPage
     /// </summary>
    [QueryProperty(nameof(ShoppingListItemId), nameof(ShoppingListItemId))]
    public class ShoppingListViewModel : BaseViewModel
    {
        public ObservableRangeCollection<ShoppingListItem> Items { get; set; }
        public Command OpenAddPageCommand { get; }
        public Command AddCommand { get; }
        public Command RemoveCommand { get; }
        public Command OpenEditPageCommand { get; }
        public Command EditCommand { get; }


        private int shoppingListId;
        public int ShoppingListId
        {
            get => shoppingListId;
            set
            {
                if (value == shoppingListId)
                    return;
                shoppingListId = value;
                OnPropertyChanged();
            }
        }
        
        Guid shoppingListItemId;
        public Guid ShoppingListItemId
        {
            get => shoppingListItemId;
            set
            {
                SetProperty(ref shoppingListItemId, value);
                GetItem();
            }
        }


        private ShoppingListItem selectedItem;
        public ShoppingListItem SelectedItem
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

        public ShoppingListViewModel()
        {
            Title = "Einkaufslistenitems";
            OpenAddPageCommand = new Command(OpenAddPage);
            AddCommand = new Command(Add);
            RemoveCommand = new Command(Remove);
            OpenEditPageCommand = new Command(OpenEditPage);
            EditCommand = new Command(Edit);
            Items = new ObservableRangeCollection<ShoppingListItem>();
            selectedItem = new ShoppingListItem();
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
        ///     The OpenAddPage method opens a NewShoppingListItemPage.
        /// </summary>
        private async void OpenAddPage()
        {
            await Shell.Current.GoToAsync(nameof(NewShoppingListItemPage));
        }

        /// <summary>
        ///     The Add method is called by a command from NewShoppingListItemPage and adds a new ShoppingListItem.
        /// </summary>
        private async void Add()
        {
            selectedItem.Id = new Guid();
            await BaseService<ShoppingListItem>.Add(selectedItem);
            MessagingCenter.Send<object, string>("MyApp", "Update", "List");
            await Application.Current.MainPage.Navigation.PopAsync();

        }

        /// <summary>
        ///     The Remove method is called by ShoppingListPage to delete a selected item.
        /// </summary>
        private async void Remove()
        {
            await BaseService<ShoppingListItem>.Delete(selectedItem.Id);
            Load();
        }

        /// <summary>
        ///     The OpenEditPage method opens a new EditShoppingListPage if an item is selected.
        /// </summary>
        private async void OpenEditPage()
        {
            if (SelectedItem == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(EditShoppingListPage)}?FridgeItemId={selectedItem.Id}");
        }

        /// <summary>
        ///     Returns the record for the passed Id
        /// </summary>
        private async void GetItem()
        {
            var item = await BaseService<ShoppingListItem>.GetById(ShoppingListItemId);

            SelectedItem = item;
        }

        /// <summary>
        ///     The Edit method edits an existing ShoppingListItem
        /// </summary>
        private async void Edit()
        {
            await BaseService<ShoppingListItem>.Edit(selectedItem);
            MessagingCenter.Send<object, string>("MyApp", "Update", "List");
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        /// <summary>
        ///     The Load method loads all ShoppingListItems and adds them to the view
        /// </summary>
        public async Task Load()
        {
            IsBusy = true;
            Items.Clear();
            IEnumerable<ShoppingListItem> selectedItems = null;   
            var items = await BaseService<ShoppingListItem>.GetItems();
            selectedItems = items.Where(i => i.ShoppingListId == ShoppingListId);
            Items.AddRange(selectedItems);
            IsBusy = false;
        }
    }
}