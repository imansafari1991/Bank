using Bank.API.Models;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace Bank.API.Middlewares
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        if (contextFeature.Error is ArgumentException) // Bad Request
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        }
                        else if (contextFeature.Error is KeyNotFoundException) // Not Found
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        }
                        else // Internal Server Error
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        }
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }.ToString());
                    }
                });
            });
        }
    }
}
