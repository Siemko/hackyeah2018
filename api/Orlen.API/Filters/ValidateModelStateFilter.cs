using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Orlen.API.Filters
{
    public class ValidateModelStateFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            ValidateModelState(context);
        }

        private static void ValidateModelState(ActionExecutingContext actionContext)
        {
            if (actionContext.ModelState.IsValid) return;

            var errorsList = actionContext.ModelState.Values.Where(v => v.Errors.Any()).SelectMany(v => v.Errors);
            var modelErrors = errorsList as ModelError[] ?? errorsList.ToArray();
            var errorsMessages = modelErrors.Select(e => e.ErrorMessage).Where(m => !string.IsNullOrEmpty(m));

            var enumerable = errorsMessages as string[] ?? errorsMessages.ToArray();

            var result = new ContentResult {Content = string.Join("; ", enumerable), StatusCode = (int) HttpStatusCode.BadRequest};

            actionContext.Result = result;
        }
    }
}