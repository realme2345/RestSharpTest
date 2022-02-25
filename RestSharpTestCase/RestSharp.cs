using System;
using JsonServerRestSharpTest;
using System.Collections.Generic;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace RestSharpTestCase
{
    public class Person
    {
        public string Firstperson { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhNum { get; set; }
        public string Address { get; set; }
    }
    [TestClass]
    public class RestSharptestCase
    {
        RestClient client;
        [TestInitialize]
        public void Setup()
        {
            client = new RestClient("http://localhost:5000");
        }
        [TestMethod]
        public void TestMethod1()
        {
            RestResponse response = getEmployeeList();
            //assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Person> dataResponse = JsonConvert.DeserializeObject<List<Person>>(response.Content);
            Assert.AreEqual(4, dataResponse.Count);
        }
        private RestResponse getEmployeeList()
        {
            //arrange
            RestRequest request = new RestRequest("/AddressBook/list", Method.Get);
            //act
            RestResponse response = client.ExecuteAsync(request).Result;
            return response;
        }
        /// <summary>
        /// Adding the people
        /// </summary>
        [TestMethod]
        public void AddPerson()
        {
            // arrange
            RestRequest request = new RestRequest("/AddressBook/create", Method.Post);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(
            new Person
            {

                FirstPerson = "pavan",
                lastName = "guggill",
                Email = "pavan@gmail.com",
                PhNum = "8347764435",
                Address = "KHM"
            });
            // act
            RestResponse response = client.ExecuteAsync(request).Result;
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Person dataResponse = JsonConvert.DeserializeObject<Person>(response.Content);
           Assert.AreEqual("pavan", dataResponse.Firstperson);
            Assert.AreEqual("guggill", dataResponse.LastName);
            Assert.AreEqual("pavan@gmail.com", dataResponse.Email);
            Assert.AreEqual("8347764435", dataResponse.PhNum);
            Assert.AreEqual("KHM", dataResponse.Address);
        }
        /// <summary>
        /// Update the 
        /// </summary>
        [TestMethod]
        public void UpdatePerson()
        {
            // arrange
            RestRequest request = new RestRequest("/AddressBook/update/1", Method.Put);
            request.AddHeader("Content-type", "application/json");
            request.AddJsonBody(
            new
            {

                FirstPerson = "Raja",
                lastName = "Kongara",
                Email = "Kongara@gmail.com",
                PhNum = "923874744",
                Address = "KHM"
            });
            // act
            RestResponse response = client.ExecuteAsync(request).Result;
            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Person dataResponse = JsonConvert.DeserializeObject<Person>(response.Content);
            Assert.AreEqual("923874744", dataResponse.PhNum);
            Assert.AreEqual("KHM", dataResponse.Address);
        }
        /// <summary>
        /// Deleting the record
        /// </summary>
        [TestMethod]
        public void GivenEmployee_OnDelete_ShouldReturnSuccess()
        {
            // arrange
            RestRequest request = new RestRequest("/AddressBook/delete/2", Method.Delete);

            // act
            RestResponse response = client.ExecuteAsync(request).Result;

            // assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        }
    }
}
