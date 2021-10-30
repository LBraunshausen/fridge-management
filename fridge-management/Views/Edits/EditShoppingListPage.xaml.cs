using fridge_management.Models;
using fridge_management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace fridge_management.Views.Edits
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class EditShoppingListItemPage : ContentView
    {
        public EditShoppingListItemPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            BindingContext = await BaseService<Item>.Get(ItemId);
            Console.WriteLine("test");
        }
    }
}