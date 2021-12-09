using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using fridge_management.Services;
using fridge_management.Models;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Essentials;
using System.Collections;

namespace fridge_management_Test
{
    [TestClass]
    public class HttpRequestTest
    {
        [TestMethod]
        public void SendHttpRequest()
        {
            string ean = "4006748001404";

            var jObject = HttpRequestService.getItemByEan(ean);

            Assert.IsNotNull(jObject);
        }

    }
    
}
