using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using fridge_management.Services;
using fridge_management.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Collections;

namespace fridge_management_Test
{
    [TestClass]
    public class BaseServiceTest
    {
        [TestMethod]
        public void TestAdd()
        {
            FridgeItem addItem = new FridgeItem()
            {
                Id = new Guid(),
                Text = "Testprodukt",
                Amount = 1,
                ExpirationDate = DateTime.Now
            };
            
            Task task = Task.Run(async () =>
            {
                await BaseService<FridgeItem>.Add(addItem);
                FridgeItem fridgeItem = null;

                foreach (FridgeItem item in await BaseService<FridgeItem>.GetById(addItem.Id))
                {
                    fridgeItem = item;
                } 
                Assert.AreEqual(addItem.Id, fridgeItem.Id, "not equal");
            });
            task.Wait();                        
        }
    }
}
