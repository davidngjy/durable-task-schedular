using Application.Abstractions;
using Application.Extensions;
using Domain.Abstractions;
using Domain.BankAccounts;
using Microsoft.Extensions.Logging;

namespace Application.BankAccounts;

public static class ExecuteReadyForBankTransfer
{
    public class Handler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ILogger<Handler> _logger;

        public Handler(IUnitOfWork unitOfWork, IBankAccountRepository bankAccountRepository, ILogger<Handler> logger)
        {
            _unitOfWork = unitOfWork;
            _bankAccountRepository = bankAccountRepository;
            _logger = logger;
        }

        public async Task HandleAsync(CancellationToken cancellationToken)
        {
            var accounts = await _bankAccountRepository.GetWithReadyForTransferAsync(cancellationToken);
            _logger.LogInformation("Processing {Number} transfers.", accounts.Count);

            foreach (var account in accounts)
            {
                var result = await account.ProcessScheduledTransferAsync(_bankAccountRepository, cancellationToken);
                if (result is FailureResult failureResult)
                    _logger.LogFailureResult(failureResult);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
