using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using PublicInquiriesAPI.Utils.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PublicInquiriesAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case CustomException customException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await WriteResponseAsync(context, customException.Message);
                    break;

                case NotFoundException notFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    await WriteResponseAsync(context, notFoundException.Message);
                    break;

                case UnauthorizedException unauthorizedException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    await WriteResponseAsync(context, unauthorizedException.Message);
                    break;

                case ValidationException validationException:
                    context.Response.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    await WriteResponseAsync(context, validationException.Message);
                    break;

                case Microsoft.EntityFrameworkCore.DbUpdateException dbUpdateException:
                    context.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                    _logger.LogError(dbUpdateException, "Database update failed.");
                    await WriteResponseAsync(context, "Database error occurred. Please try again later.");
                    break;

                default:
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    _logger.LogError(exception, "An unexpected error occurred.");
                    await WriteResponseAsync(context, "An unexpected error occurred. Please try again later.");
                    break;
            }
        }

        private Task WriteResponseAsync(HttpContext context, string message)
        {
            return context.Response.WriteAsync(new
            {
                error = message,
                statusCode = context.Response.StatusCode
            }.ToString());
        }
    }
}
