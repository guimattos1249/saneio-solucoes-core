using FluentValidation;
using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Exceptions;

namespace SaneioSolucoes.Application.UseCases.Bank.Register
{
    public class RegisterBankValidator : AbstractValidator<RequestRegisterBankJson>
    {
        public RegisterBankValidator()
        {
            RuleFor(bank => bank.LegalName).NotEmpty().WithMessage(ResourceMessageExceptions.EMPTY_LEGAL_NAME);
            RuleFor(bank => bank.Code).NotEmpty().WithMessage(ResourceMessageExceptions.EMPTY_BANK_CODE);
        }

    }
}
