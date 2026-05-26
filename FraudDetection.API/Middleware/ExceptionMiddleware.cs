using System.Net;
using System.Text.Json;

namespace FraudDetection.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(
            RequestDelegate nextMiddleware
        )
        {
            next = nextMiddleware;
        }

        public async Task InvokeAsync(
            HttpContext context
        )
        {
            try
            {
                await next(context);
            }
            catch (ArgumentException ex)
            {
                await HandleException(
                    context,
                    HttpStatusCode.BadRequest,
                    ex.Message
                );
            }
            catch (UnauthorizedAccessException ex)
            {
                await HandleException(
                    context,
                    HttpStatusCode.Unauthorized,
                    ex.Message
                );
            }
            catch (Exception ex)
            {
                await HandleException(
                    context,
                    HttpStatusCode.InternalServerError,
                    ex.Message
                );
            }
        }

        private static async Task HandleException(
            HttpContext context,
            HttpStatusCode statusCode,
            string message
        )
        {
            context.Response.ContentType =
                "application/json";

            context.Response.StatusCode =
                (int)statusCode;

            var response = new
            {
                success = false,
                status = context.Response.StatusCode,
                error = message,
                timestamp = DateTime.UtcNow
            };

            string json =
                JsonSerializer.Serialize(
                    response
                );

            await context.Response.WriteAsync(
                json
            );
        }
    }
}