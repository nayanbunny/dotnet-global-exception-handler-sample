using System.Net;
using System.Text.Json;
using DotNet.Global.Exceptions.Handler.Sample.Library.Helpers;

namespace DotNet.Global.Exceptions.Handler.Sample.Api.Middleware
{
    /// <summary>
    /// Exception Handler Middleware to Request Delegate and handles Exceptions with Customizations.
    /// </summary>
    public class ExceptionHandlerMiddleware
	{
        /// <summary>
        /// Request Delegate field to invoke HTTP Context
        /// </summary>
        private readonly RequestDelegate _next;

        /// <summary>
        /// The Exception Handler Middleware Constructor
        /// </summary>
        /// <param name="next">The Request Delegate</param>
        public ExceptionHandlerMiddleware(RequestDelegate next) => _next = next;

        /// <summary>
        /// Invoke Method for the HttpContext
        /// </summary>
        /// <param name="context">The HTTP Context</param>
        /// <returns>Response</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                // Log Input Request

                // Passing call to Controller
                await _next(context);

                // Global Response Formatting Wrapper Handler
                // Log Output Response
            }
            catch (Exception ex)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                response.StatusCode = ex switch
                {
                    AppException => (int)HttpStatusCode.BadRequest, // Custom Application Exception
                    KeyNotFoundException => (int)HttpStatusCode.NotFound, // Not Found Exception
                    _ => (int)HttpStatusCode.InternalServerError,// UnHandled Exceptions
                };

                var result = JsonSerializer.Serialize(new { message = ex?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}

