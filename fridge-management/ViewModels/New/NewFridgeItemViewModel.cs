﻿using fridge_management.Models;
using fridge_management.Services;
using MvvmHelpers;
using System;
using Xamarin.Forms;

namespace fridge_management.ViewModels
{
    class NewFridgeItemViewModel : BindableObject
    {
        public Command AddCommand { get; }
        public Item FridgeItem { get; set; }
        public ObservableRangeCollection<FridgeItem> FridgeItems { get; set; }
        public string Title { get; set; }

        public NewFridgeItemViewModel()
        {
            Title = "Inhalt hinzufügen";
            AddCommand = new Command(Add);
            FridgeItem = new Item();
            ExpirationDate = DateTime.Now;
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

        public async void Add()
        {
            await FridgeItemService.AddFridgeItem((string)FridgeItem.Text, (DateTime)FridgeItem.ExpirationDate, (int)FridgeItem.Amount);
            MessagingCenter.Send<object, string>("MyApp", "Update", "List");
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}