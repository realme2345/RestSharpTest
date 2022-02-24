using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace RestSharpTest
{
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }

    [TestClass]
    public class RestSharpTestCase
    {
        RestClient client;
        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:4000");
        }
        private IRestResponse getEmployeeList()
        {
            //arrange
            RestRequest request = new RestRequest("/AddressBook", Method.Get);
            //act
            IRestResponse response = client.Execute(request);
            return response;
        }
        [TestMethod]
        public void ReturnEmployeeList()
        {
            IRestResponse response = getEmployeeList();
            //assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Person> dataResponse = JsonConvert.DeserializeObject<List<Person>>(response.Content);
            Assert.AreEqual(4,dataResponse.Count);
            foreach(Person person in dataResponse)
            {
                System.Console.WriteLine("FirstName" + person.FirstName);
            }
        }
        
    }
}
