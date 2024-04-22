using Domain.Abstractions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DurableTaskSchedular.Web.Endpoints;

public static class HttpFailureResultFactory
{
    public static ProblemHttpResult MapToHttpResult(this FailureResult failureResult)
    {
        var statusCode = failureResult.Type switch
        {
            FailureResult.FailureResultType.NotFound => StatusCodes.Status404NotFound,
            FailureResult.FailureResultType.Conflicted => StatusCodes.Status409Conflict,
            FailureResult.FailureResultType.Validation => StatusCodes.Status422UnprocessableEntity,
            _ => throw new ArgumentOutOfRangeException()
        };

        var extensions = failureResult
            .Detail
            .ToDictionary(
                x => x.Key,
                y => (object?)y.Value);

        return TypedResults.Problem(
            title: failureResult.Title,
            detail: failureResult.Description,
            statusCode: statusCode,
            type: failureResult.ErrorCode,
            extensions: extensions);

        /*
         {
             "type": "https://bookstore.example.com/problems/book-not-found",
             "title": "Book Not Found",
             "status": 404,
             "detail": "The book with ID 12345 could not be found in our database.",
             "instance": "/api/books/12345",
             "timestamp": "2023-11-28T12:34:56Z",
             "custom-field": "Additional information or context here"
           }
         */
    }
}
