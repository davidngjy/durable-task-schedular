using Application.BankAccountCreation;
using Application.BankAccounts;
using Application.ReadModels;
using Domain.Abstractions;
using DurableTaskSchedular.Web.Requests;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace DurableTaskSchedular.Web.Endpoints;

public static class BankAccountEndpoints
{
    public static void MapBankAccountEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        var routerGroup = routeBuilder
            .MapGroup("/api/v1/bank-accounts")
            .WithTags("bank-accounts");

        routerGroup
            .MapGet("/", GetBankAccountsAsync)
            .WithName(nameof(GetBankAccountsAsync));

        routerGroup
            .MapPost("/", CreateBankAccountAsync)
            .WithName(nameof(CreateBankAccountAsync));

        routerGroup
            .MapPost("/schedule", ScheduleCreateBankAccountAsync)
            .WithName(nameof(ScheduleCreateBankAccountAsync));

        routerGroup
            .MapPost("/{bank_account_id:guid}/deposit", DepositMoneyAsync)
            .WithName(nameof(DepositMoneyAsync));

        routerGroup
            .MapPost("/{bank_account_id:guid}/schedule-bank-transfer", ScheduleTransferAsync)
            .WithName(nameof(ScheduleTransferAsync));
    }

    public static async Task<Ok<IEnumerable<BankAccount>>> GetBankAccountsAsync(
        [FromServices] GetBankAccounts.Handler handler,
        CancellationToken cancellationToken)
    {
        var bankAccounts = await handler.HandleAsync(cancellationToken);

        return TypedResults.Ok(bankAccounts);
    }

    public static async Task<Created> CreateBankAccountAsync(
        [FromServices] CreateBankAccount.Handler handler,
        [FromBody] CreateNewBankAccount request,
        CancellationToken cancellationToken)
    {
        var command = new CreateBankAccount.Command
        {
            UserId = request.UserId,
            Currency = request.Currency
        };

        await handler.HandleAsync(command, cancellationToken);

        return TypedResults.Created();
    }

    public static async Task<Created> ScheduleCreateBankAccountAsync(
        [FromServices] ScheduleBankAccountCreation.Handler handler,
        [FromBody] ScheduleCreateNewBankAccount request,
        CancellationToken cancellationToken)
    {
        var command = new ScheduleBankAccountCreation.Command
        {
            UserId = request.UserId,
            Currency = request.Currency,
            CreationDateTime = request.ScheduleDateTime
        };

        await handler.HandleAsync(command, cancellationToken);

        return TypedResults.Created();
    }

    public static async Task<NoContent> DepositMoneyAsync(
        [FromServices] DepositMoney.Handler handler,
        [FromRoute(Name = "bank_account_id")] Guid bankAccountId,
        [FromBody] NewDeposit request,
        CancellationToken cancellationToken)
    {
        await handler.HandleAsync(new DepositMoney.Command
        {
            BankAccountId = bankAccountId,
            Amount = request.Amount
        }, cancellationToken);

        return TypedResults.NoContent();
    }

    public static async Task<Results<NoContent, ProblemHttpResult>> ScheduleTransferAsync(
        [FromServices] ScheduleBankTransfer.Handler handler,
        [FromRoute(Name = "bank_account_id")] Guid bankAccountId,
        [FromBody] ScheduleNewBankTransfer request,
        CancellationToken cancellationToken)
    {
        var command = new ScheduleBankTransfer.Command
        {
            FromBankAccountId = bankAccountId,
            ToBankAccountId = request.ToBankAccountId,
            Amount = request.Amount,
            ScheduleDateTime = request.ScheduleDateTime
        };

        var result = await handler.HandleAsync(command, cancellationToken);

        return result switch
        {
            SuccessfulResult _ => TypedResults.NoContent(),
            FailureResult failureResult => failureResult.MapToHttpResult(),
            _ => throw new ArgumentOutOfRangeException(nameof(result))
        };
    }
}
