using System.Text.Json;
using Domain.Abstractions;
using Microsoft.Extensions.Logging;

namespace Application.Extensions;

public static class LoggerExtensions
{
    public static void LogFailureResult(this ILogger logger, FailureResult result) =>
        logger.LogError("Failure in processing: " +
                        "Title: {Title}, " +
                        "ErrorCode: {ErrorCode}, " +
                        "Description: {Description}, " +
                        "Details: {Details}",
            result.Title,
            result.ErrorCode,
            result.Description,
            JsonSerializer.Serialize(result.Detail));
}
