using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagementAPI.Exception_Handling
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger):IExceptionHandler
    {
         private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    /// <summary>
    /// Invokes the exception handler with the specified <see cref="HttpContext"/>, <see cref="Exception"/>, and <see cref="CancellationToken"/>.
    /// </summary>
    /// <param name="httpContext">The HTTP httpContext.</param>
    /// <param name="exception">The exception.</param>
    /// <param name="cancellationToken">The cancelltaion token.</param>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        //TODO: Improve exception logging for better identification of exceptions in Datadog.
        _logger.LogError(exception, "Exception occured: {Message}", exception.Message);

        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Title = "Internal Server Error",
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken: cancellationToken);

        return true;
    }
    }
}