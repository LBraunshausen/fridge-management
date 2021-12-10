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
    public class DBServiceTest
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
                await DBService<FridgeItem>.Add(addItem);
                FridgeItem fridgeItem = null;

                fridgeItem = await DBService<FridgeItem>.GetById(addItem.Id);

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
                await DBService<FridgeItem>.Add(addItem);
                FridgeItem fridgeItem = null;

                var fridgeItems = (ICollection)await DBService<FridgeItem>.GetItems();

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
                await DBService<FridgeItem>.Add(addItem);
                FridgeItem fridgeItem = null;


                addItem.Text = "Testprodukt2";
                await DBService<FridgeItem>.Edit(addItem);

                fridgeItem = await DBService<FridgeItem>.GetById(addItem.Id);

                Assert.AreEqual(addItem.Text, fridgeItem.Text, "not equal");                
            });
            task.Wait();
        }

        [TestMethod]
        public void TestDeleteAll()
        {
            Task task = Task.Run(async () =>
            {
                await DBService<FridgeItem>.DeleteAll();

                var fridgeItems = (ICollection)await DBService<FridgeItem>.GetItems();

                Assert.IsTrue(fridgeItems.Count == 0);
            });
        }

        [TestMethod]
        public void TestDelete()
        {
            FridgeItem item = new FridgeItem()
            {
                Id = new Guid(),
                Text = "Testprodukt",
                Amount = 1,
                ExpirationDate = DateTime.Now
            };

            Task.Run(async () =>
            {
                await DBService<FridgeItem>.Add(item);

                await DBService<FridgeItem>.Delete(item.Id);

                var item1 = await DBService<FridgeItem>.GetById(item.Id);

                Assert.AreEqual(1, 1);
            });



        }
    }


    
}
