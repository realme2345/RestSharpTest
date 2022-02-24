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
        public int Id { get; set; }
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
            //arrange
            RestRequest request = new RestRequest("/AddressBook/create", Method.Post);
            JObject jobjectBody=new JObject();
            jobjectBody.Add("Firstperson", "Pavan");
            jobjectBody.Add("LastName", "ijfjfhf");
            jobjectBody.Add("Email", "Pavan@gmail.com");
            jobjectBody.Add("PhNum", "938474837");
            jobjectBody.Add("Address", "KHM");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json",jobjectBody,ParameterType.RequestBody);
            //request.AddHeader("Content-Type","application/json");
            //request.AddJsonBody(jobjectBody);
            //act
            RestResponse response = client.ExecuteAsync(request).Result;
            //assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Person person = JsonConvert.DeserializeObject<Person>(response.Content);
            Assert.AreEqual("Raja", person.Firstperson);
            Assert.AreEqual("shdd", person.LastName);
            Assert.AreEqual("Pavankumar@gmail.com", person.Email);
            Assert.AreEqual("938474837", person.PhNum);
            Assert.AreEqual("KHM", person.Address);
            Console.WriteLine(response.Content);
        }
        /// <summary>
        /// Update the 
        /// </summary>
        [TestMethod]
        public void UpdatePerson()
        {
            //arrange
            RestRequest request = new RestRequest("/AddressBook/update/2", Method.Put);
            JObject jobjectBody = new JObject();
            jobjectBody.Add("Firstperson", "Raja");
            jobjectBody.Add("LastName", "shdd");
            jobjectBody.Add("Email", "Pavankumar@gmail.com");
            jobjectBody.Add("PhNum", "938474837");
            jobjectBody.Add("Address", "KHM");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json",jobjectBody,ParameterType.RequestBody);
           // request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(jobjectBody);
            //act
            RestResponse response = client.ExecuteAsync(request).Result;
            //assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Person person = JsonConvert.DeserializeObject<Person>(response.Content);
            //Assert.AreEqual(1,person.Id);
            Assert.AreEqual("Raja", person.Firstperson);
            Assert.AreEqual("shdd", person.LastName);
            Assert.AreEqual("Pavankumar@gmail.com", person.Email);
            Assert.AreEqual("938474837", person.PhNum);
            Assert.AreEqual("KHM", person.Address);
            Console.WriteLine(response.Content);
        }
    }
}