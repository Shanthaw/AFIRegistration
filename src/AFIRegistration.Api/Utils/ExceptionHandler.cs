using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AFIRegistration.Api.Utils
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandler> _logger;
        public ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (EntityException et)
            {
                await HandleExceptionAsync(context, et);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, EntityException ex)
        {
            _logger.LogError($"{ex.Message} - Trace {ex.StackTrace}");
            string result = JsonConvert.SerializeObject(Envelope.Error(ex.Message));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return context.Response.WriteAsync(result);
        }
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError($"{ex.Message} - Trace {ex.StackTrace}");
            string result = JsonConvert.SerializeObject(Envelope.Error(ex.Message));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            return context.Response.WriteAsync(result);
        }
    }
}
