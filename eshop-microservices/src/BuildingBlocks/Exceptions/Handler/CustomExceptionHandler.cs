using BuildingBlocks.Exeptions;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {

            (string Detail, string Title, int StatusCode) details = exception switch
            {
                ValidationException validationException1 => (exception.Message, "Validation Error", StatusCodes.Status400BadRequest),
                BadRequestException badRequestException => (exception.Message, "Bad Request", StatusCodes.Status400BadRequest),
                NotFoundException notFoundException => (exception.Message, "Not Found", StatusCodes.Status404NotFound),
                InternalServerException internalServerException => (exception.Message, "Internal Server Error", StatusCodes.Status500InternalServerError),
                _ => (exception.Message, "Error", StatusCodes.Status500InternalServerError)
            };

            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Detail = details.Detail,
                Status = details.StatusCode,
                Instance = httpContext.Request.Path
            };

            problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);
            if(exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("errors", validationException.Errors);
            }

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
