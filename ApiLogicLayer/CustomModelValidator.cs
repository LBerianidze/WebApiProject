using ApiDataLayer.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace ApiLogicLayer
{
	public class CustomModelValidator : ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			if (!context.ModelState.IsValid)
			{
				var response = new Response
				{
					ErrorCode = 1,
					Status = "Error",
					Description = "Bad params",
					Params = context.ModelState.Select(t => t.Key + ": " + string.Join(", ", t.Value.Errors.Select(t => t.ErrorMessage))).ToArray()
				};

				context.Result = new JsonResult(response)
				{
					StatusCode = 400
				};
			}
		}
	}
}
