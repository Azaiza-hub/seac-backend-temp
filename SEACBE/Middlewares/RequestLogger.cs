using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace SEACBE.Middlewares
{
    public class RequestLogger
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLogger(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLogger>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                _logger.LogInformation(
                    "Request {method} {url} => {statusCode}",
                    context.Request?.Method,
                    context.Request?.Path.Value,
                    context.Response?.StatusCode);
            }
        }
    }
}