using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Library.Api;

/// <summary>
/// Action filter that logs execution of controller actions.
/// Logs entry, successful completion (with item count only if result is a collection), and exceptions.
/// Replaces duplicated logging logic in controllers.
/// </summary>
public class LoggingActionFilter(ILogger<LoggingActionFilter> logger) : ActionFilterAttribute
{
    /// <summary>
    /// Logs when an action is about to execute.
    /// </summary>
    /// <param name="context">Context containing action metadata.</param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        logger.LogInformation("Initiating {Action}", context.ActionDescriptor.DisplayName);
    }

    /// <summary>
    /// Logs after action execution: success (with item count only for collections) or failure with exception.
    /// If exception occurs, replaces result with 500 error response.
    /// </summary>
    /// <param name="context">Context containing result or exception.</param>
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        var operation = context.ActionDescriptor.DisplayName;

        if ((context.Result is OkObjectResult okResult && okResult.Value != null))
        {
            if (okResult.Value is System.Collections.IEnumerable collection)
            {
                var count = collection.Cast<object>().Count();
                logger.LogInformation("Completed {Operation}. Retrieved {Count} items.", operation, count);
            }
            else
            {
                logger.LogInformation("Completed {Operation}.", operation);
            }
        }
        if (context.Result is NoContentResult noContentResult)
        {
            logger.LogInformation("Completed {Operation}.", operation);
        }

        if (context.Exception != null)
        {
            logger.LogError(context.Exception, "Failed to execute {Operation}", operation);
            context.Result = new ObjectResult("Internal error occurred.")
            {
                StatusCode = 500
            };
        }
    }
}