using Application.Abstractions;
using Domain.BankAccounts;
using Domain.ScheduledBankAccountCreations;
using Microsoft.Extensions.Logging;

namespace Application.BankAccountCreation;

public static class ExecuteReadyForCreationBankAccounts
{
    public class Handler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IScheduledBankAccountCreationRepository _scheduledBankAccountCreationRepository;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IUnitOfWork unitOfWork,
            IScheduledBankAccountCreationRepository scheduledBankAccountCreationRepository,
            IBankAccountRepository bankAccountRepository, ILogger<Handler> logger)
        {
            _unitOfWork = unitOfWork;
            _scheduledBankAccountCreationRepository = scheduledBankAccountCreationRepository;
            _bankAccountRepository = bankAccountRepository;
            _logger = logger;
        }

        public async Task HandleAsync(CancellationToken cancellationToken)
        {
            var scheduledCreations =
                await _scheduledBankAccountCreationRepository.GetReadyForCreationAsync(cancellationToken);
            _logger.LogInformation("Creating {Number} Accounts", scheduledCreations.Count);

            foreach (var scheduled in scheduledCreations)
            {
                var newBankAccount = new BankAccount(
                    scheduled.UserId,
                    scheduled.Currency);

                _bankAccountRepository.Add(newBankAccount);
                _scheduledBankAccountCreationRepository.Remove(scheduled);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
