using fridge_management.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace fridge_management.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}