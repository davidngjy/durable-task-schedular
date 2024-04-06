using Domain.ScheduledBankAccountCreations;

namespace Application.BankAccountCreation;

public static class GetScheduledBankAccountCreations
{
    public class Handler
    {
        private readonly IScheduledBankAccountCreationRepository _scheduledBankAccountCreationRepository;

        public Handler(IScheduledBankAccountCreationRepository scheduledBankAccountCreationRepository)
        {
            _scheduledBankAccountCreationRepository = scheduledBankAccountCreationRepository;
        }

        public async Task<IEnumerable<ReadModels.ScheduledBankAccountCreation>> HandleAsync(CancellationToken cancellationToken)
        {
            var scheduledCreations = await _scheduledBankAccountCreationRepository.GetAllAsync(cancellationToken);

            return scheduledCreations.Select(c => new ReadModels.ScheduledBankAccountCreation
            {
                Id = c.Id.Value,
                UserId = c.UserId.Value,
                Currency = c.Currency.Code,
                ScheduledDateTime = c.ScheduleDateTime
            });
        }
    }
}
