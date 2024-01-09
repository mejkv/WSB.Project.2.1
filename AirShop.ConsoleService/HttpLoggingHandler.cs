using Microsoft.Extensions.Logging;

namespace AirShop.AirService
{
    public class HttpLoggingHandler : DelegatingHandler
    {
        private readonly ILogger _logger;

        public HttpLoggingHandler(HttpMessageHandler innerHandler, ILogger logger)
            : base(innerHandler)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // Log Request
            _logger.LogInformation($"HTTP Request: {request.Method} {request.RequestUri}");

            if (request.Content != null)
            {
                var requestContent = await request.Content.ReadAsStringAsync();
                _logger.LogInformation($"Request Content: {requestContent}");
            }

            // Call the inner handler
            var response = await base.SendAsync(request, cancellationToken);

            // Log Response
            _logger.LogInformation($"HTTP Response: {response.StatusCode}");

            if (response.Content != null)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                _logger.LogInformation($"Response Content: {responseContent}");
            }

            return response;
        }
    }
}