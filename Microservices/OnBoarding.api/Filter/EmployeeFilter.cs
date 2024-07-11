using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace OnBoarding.api.Filter
{
    public class EmployeeFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            // write filter logic. conditions which empotee type/name to be allowed.
            var path = context.HttpContext.Request.Path;

            if (path == "/onboarding/api/v1")
            {
                base.OnActionExecuting(context);
            }
            else
            {              
                context.Result = new BadRequestObjectResult("Invalid Employee");
            }
        }
    }
}
