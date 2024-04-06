using Application.BankAccountCreation;

namespace DurableTaskSchedular.Web.BackgroundServices;

public class ScheduledBankAccountCreationProcessor : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ScheduledBankAccountCreationProcessor(IServiceScopeFactory serviceScopeFactory)
        => _serviceScopeFactory = serviceScopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

        do
        {
            await using var scope = _serviceScopeFactory.CreateAsyncScope();

            var handler = scope
                .ServiceProvider
                .GetRequiredService<ExecuteReadyForCreationBankAccounts.Handler>();

            await handler.HandleAsync(stoppingToken);
        } while (await timer.WaitForNextTickAsync(stoppingToken));
    }
}
