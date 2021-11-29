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

                fridgeItem = await BaseService<FridgeItem>.GetById(addItem.Id);

                Assert.AreEqual(addItem.Id, fridgeItem.Id, "not equal");
            });
            task.Wait();                        
        }

        [TestMethod]
        public void TestGetItems()
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

                var fridgeItems = (ICollection)await BaseService<FridgeItem>.GetItems();

                Assert.IsTrue(fridgeItems.Count > 0); 
            });
            task.Wait();
        }

        [TestMethod]
        public void TestEdit()
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


                addItem.Text = "Testprodukt2";
                await BaseService<FridgeItem>.Edit(addItem);

                fridgeItem = await BaseService<FridgeItem>.GetById(addItem.Id);

                Assert.AreEqual(addItem.Text, fridgeItem.Text, "not equal");                
            });
            task.Wait();
        }

        [TestMethod]
        public void TestDeleteAll()
        {
            Task task = Task.Run(async () =>
            {
                await BaseService<FridgeItem>.DeleteAll();

                var fridgeItems = (ICollection)await BaseService<FridgeItem>.GetItems();

                Assert.IsTrue(fridgeItems.Count == 0);
            });
        }
    }
}
