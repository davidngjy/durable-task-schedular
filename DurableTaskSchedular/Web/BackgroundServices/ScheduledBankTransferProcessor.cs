using Application.BankAccounts;

namespace DurableTaskSchedular.Web.BackgroundServices;

public class ScheduledBankTransferProcessor : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public ScheduledBankTransferProcessor(IServiceScopeFactory serviceScopeFactory)
        => _serviceScopeFactory = serviceScopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var timer = new PeriodicTimer(TimeSpan.FromSeconds(1));

        do
        {
            await using var scope = _serviceScopeFactory.CreateAsyncScope();

            var handler = scope
                .ServiceProvider
                .GetRequiredService<ExecuteReadyForBankTransfer.Handler>();

            await handler.HandleAsync(stoppingToken);
        } while (await timer.WaitForNextTickAsync(stoppingToken));
    }
}
