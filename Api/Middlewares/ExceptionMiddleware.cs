using Api.Errors;
using System.Net;
using System.Text.Json;

namespace Api.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IHostEnvironment _host;
        public ExceptionMiddleware(RequestDelegate request, ILogger logger,IHostEnvironment hostEnvironment)
        {
            _next = request;
            _logger = logger;
            _host = hostEnvironment;
        }
        public async Task InvokeAsync(HttpContext context) {

            try
            {
                await _next(context);
            }
            catch (Exception ex) {

                _logger.LogError(ex, ex.Message);
                context.Response.ContentType = "Application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = _host.IsDevelopment() ? new ApiResponseDetail((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString()) :
                    new ApiResponse((int)HttpStatusCode.InternalServerError, ex.Message);

                var opt = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };
                var json = JsonSerializer.Serialize(response, opt);
                await context.Response.WriteAsync(json);
            }
        
        }
    }
}
