using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WkApi.Filters
{
    /// <summary>
    /// Validate the input model to API
    /// </summary>
    public class RequestValidateAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Over-ride OnActionExecuting function
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
