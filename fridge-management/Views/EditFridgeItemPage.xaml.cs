using fridge_management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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