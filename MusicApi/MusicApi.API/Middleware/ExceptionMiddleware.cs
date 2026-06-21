using MusicApi.MusicApi.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace MusicApi.MusicApi.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("Response has already started, rethrowing exception: {Message}", ex.Message);
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status404NotFound;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
            catch (AlreadyExistsException ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("Response has already started, rethrowing exception: {Message}", ex.Message);
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status409Conflict;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }

            catch (DomainException ex)
            {
                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("Response has already started, rethrowing exception: {Message}", ex.Message);
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception caught by middleware");

                if (context.Response.HasStarted)
                {
                    _logger.LogWarning("Response has already started, rethrowing exception: {Message}", ex.Message);
                    throw;
                }

                context.Response.Clear();
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var payload = new { error = "An error occurred" };
                await context.Response.WriteAsJsonAsync(payload, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
        }
    }
}
