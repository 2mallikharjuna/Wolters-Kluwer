using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WkApi.Domain.Models;
using WkApi.Repositories.Base;
using WkApi.Repositories.Data;

namespace WkApi.Repositories
{
    /// <summary>
    /// Customer Repository implementation
    /// </summary>
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        IQueryable<Customer> _Customers;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="appDbContext"></param>
        public CustomerRepository(WkApiDbContext appDbContext) : base(appDbContext)
        {
            appDbContext.Database.EnsureCreated();
            
            _Customers = GetAll();
        }

        private bool CustomerExists(Guid id)
        {
            return _Customers.Any(e => e.Id == id);
        }

        /// <summary>
        /// Read all the customers
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            return _Customers.ToList();
        }

        /// <summary>
        /// Read the customer matching with first/last name
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Customer>> GetCustomerByIdAsync(string customerId)
        {
            var foundCustomers = _Customers.Where(pItem => pItem.Id.ToString().ToUpper().Contains(customerId.ToUpper())).ToList();
            return foundCustomers;
        }

        /// <summary>
        /// Add the customer to repository
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<Customer> AddCustomerAsync(Customer customer)
        {
            if (CustomerExists(customer.Id)) throw new ArgumentException($"Customer already exists with CustomerID {customer.Id}");
            return await AddAsync(customer);
        }

        /// <summary>
        /// Update the existing customer details 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public async Task<Customer> UpdateCustomerAsync(Guid id, Customer customer)
        {                           
            if (!CustomerExists(id)) throw new ArgumentException($"Customer Not exists with CustomerID {customer.Id}");
            return await UpdateAsync(customer);
        }


        /// <summary>
        /// Delete the existing Customer 
        /// </summary>
        /// /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<Customer> DeleteCustomerAsync(Guid customerId)
        {
            if (!CustomerExists(customerId)) throw new ArgumentException($"Customer Not exists with CustomerID {customerId}");
            return await DeleteAsync(_Customers.FirstOrDefault(pItem => pItem.Id == customerId));
        }
    }
}
