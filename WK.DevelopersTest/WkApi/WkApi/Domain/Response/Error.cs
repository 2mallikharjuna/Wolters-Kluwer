using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WkApi.Domain.Response
{
    /// <summary>
    /// Error Model
    /// </summary>
    public class Error
    {
        /// <summary>
        /// Error code
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 1)]
        public string code { get; set; }
        /// <summary>
        /// Error Message
        /// </summary>

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 2)]
        public string Message { get; set; }
    }
}
