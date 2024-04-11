using Application.Abstractions;
using Domain.BankAccounts;
using Domain.ScheduledBankAccountCreations;
using Domain.Users;

namespace Application.BankAccountCreation;

public static class ScheduleBankAccountCreation
{
    public record Command
    {
        public required Guid UserId { get; init; }

        public required string Currency { get; init; }

        public required DateTimeOffset CreationDateTime { get; init; }
    }

    public class Handler
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IScheduledBankAccountCreationRepository _scheduledBankAccountCreationRepository;

        public Handler(
            IUnitOfWork unitOfWork,
            IScheduledBankAccountCreationRepository scheduledBankAccountCreationRepository,
            IUserRepository userRepository)
        {
            _unitOfWork = unitOfWork;
            _scheduledBankAccountCreationRepository = scheduledBankAccountCreationRepository;
            _userRepository = userRepository;
        }

        public async Task<ScheduledBankAccountCreationId> HandleAsync(Command command,
            CancellationToken cancellationToken)
        {
            var currency = Currency.TryFromCode(command.Currency);
            if (currency is null)
                throw new Exception($"Currency {command.Currency} is not supported.");

            var user = await _userRepository.GetByIdAsync(new UserId(command.UserId), cancellationToken);
            if (user is null)
                throw new Exception($"User Id {command.UserId} not found.");

            var newScheduledCreation = new ScheduledBankAccountCreation(
                command.CreationDateTime,
                user,
                currency);

            _scheduledBankAccountCreationRepository.Add(newScheduledCreation);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return newScheduledCreation.Id;
        }
    }
}
