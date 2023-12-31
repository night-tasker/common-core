﻿using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NightTasker.Common.Core.Exceptions.Base;
using NightTasker.Common.Core.Exceptions.Models;

namespace NightTasker.Common.Core.Exceptions.Middlewares;

/// <summary>
/// Middleware для глобальной обработки исключений.
/// </summary>
public class ExceptionHandlerMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleException(context, exception);
            logger.LogError(exception.ToString());
        }
    }

    /// <summary>
    /// Обработать исключение
    /// </summary>
    /// <param name="context">HTTP-контекст.</param>
    /// <param name="exception">Исключение.</param>
    private static Task HandleException(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var errorDetails = new ErrorDetails
        {
            TraceId = context.TraceIdentifier
        };

        context.Response.StatusCode = ResolveStatusCode(exception);
        errorDetails.Message = ErrorDetails.DefaultErrorMessage;
        errorDetails.DisplayMessage = ResolveDisplayMessage(exception);

        var jsonStringErrorDetails = JsonSerializer.Serialize(errorDetails, new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        
        return context.Response.WriteAsync(jsonStringErrorDetails);
    }

    /// <summary>
    /// Разрешить статус-код, исходя из исключения.
    /// </summary>
    /// <param name="exception">Исключение.</param>
    /// <returns>Код.</returns>
    private static int ResolveStatusCode(Exception exception)
    {
        return exception is IStatusCodeException statusCodeException
            ? statusCodeException.StatusCode
            : StatusCodes.Status500InternalServerError;
    }

    /// <summary>
    /// Разрешить сообщение для отображения, исходя из исключения.
    /// </summary>
    /// <param name="exception">Исключение.</param>
    /// <returns>Сообщение для отображения.</returns>
    private static string? ResolveDisplayMessage(Exception exception)
    {
        return exception is IPublicException publicException
            ? publicException.DisplayMessage
            : null;
    }
}