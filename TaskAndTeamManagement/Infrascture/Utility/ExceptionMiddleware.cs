using System.Net;
using System.Text.Json;

namespace TaskAndTeamManagement.Infrascture.Utility
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env)
        {
            _env = env;
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "application/json";

                // Check if it's a foreign key constraint violation
                if (ExceptionHandler.IsForeignKeyConstraintViolation(ex))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest; // 400 - Bad Request

                    //var response = new ApiException(
                    //    (int)HttpStatusCode.BadRequest,
                    //    "This record is referenced by another entity and cannot be deleted."
                    //);

                    var response = new ApiException(
                        (int)HttpStatusCode.BadRequest,
                        $"reference error. {ex.Message} Inner - {ex.InnerException.Message}"
                    );

                    var json = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                    await context.Response.WriteAsync(json);
                    return;
                }

                // Generic error handling
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var errorResponse = _env.IsDevelopment()
                    ? new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
                    : new ApiException((int)HttpStatusCode.InternalServerError, $"An unexpected error occurred. {ex.Message}");

                var errorJson = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                await context.Response.WriteAsync(errorJson);
            }
        }
    }
}
