using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;
using Orlen.Common.Exceptions;
using Orlen.Common.Extensions;
using Orlen.Common.Models;

namespace Orlen.API.Attributes
{
    public class ExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var logger = (ILogger)context.HttpContext.RequestServices.GetService(typeof(ILogger<ExceptionHandlerAttribute>));
            var msg = context.Exception.GetFullMessage();

            var apiErrorModel = new ApiErrorModel
            {
                Message = msg,
                StackTrace = context.Exception.StackTrace,
            };

            var result = new JsonResult(apiErrorModel);
            logger.LogError(msg);
            switch (context.Exception.GetType().Name)
            {
                case nameof(ResourceNotFoundException):
                    result.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case nameof(ResourceDuplicatedException):
                    result.StatusCode = (int)HttpStatusCode.Conflict;
                    break;
                case nameof(InvalidParameterException):
                    result.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                default:
                    result.StatusCode = (int)HttpStatusCode.InternalServerError;
                    logger.LogError(context.Exception, context.Exception.StackTrace);
                    break;
            }

            context.Result = result;
            context.ExceptionHandled = true;
        }
    }
}