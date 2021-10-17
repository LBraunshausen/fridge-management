using fridge_management.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace fridge_management.ViewModels
{
    public class FridgeItemsViewModel : BaseViewModel
    {
        public ObservableCollection<FridgeItem> FridgeItem { get; set; }
        public Command AddCommand { get; }
        public FridgeItemsViewModel()
        {
            Title = "Kühlschrankinhalt";

            AddCommand = new Command(Add);
        }

        private async void Add()
        {
            
        }
    }
}