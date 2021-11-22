using System;
using WkApi;
using System.Net;
using System.Text;
using System.Net.Http;
using NUnit.Framework;
using System.Text.Json;
using WkApi.Domain.Models;
using WkApi.Domain.Common;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace WkApiApi.Tests
{
    [TestFixture]
    public class CustomersControllerIntegrationTest : IDisposable
    {
        private APIWebApplicationFactory<Startup> _factory;
        private HttpClient _client;
        private const string version = "v1.0";

        [OneTimeSetUp]
        public void GivenARequestToTheController()
        {
            _factory = new APIWebApplicationFactory<Startup>();
            _client = _factory.CreateClient();
        }

        private Customer ToCustomer(string firstName, string lastName, string dob, string id, string address, Gender gender, string emailId, string mobileNo, string pincode)
        {
            return new Customer() {
                Id = new Guid(id),
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = DateTime.Parse(dob),
                Address = address,
                Gender = gender,
                EmailId = emailId,
                MobileNo = mobileNo,
                PinCode = pincode
            };
        }

        [Test, Order(1)]
        [TestCase("Mallik", "WkApi", "1984-04-6", "ef743a6d-e780-4406-9fd7-6e398db82adc", "1 Collins Street, Melbourne", Gender.Male, "test1@yahoo.com", "0401645133", "3000")]
        [TestCase("Udaya", "WkApi", "1986-04-6", "6db0cc43-70ef-44d5-ac56-39970a036b3d", "2 Collins Street, Melbourne", Gender.Female, "test1@yahoo.com", "0401645243", "3000")]
        [TestCase("Daksha", "WkApi", "2010-04-6", "753aca39-53f3-426c-b15e-c5385c38d3e4", "3 Collins Street, Melbourne", Gender.Female, "test1@yahoo.com", "0401645353", "3000")]
        [TestCase("MyLittle", "WkApi", "2014-04-6", "49ff6071-49bc-4372-9c8d-26d190fa9485", "4 Collins Street, Melbourne", Gender.Male, "test1@yahoo.com", "0401645463", "3000")]
        [TestCase("RandomGuy", "WkApi", "2000-04-6", "49ff6071-49bc-4372-9c8d-26d190fa9185", "12 Collins Street, Melbourne", Gender.NotToSay, "test1@yahoo.com", "0401643003", "3000")]
        public async Task WhenAddingCustomer_ThenTheResultIsOk(string firstName, string lastName, string dob, string id, string address, Gender gender, string emailId, string mobileNo, string pincode)
        {
            var customer = ToCustomer(firstName, lastName, dob, id, address, gender, emailId, mobileNo, pincode);
            
            var json = JsonSerializer.Serialize(customer);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await _client.PostAsync($"api/{version}/Customers/", httpContent);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }        

        [Test, Order(2)]
        public async Task WhenReadingAddedCustomer_ThenTheResultIsOk()
        {
            var textContent = new ByteArrayContent(Encoding.UTF8.GetBytes("Backpack for his applesauce"));
            textContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

            var result = await _client.GetAsync($"api/{version}/Customers/ListAll");
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseContent = await result.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Length == 1216);
        }

        [Test, Order(3)]
        [TestCase("753aca39-53f3-426c-b15e-c5385c38d3e4")]        
        public async Task WhenGettingSelectiveCustomer_ThenTheResultIsOk(string id)
        {
            var json = JsonSerializer.Serialize(id);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await _client.GetAsync($"api/{version}/Customers/" + id);
            
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.NotNull(result.Content);

            var responseContent = await result.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Length == 243);
        }

        [Test, Order(4)]
        [TestCase("6db0cc43-70ef-44d5-ac56-39970a036b3d")]
        public async Task WhenDeleting_ThenTheResultIsOk(string guid)
        {
            var textContent = new ByteArrayContent(Encoding.UTF8.GetBytes("Backpack for his applesauce"));
            textContent.Headers.ContentType = new MediaTypeHeaderValue("text/plain");

            var result = await _client.DeleteAsync($"api/{version}/Customers/" + guid);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseContent = await result.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Length == 240);
        }

        [Test, Order(5)]
        [TestCase("MallikUpdated", "WkApi", "2000-04-6", "ef743a6d-e780-4406-9fd7-6e398db82adc", "1 Collins Street, Melbourne", Gender.Male, "test1@yahoo.com", "0401645133", "3000")]
        public async Task WhenUpdating_ThenTheResultIsOk(string firstName, string lastName, string dob, string id, string address, Gender gender, string emailId, string mobileNo, string pincode)
        {
            var customer = ToCustomer(firstName, lastName, dob, id, address, gender, emailId, mobileNo, pincode);

            var json = JsonSerializer.Serialize(customer);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await _client.PutAsync($"api/{version}/Customers/{id}", httpContent);
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var responseContent = await result.Content.ReadAsStringAsync();
            Assert.IsTrue(responseContent.Length == 248);
        }

        public void Dispose()
        {
            _client.Dispose();
        }
    }
}
