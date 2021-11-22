using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WkApi.Domain.Common.Base
{
    /// <summary>
    /// Common model contract definition
    /// </summary>    
    public abstract class EntityBase
    {
        /// <summary>
        /// Get or Set the Entity Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }
    }
}
