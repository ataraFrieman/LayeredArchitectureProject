using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PublicInquiriesAPI.Utils.Exceptions;
using System;
using System.Net;
using System.Threading.Tasks;

namespace PublicInquiriesAPI.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
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

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // הגדרת ברירת מחדל
            var statusCode = HttpStatusCode.InternalServerError;
            var errorResponse = new ErrorResponse
            {
                StatusCode = (int)statusCode,
                Message = "An unexpected error occurred."
            };

            switch (exception)
            {
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    errorResponse.Message = notFoundException.Message;
                    break;
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    errorResponse.Message = validationException.Message;
                    break;
                case UnauthorizedException unauthorizedException:
                    statusCode = HttpStatusCode.Unauthorized;
                    errorResponse.Message = unauthorizedException.Message;
                    break;
                default:
                    Console.WriteLine($"Unhandled exception: {exception.Message}");
                    break;
            }

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            var response = JsonConvert.SerializeObject(errorResponse);
            return context.Response.WriteAsync(response);
        }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
    }
}
