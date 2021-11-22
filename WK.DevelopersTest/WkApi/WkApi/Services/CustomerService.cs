using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using WkApi.Domain.Models;
using WkApi.Repositories;
using Microsoft.Extensions.Logging;

namespace WkApi.Services
{
    /// <summary>
    /// Customer service implementaion
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _CustomerRepository;
        private ILogger<CustomerService> Logger { get; }

        /// <summary>
        /// CustomerService constructor
        /// </summary>
        /// <param name="CustomerRepository"></param>        
        /// <param name="logger"></param>
        public CustomerService(ICustomerRepository CustomerRepository, ILogger<CustomerService> logger)
        {
            _CustomerRepository = CustomerRepository ?? throw new ArgumentNullException(nameof(ICustomerService));
            Logger = logger ?? throw new ArgumentNullException(nameof(ILogger<CustomerService>));
        }

        /// <summary>
        /// Get all the Customer entiries from Customer table
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return await _CustomerRepository.GetAllCustomersAsync();
        }

        /// <summary>
        /// Get the Customer entity from give Customer Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> GetCustomersByIdAsync(string id)
        {
            return await _CustomerRepository.GetCustomerByIdAsync(id);
        }

        /// <summary>
        /// Add Customer entity to Customer table
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            return await _CustomerRepository.AddCustomerAsync(customer);
        }

        /// <summary>
        /// Add Customer entity to Customer table
        /// </summary>
        /// <param name="customers"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> AddCustomersAsync(IEnumerable<Customer> customers)
        {
            return customers.Select(async customer => await _CustomerRepository.AddCustomerAsync(customer)).Select(addedCustomer => addedCustomer.Result);
        }

        /// <summary>
        /// update the Customer entity of given Customer Id
        /// </summary>
        /// <param name="customer"></param>        
        /// <param name="id"></param>       
        /// <returns></returns>
        public async Task<Customer> UpdateCustomerAsync(Guid id,Customer customer)
        {
            return await _CustomerRepository.UpdateCustomerAsync(id, customer);
        }

        /// <summary>
        /// update the Customer entity of given Customer Id
        /// </summary>
        /// <param name="customers"></param>        
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> UpdateCustomersAsync(IEnumerable<Customer> customers)
        {
            return (IEnumerable<Customer>)customers.Select(async customer => await _CustomerRepository.UpdateCustomerAsync(customer.Id, customer));
        }

        /// <summary>
        /// Delete the Customer entity of given Customer Id
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        public async Task<Customer> DeleteCustomerAsync(Guid CustomerId)
        {
            return await _CustomerRepository.DeleteCustomerAsync(CustomerId);
        }

    }
}
