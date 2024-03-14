using System.Text.Json;

namespace Voam.Server.Common
{
    public class AuthorizationResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthorizationResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                if (context.Request.Path == "/api/Auth/Login")
                {
                    return;
                }

                // Reset the response
                context.Response.ContentType = "application/json";
                //context.Response.Body.SetLength(0); // Requires a rewindable body or buffering enabled

                // Write the custom content
                await context.Response.WriteAsync(JsonSerializer.Serialize(new
                {
                    Error = "Unauthorized"
                }));
            }
        }
    }
}
