using fridge_management.ViewModels;
using fridge_management.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace fridge_management
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(NewFridgeItemPage), typeof(NewFridgeItemPage));
            Routing.RegisterRoute(nameof(FridgeItemsPage), typeof(FridgeItemsPage));
            Routing.RegisterRoute(nameof(EditFridgeItemPage), typeof(EditFridgeItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
