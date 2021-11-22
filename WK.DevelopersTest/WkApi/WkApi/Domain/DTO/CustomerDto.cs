using System;
using WkApi.Domain.Common.Base;

namespace WkApi.Domain.DTO
{
    /// <summary>
    /// Customer DTO
    /// </summary>
    public class CustomerDto : EntityBase
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Address { get; set; }
        public string PinCode { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
    }
}
