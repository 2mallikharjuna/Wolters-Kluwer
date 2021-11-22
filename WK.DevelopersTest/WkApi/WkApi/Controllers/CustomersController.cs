using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WkApi.Domain.Models;
using WkApi.Filters;
using WkApi.Services;

namespace WkApi.Controllers
{
    /// <summary>
    /// Customers Api Controller
    /// </summary>
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]    
    [ApiController]
    public class CustomersController : ControllerBase
    {
        #region Members
        private readonly ICustomerService _CustomerService;
        private ILogger<CustomersController> Logger { get; }
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="CustomerService">Inject ICustomerService</param>
        /// <param name="logger">Inject CustomersController logger</param>
        public CustomersController(ICustomerService CustomerService, ILogger<CustomersController> logger)
        {
            _CustomerService = CustomerService ?? throw new ArgumentNullException(nameof(ICustomerService));
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion


        #region Controller Methods
        /// <summary>
        /// HttpGet Method to return the all the Customers
        /// </summary>        
        /// <returns>Customers in json format</returns>         
        [HttpGet("ListAll")]
        [ResultsFilter]
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> ListAllCustomers()
        {
            try
            {
                var result = await _CustomerService.GetAllCustomersAsync();
                if (result != null && result.Count() != 0)
                    return Ok(result);
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Get All Customers. Request: " + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetAllCustomersAsync. Request: " + " Error " + ex);
                return BadRequest(ex);
            }
        }


        /// <summary>
        /// HttpGet to return the matching customers
        /// </summary>        
        /// <returns>Customers in json format</returns>         
        [HttpGet("{Id}")]
        [RequestValidate]
        [ResultsFilter]
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> GetCustomersById([Required] string id)
        {
            try
            {
                var result = await _CustomerService.GetCustomersByIdAsync(id);
                if (result.Count() != 0)
                    return Ok(result);
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Get Matching Customer. Request: " + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in GetCustomerByIdAsync. Request: " + " Error " + ex);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Update the existing customer details 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [RequestValidate]
        [ResultsFilter]
        [ProducesResponseType(typeof(Customer), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> UpdateCustomer(Guid id, [FromBody] Customer customer)
        {
            try
            {
                if (id != customer.Id)
                {
                    return BadRequest();
                }
                var result = await _CustomerService.UpdateCustomerAsync(id, customer);
                if (result != null)
                    return Ok(result);
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Update Customer. Request: " + JsonConvert.SerializeObject(customer) + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in UpdateCustomerAsync. Request: " + JsonConvert.SerializeObject(customer) + " Error " + ex);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Update list of customer details
        /// </summary>
        /// <param name="customers"></param>
        /// <returns></returns>
        [HttpPut("UpdateCustomers")]
        [RequestValidate]
        [ResultsFilter]
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> UpdateCustomers([FromBody] IEnumerable<Customer> customers)
        {
            try
            {
                var result = await _CustomerService.UpdateCustomersAsync(customers);
                if (result != null)
                    return Ok(result);
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Update Customer. Request: " + JsonConvert.SerializeObject(customers) + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in UpdateCustomerAsync. Request: " + JsonConvert.SerializeObject(customers) + " Error " + ex);
                return BadRequest(ex);
            }
        }


        /// <summary>
        /// HttpPost method to Create the customer using 
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        [HttpPost]
        [RequestValidate]
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            try
            {
                var entity = await _CustomerService.AddCustomerAsync(customer);
                if (entity != null)
                    return new ObjectResult(entity) { StatusCode = StatusCodes.Status201Created };
                return BadRequest();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Create Customer. Request: " + JsonConvert.SerializeObject(customer) + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in CreateCustomerAsync. Request: " + JsonConvert.SerializeObject(customer) + " Error " + ex);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// HttpPost method to Create the customer using 
        /// </summary>
        /// <param name="customers"></param>
        /// <returns></returns>
        [HttpPost("InsertCustomers")]
        [RequestValidate]
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> CreateCustomers([FromBody] IEnumerable<Customer> customers)
        {
            try
            {
                var entity = await _CustomerService.AddCustomersAsync(customers);
                if (entity != null)
                    return new ObjectResult(entity) { StatusCode = StatusCodes.Status201Created };
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Create Customer. Request: " + JsonConvert.SerializeObject(customers) + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in CreateCustomerAsync. Request: " + JsonConvert.SerializeObject(customers) + " Error " + ex);
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// HttpDelete method to delete the customer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpDelete("{CustomerId}")]
        [RequestValidate]
        [ResultsFilter]
        [ProducesResponseType(typeof(IEnumerable<Customer>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(BadRequestObjectResult))]
        public async Task<IActionResult> DeleteCustomer([Required] Guid customerId)
        {
            try
            {
                var response = await _CustomerService.DeleteCustomerAsync(customerId);
                if (response != null)
                    return Ok(response);
                return NotFound();
            }
            catch (AggregateException aggEx)
            {
                string errorMsg = String.Empty;
                Logger.LogError("Error in Delete Customer. Request: " + JsonConvert.SerializeObject(customerId) + " Error " + aggEx);
                aggEx.InnerExceptions.ToList().ForEach(ex => errorMsg += $" Error Message :{ex.Message} StackTace: {ex.StackTrace.ToString()} " + Environment.NewLine);
                return BadRequest(errorMsg);
            }
            catch (Exception ex)
            {
                Logger.LogError("Error in Get DeleteCustomerAsync. Request: " + JsonConvert.SerializeObject(customerId) + " Error " + ex);
                return BadRequest(ex);
            }
        }
        #endregion
    }
}
