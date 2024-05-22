using HumanResourceManagement.Domain.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;
using System.Net;

namespace HumanResourceManagement.Api.Extensions;

public static class ExceptionMiddlewareExtensions
{
    public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            Domain.Exceptions.NotFoundException => StatusCodes.Status404NotFound,
                            Domain.Exceptions.NotFoundRequestException => StatusCodes.Status404NotFound,
                            Domain.Exceptions.BadRequestException => StatusCodes.Status400BadRequest,
                            Domain.Exceptions.ConflictRequestException => StatusCodes.Status409Conflict,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        //logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message,

                        }.ToString());
                    }
                });
            });
        }
}
