using fridge_management.Services;
using System;
using Xamarin.Forms;

namespace fridge_management.Views
{
    [QueryProperty(nameof(FridgeItemId), nameof(FridgeItemId))]
    public partial class EditFridgeItemPage : ContentPage
    {
        public EditFridgeItemPage()
        {
            InitializeComponent();
        }

        public int FridgeItemId { get; set; }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = await FridgeItemService.GetFridgeItem(FridgeItemId);
            Console.WriteLine("test");
        }
    }
}