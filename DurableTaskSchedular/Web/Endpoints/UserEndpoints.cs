using Application.ReadModels;
using Application.Users;
using DurableTaskSchedular.Web.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DurableTaskSchedular.Web.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        var userRouteGroup = routeBuilder
            .MapGroup("/api/users")
            .WithTags("Users");

        userRouteGroup
            .MapGet("/", GetUsersAsync)
            .WithName(nameof(GetUsersAsync));

        userRouteGroup
            .MapPost("/", CreateUserAsync)
            .WithName(nameof(CreateUserAsync));
    }

    public static async Task<Ok<IEnumerable<User>>> GetUsersAsync(
        [FromServices] GetUsers.Handler handler,
        CancellationToken cancellationToken)
    {
        var users = await handler.HandleAsync(cancellationToken);

        return TypedResults.Ok(users);
    }

    public static async Task<Created> CreateUserAsync(
        [FromServices] CreateUser.Handler handler,
        [FromBody] CreateNewUser request,
        CancellationToken cancellationToken)
    {
        await handler
            .HandleAsync(new CreateUser.Command
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            }, cancellationToken);

        return TypedResults.Created();
    }
}
