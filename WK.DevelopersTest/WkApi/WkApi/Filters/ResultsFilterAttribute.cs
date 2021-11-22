using System;
using Newtonsoft.Json;
using WkApi.Domain.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WkApi.Filters
{
    /// <summary>
    /// Results Filter attribute to check return result from Web API
    /// </summary>
    public class ResultsFilterAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// Over riding the OnActionExecuted
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

            var resonseBody = new ResultResponse();
            if (!filterContext.ModelState.IsValid)
            {
                var e = new Error()
                {
                    code = "Error",
                    Message = "Validation Failed",

                };
                resonseBody.Error = e;
                filterContext.HttpContext.Response.StatusCode = 400;
                filterContext.ExceptionHandled = true;
            }

            if (filterContext.Exception != null)
            {
                var e = new Error()
                {
                    code = "Exception",
                    Message = filterContext.Exception.Message,
                };
                resonseBody.Error = e;
                filterContext.HttpContext.Response.StatusCode = 400;
                filterContext.ExceptionHandled = true;
            }
            if (!filterContext.ExceptionHandled)
            {
                if (filterContext.Result is BadRequestObjectResult objectResult)
                {
                    var oResult = objectResult as ObjectResult;
                    var e = new Error()
                    {
                        code = "Exception",
                        Message = JsonConvert.SerializeObject(oResult.Value, Formatting.None),
                    };
                    resonseBody.Error = e;
                    filterContext.HttpContext.Response.StatusCode = 400;
                    filterContext.ExceptionHandled = true;
                }
            }
            if (filterContext.Result is OkObjectResult okObjectResult)
            {
                resonseBody.Data = okObjectResult.Value;
                filterContext.HttpContext.Response.StatusCode = 200;
            }
        }
        /// <summary>
        /// Over ride the OnActionExecuting
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {

        }
    }
}
