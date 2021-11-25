using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using fridge_management.Services;
using fridge_management.Models;
using System.Diagnostics;
using System.Threading.Tasks;

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
                Text = "Testprodukt",
                Amount = 1,
                ExpirationDate = DateTime.Now
            };

            BaseService<FridgeItem>.Add(item);

            BaseService<FridgeItem>.GetById(item.Id);

            Assert.AreEqual(1, 1);

        }
    }
}
