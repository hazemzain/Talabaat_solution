using System.Text.Json;
using Talabaat.APIs.Errors;

namespace Talabaat.APIs.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";


                var response = _environment.IsDevelopment()
                    ? new ApiExceptionResponce(500, ex.Message, ex.StackTrace.ToString())
                    : new ApiExceptionResponce(500);

                var ResultJson=JsonSerializer.Serialize(response);
                

                await context.Response.WriteAsync(ResultJson);
            }
        }
    }
}
