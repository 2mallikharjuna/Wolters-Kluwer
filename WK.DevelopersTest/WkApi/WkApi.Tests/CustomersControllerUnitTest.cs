using Moq;
using System;
using NUnit.Framework;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using WkApi.Services;
using WkApi.Controllers;
using WkApi.Domain.Models;

namespace WkApi.Tests
{    
    public class CustomersControllerUnitTest
    {
        private Mock<ICustomerService> _customerService;
        private Mock<ILogger<CustomersController>> _logger;
        private CustomersController _controller;

        [SetUp]
        public void Setup()
        {
            _customerService = new Mock<ICustomerService>();
            _logger = new Mock<ILogger<CustomersController>>();
            _controller = new CustomersController(_customerService.Object, _logger.Object);
        }

        [Test]
        public async Task GetAllCustomers_NullResponse_Notfound()
        {
            //arrange
            _customerService.Setup(x => x.GetAllCustomersAsync()).Returns(Task.FromResult((IEnumerable<Customer>)null));

            //act
            var result = await _controller.ListAllCustomers();

            //assert
            Assert.IsInstanceOf<NotFoundResult>(result);            
        }

        [Test]
        public async Task GetAllCustomers_Datafound_OkResponse()
        {
            //arrange
            var response = new List<Customer> { new Customer() } as IEnumerable<Customer>;
            _customerService.Setup(x => x.GetAllCustomersAsync()).Returns(Task.FromResult(response));

            //act
            var result = await _controller.ListAllCustomers() as OkObjectResult;

            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);            
            Assert.AreEqual(result.Value, response);
        }

        [Test]
        public async Task GetAllCustomers_Datafound_ReturnsBadRequest()
        {
            //arrange
            _customerService.Setup(x => x.GetAllCustomersAsync()).Throws<Exception>();

            //act
            var result = await _controller.ListAllCustomers();

            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);            
        }

        [Test]
        public async Task GetCustomerByName_NullResponse_Notfound()
        {
            //arrange            
            string id = new Guid().ToString();
            var response = new List<Customer>() as IEnumerable<Customer>;
            _customerService.Setup(x => x.GetCustomersByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(response));

            //act
            var result = await _controller.GetCustomersById(id);

            //assert            
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetCustomerById_Datafound_OkResponse()
        {
            //arrange
            string id = new Guid().ToString();
            var response = new List<Customer> { new Customer() } as IEnumerable<Customer>;

            _customerService.Setup(x => x.GetCustomersByIdAsync(It.IsAny<string>())).Returns(Task.FromResult(response));

            //act
            var result = await _controller.GetCustomersById(id) as OkObjectResult;

            //assert            
            Assert.IsInstanceOf<OkObjectResult>(result);
            Assert.AreEqual(result.Value, response);
        }

        [Test]
        public async Task GetCustomerByName_Datafound_ReturnsBadRequest()
        {            
            //arrange
            string id = new Guid().ToString();
            _customerService.Setup(x => x.GetCustomersByIdAsync(It.IsAny<string>())).Throws<Exception>();            

            //act
            var result = await _controller.GetCustomersById(id);

            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);            
        }

        [Test]
        public async Task AddCustomer_Exception_BadRequestResult()
        {
            //arrange
            var customer = new Customer();
            _customerService.Setup(x => x.AddCustomerAsync(It.IsAny<Customer>())).Throws<Exception>();

            //act
            var result = await _controller.CreateCustomer(customer);

            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task AddCustomer_NoException_OkResult()
        {
            //arrange
            var customer = new Customer();
            _customerService.Setup(x => x.AddCustomerAsync(It.IsAny<Customer>())).Returns(Task.FromResult(customer));

            //act
            var result = await _controller.CreateCustomer(customer);
            var statuscode = result.GetType().GetProperty("StatusCode").GetValue(result, null).ToString();
            //assert
            Assert.IsInstanceOf<ObjectResult>(result);
            Assert.IsTrue(statuscode == "201");
        }

        [Test]
        public async Task UpdateCustomer_Exception_BadRequestResult()
        {
            //arrange            
            var customer = new Customer() { Id = new Guid() };
            _customerService.Setup(x => x.UpdateCustomerAsync(It.IsAny<Guid>(), It.IsAny<Customer>())).Throws<Exception>();

            //act
            var result = await _controller.UpdateCustomer(customer.Id, customer);

            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public async Task UpdateCustomer_NoException_OkResult()
        {
            //arrange            
            var customer = new Customer() { Id = new Guid() };
            _customerService.Setup(x => x.UpdateCustomerAsync(It.IsAny<Guid>(), It.IsAny<Customer>())).Returns(Task.FromResult(customer));

            //act
            var result = await _controller.UpdateCustomer(customer.Id, customer);

            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);           

        }

        [Test]
        public async Task DeleteCustomer_Exception_BadRequestResult()
        {
            //arrange
            var CustomerId = new Guid();
            var Customer = new Customer();
            _customerService.Setup(x => x.DeleteCustomerAsync(It.IsAny<Guid>())).Throws<Exception>();

            //act
            var result = await _controller.DeleteCustomer(CustomerId);

            //assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);            
        }

        [Test]
        public async Task DeleteCustomer_NoException_OkResult()
        {
            //arrange
            var CustomerId = new Guid();
            var Customer = new Customer();
            _customerService.Setup(x => x.DeleteCustomerAsync(It.IsAny<Guid>())).Returns(Task.FromResult(Customer));

            //act
            var result = await _controller.DeleteCustomer(CustomerId);

            //assert
            Assert.IsInstanceOf<OkObjectResult>(result);            
        }
    }
}