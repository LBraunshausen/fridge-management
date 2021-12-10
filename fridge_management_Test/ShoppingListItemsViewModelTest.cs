using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using fridge_management.ViewModels;
using fridge_management.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Collections;


namespace fridge_management_Test
{
    [TestClass]
    public class ShoppingListItemsViewModelTest
    {
        [TestMethod]
        public void TestAdd()
        {
            ShoppingListItemViewModel vm = new ShoppingListItemViewModel();

            if (vm.AddCommand.CanExecute(null))
                vm.AddCommand.Execute(null);
            
        }
    }
}
