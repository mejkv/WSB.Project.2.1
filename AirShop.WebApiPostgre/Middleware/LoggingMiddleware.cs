using System.Net;

namespace AirShop.WebApiPostgre.Middleware
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation($"API started");

            // Log incoming request using NLog
            var request = await FormatRequest(context.Request);
            _logger.LogInformation($"Request {request}");

            // Continue to the next middleware in the pipeline
            await _next(context);

            // Log outgoing response using NLog
            //var response = await FormatResponse(context.Response);
            switch (context.Response.StatusCode)
            {
                case 200:
                    _logger.LogInformation($"Response code {context.Response.StatusCode}, {context.Response.HttpContext.Response.Body}");
                    break;
                case 500:
                    _logger.LogCritical($"Response code {context.Response.StatusCode}, {context.Response.HttpContext.Response.Body}");
                    break;
                case 502:
                    _logger.LogError($"Response code {context.Response.StatusCode}, {context.Response.HttpContext.Response.Body}");
                    break;
                case 404:
                    _logger.LogError($"Response code {context.Response.StatusCode}, {context.Response.HttpContext.Response.Body}");
                    break;
                default:
                    _logger.LogDebug($"Response code {context.Response.StatusCode}, {context.Response.HttpContext.Response.Body}");
                    break;
            }
            //_logger.LogInformation($"Response code {context.Response}");
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();
            var body = await new StreamReader(request.Body).ReadToEndAsync();
            request.Body.Seek(0, SeekOrigin.Begin);

            if (string.IsNullOrWhiteSpace(body))
                return $"method: {request.Method}";

            return $"method: {request.Method}, Request body: {body}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            
            var body = await new StreamReader(response.Body).ReadToEndAsync();

            return $"{response.StatusCode}: {body}";

        }
    }
}