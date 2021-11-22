using System;
using WkApi.Domain.Models;
using System.Threading.Tasks;
using WkApi.Repositories.Base;
using System.Collections.Generic;

namespace WkApi.Repositories
{
    /// <summary>
    /// Customer Reposiotory definition
    /// </summary>
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        /// <summary>
        /// Get all the customers
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        /// <summary>
        /// Get the customers matched with first or last name
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<IEnumerable<Customer>> GetCustomerByIdAsync(string customerId);
        /// <summary>
        /// Add the new customer record
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        Task<Customer> AddCustomerAsync(Customer customer);
        /// <summary>
        /// update the existing customer record
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<Customer> UpdateCustomerAsync(Guid customerId, Customer customer);
        /// <summary>
        /// Delete the customer record
        /// </summary>
        /// <param name="CustomerId"></param>
        /// <returns></returns>
        Task<Customer> DeleteCustomerAsync(Guid customerId);
    }
}
