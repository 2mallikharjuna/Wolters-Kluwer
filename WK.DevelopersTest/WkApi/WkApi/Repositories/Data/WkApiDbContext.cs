using WkApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;

namespace WkApi.Repositories.Data
{
    /// <summary>
    /// Common application DB Context
    /// </summary>
    public class WkApiDbContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public WkApiDbContext (DbContextOptions<WkApiDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Customer> Customer { get; set; }

        /// <summary>
        /// Override Model creation
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Apply configurations for entity
            modelBuilder.ApplyConfiguration(new Customer());

            IList<Customer> defaultCustomers = new List<Customer>();

            defaultCustomers.Add(new Customer() { FirstName = "TestFirstName1", LastName = "TestLastName1", Address = "11 Collins Street, Melbourne", DateOfBirth = new DateTime(1991, 1, 1), Gender = Domain.Common.Gender.Male, MobileNo="0401645111", EmailId = "Test1@yahoo.com", Id = Guid.NewGuid(), PinCode = "1038" });
            defaultCustomers.Add(new Customer() { FirstName = "TestFirstName2", LastName = "TestLastName2", Address = "22 Collins Street, Melbourne", DateOfBirth = new DateTime(1992, 2, 2), Gender = Domain.Common.Gender.Female, MobileNo = "0401645222", EmailId = "Test2@yahoo.com", Id = Guid.NewGuid(), PinCode = "2038" });
            defaultCustomers.Add(new Customer() { FirstName = "TestFirstName3", LastName = "TestLastName3", Address = "33 Collins Street, Melbourne", DateOfBirth = new DateTime(1993, 3, 3), Gender = Domain.Common.Gender.NotToSay, MobileNo = "0401645333", EmailId = "Test3@yahoo.com", Id = Guid.NewGuid(), PinCode = "3038" });

            modelBuilder.Entity<Customer>().HasData(defaultCustomers);

        }
    }
}
