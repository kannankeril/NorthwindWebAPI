using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace NorthwindWebAPI.Filters
{
    /// <summary>
    /// By extending the ExceptionFilter attribute you can, for example, establish a mapping 
    /// of thrown exceptions to HTTP status codes.
    /// decorating the controllers with the ControllerExceptionFilter attribute will enforce this
    ///  policy, and ensure that proper HTTP status codes are returned when exceptions are thrown.
    /// </summary>
    public class ControllerExceptionFilterAttribute:ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var ex = actionExecutedContext.Exception;
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError); //default error

            if(ex is KeyNotFoundException || ex is ArgumentOutOfRangeException)
            {
                response.StatusCode = HttpStatusCode.NotFound;
            }
            else if(ex is ArgumentException)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }

            response.Content = new StringContent(ex.Message);
            actionExecutedContext.Response = response;
        }
    }
}