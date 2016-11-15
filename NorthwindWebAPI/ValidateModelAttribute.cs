using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace NorthwindWebAPI.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// If model validation fails, this filter returns an HTTP response that contains the validation errors. 
        /// In that case, the controller action is not invoked.
        /// To apply this filter to all Web API controllers, add an instance of the filter to the 
        /// HttpConfiguration.Filters collection during configuration.
        /// Ref.: https://www.asp.net/web-api/overview/formats-and-model-binding/model-validation-in-aspnet-web-api
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if(actionContext.ModelState.IsValid == false)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest
                                                                                   , actionContext.ModelState);
            }
        }
    }
}
