using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using fridge_management.ViewModels;
using fridge_management.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Collections;
using fridge_management.Services;
using Xamarin.Forms;
using fridge_management.Views;
using fridge_management;

namespace fridge_management_Test
{
    [TestClass]
    public class FridgeItemsViewModelTest
    {
        /// <summary>
        ///     Testmethod to test FridgeItemsViewModels Add method
        /// </summary>
        [TestMethod]
        public void TestAdd()
        {                        
            FridgeItemsViewModel vm = new FridgeItemsViewModel();            

            if (vm.OpenAddPageCommand.CanExecute(null))
                vm.OpenAddPageCommand.Execute(null);

            vm.SelectedItem = new FridgeItem()
            {                
                Text = "Testprodukt",
                Amount = 1,
                ExpirationDate = DateTime.Now
            };

            if (vm.AddCommand.CanExecute(null))
                vm.AddCommand.Execute(null);

            Task task = Task.Run(async () =>
            {
                await BaseService<FridgeItem>.Add(vm.SelectedItem);
                FridgeItem fridgeItem = null;

                fridgeItem = await BaseService<FridgeItem>.GetById(vm.SelectedItem.Id);

                Assert.AreEqual(vm.SelectedItem.Id, fridgeItem.Id, "not equal");
            });
            task.Wait();
        }
    }
}
