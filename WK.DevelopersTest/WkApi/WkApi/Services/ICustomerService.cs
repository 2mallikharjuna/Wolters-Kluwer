using System;
using WkApi.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WkApi.Services
{
    /// <summary>
    /// ICustomer service definition
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Get All the Customers
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        /// <summary>
        /// Get the Customer by Guid
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IEnumerable<Customer>> GetCustomersByIdAsync(string id);
        /// <summary>
        /// Add the Customer entry
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<Customer> AddCustomerAsync(Customer customer);
        /// <summary>
        /// Add the Customer entry
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<IEnumerable<Customer>> AddCustomersAsync(IEnumerable<Customer> customer);

        /// <summary>
        /// Update the Customer entry
        /// </summary>        
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<Customer> UpdateCustomerAsync(Guid id, Customer customer);

        /// <summary>
        /// update the list of customers
        /// </summary>
        /// <param name="customers"></param>
        /// <returns></returns>
        Task<IEnumerable<Customer>> UpdateCustomersAsync(IEnumerable<Customer> customers);
        /// <summary>
        /// Delete the Customer entry
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        Task<Customer> DeleteCustomerAsync(Guid CustomerId);
    }
}
