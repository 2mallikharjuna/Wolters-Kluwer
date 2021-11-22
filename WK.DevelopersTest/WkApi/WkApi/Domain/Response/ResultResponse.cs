using System;
using Newtonsoft.Json;

namespace WkApi.Domain.Response
{
    /// <summary>
    /// Return Response Result
    /// </summary>
    public class ResultResponse
    {
        /// <summary>
        /// Get or Set Error Object
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 1)]
        public Error Error { get; set; }

        /// <summary>
        /// Get or Set Data Object
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore, Order = 2)]
        public object Data { get; set; }

        /// <summary>
        /// Get the Json fomat of response body
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
