using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NationalLandmarks.Server.Infrastructure.Filters
{
    public class ModelOrNotFoundActionFilter: ActionFilterAttribute //add Attribute for not being global
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Result is ObjectResult result)
            {
                var model = result.Value;
                if(model == null)
                {
                    context.Result = new NotFoundResult();
                }
            }

            //base.OnActionExecuted(context);
        }
    }
}
