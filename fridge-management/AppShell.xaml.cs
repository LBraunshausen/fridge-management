using fridge_management.Views;
using System;
using Xamarin.Forms;

namespace fridge_management
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(NewFridgeItemPage), typeof(NewFridgeItemPage));
            Routing.RegisterRoute(nameof(BarcodeScannerPage), typeof(BarcodeScannerPage));
            Routing.RegisterRoute(nameof(FridgeItemsPage), typeof(FridgeItemsPage));
            Routing.RegisterRoute(nameof(EditFridgeItemPage), typeof(EditFridgeItemPage));
            Routing.RegisterRoute(nameof(ShoppingListPage), typeof(ShoppingListPage));
            Routing.RegisterRoute(nameof(ShoppingListItemPage), typeof(ShoppingListItemPage));
            Routing.RegisterRoute(nameof(NewShoppingListItemPage), typeof(NewShoppingListItemPage));
            Routing.RegisterRoute(nameof(NewShoppingListPage), typeof(NewShoppingListPage));
            Routing.RegisterRoute(nameof(EditShoppingListItemPage), typeof(EditShoppingListItemPage));
            Routing.RegisterRoute(nameof(EditShoppingListPage), typeof(EditShoppingListPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
