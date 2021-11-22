using System;
using WkApi.Domain.Common;
using WkApi.Domain.Common.Base;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WkApi.Domain.Models
{
    /// <summary>
    /// Customer entity including mapping implementation
    /// </summary>
    public class Customer : EntityBase, IEntityTypeConfiguration<Customer>
    {
        /// <summary>
        /// Get or Set the first name
        /// </summary>
        [Required(ErrorMessage = "The First Name is mandatory")]
        [StringLength(50, MinimumLength = 3)]
        [RegularExpression(@"^[A-Z][a-zA-Z_]*$")]
        public string FirstName
        {
            get; set;
        }
        /// <summary>
        /// Get or sets the last name
        /// </summary>
        [Required(ErrorMessage = "The Last Name is mandatory")]
        [StringLength(50, MinimumLength = 1)]
        [RegularExpression(@"^[A-Z][a-zA-Z_]*$")]
        public string LastName { get; set; }

        /// <summary>
        /// Get or set the DOB
        /// </summary>
        [Required(ErrorMessage = "The DOB is mandatory")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{dd-MM-yyyy")]
        public DateTime DateOfBirth
        {
            get; set;
        }
        /// <summary>
        /// Get or set the EmailId
        /// </summary>
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")]
        [StringLength(15, MinimumLength = 5)]
        public string EmailId
        {
            get; set;
        }

        /// <summary>
        /// Get or set the Gender
        /// </summary>
        public Gender? Gender
        {
            get; set;
        }
        /// <summary>
        /// Get or set the Address
        /// /// </summary>
        [RegularExpression(@"^[A-Za-z0-9]+(?:\s[A-Za-z0-9',_-]+)+$")]
        [StringLength(50, MinimumLength = 10)]
        public string Address
        {
            get; set;
        }
        /// <summary>
        /// Get or set the MobileNo
        /// </summary>
        [RegularExpression(@"^[0-9]*$")]
        [StringLength(10, MinimumLength = 10)]
        public string MobileNo
        {
            get; set;
        }

        [RegularExpression(@"^[0-9]*$")]
        [StringLength(4, MinimumLength = 4)]
        /// <summary>
        /// Get or set the PinCode
        /// </summary>
        public string PinCode
        {
            get; set;
        }

        /// <summary>
        /// Configure the Customer model
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            // Set key for entity
            builder.HasKey(p => p.Id);
            // Set configuration for columns            
            builder.Property(p => p.FirstName);
            builder.Property(p => p.LastName);
            builder.Property(p => p.EmailId);
            builder.Property(p => p.Address);
            builder.Property(p => p.PinCode);
            builder.Property(p => p.Gender);
        }
    }
}
