using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using fridge_management.Services;
using fridge_management.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace fridge_management_Test
{
    [TestClass]
    public class BaseServiceTest
    {
        [TestMethod]
        public void TestAdd()
        {
            FridgeItem item = new FridgeItem()
            {
                Id = new Guid(),
                Text = "Testprodukt",
                Amount = 1,
                ExpirationDate = DateTime.Now
            };

            

            var test = new FridgeItem();

            Task.Run(async () =>
            {
                await BaseService<FridgeItem>.Add(item);
                test = await BaseService<FridgeItem>.GetById(item.Id);
                Assert.AreEqual(test, item.Id);
            });

            Console.WriteLine("");
            
        }
    }
}
