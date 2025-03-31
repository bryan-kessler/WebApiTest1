using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiUnitTest
{
    [TestClass]
    public class UnitTest1:BaseTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Perform the POST request
            //var response = HttpClient.PostAsJsonAsync("travel/v1/quotes/rate", new { }).Result;
            var response = HttpClient.GetAsync("https://api.weather.gov/").Result;

            // Ensure the request was successful
            response.EnsureSuccessStatusCode();

            // Read the response content as a string
            var content = response.Content.ReadAsStringAsync().Result;

            // Add assertions here to validate the response
            Assert.IsNotNull(content);
            Assert.AreEqual("Success", content); // Check if the response content matches the expected value
        }

    }
}

