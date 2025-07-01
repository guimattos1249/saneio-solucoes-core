using FluentValidation;
using SaneioSolucoes.Application.SharedValidators;
using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Exceptions;

namespace SaneioSolucoes.Application.UseCases.Company.Register
{
    public class RegisterCompanyValidator : AbstractValidator<RequestRegisterCompnay>
    {
        public RegisterCompanyValidator()
        {
            RuleFor(company => company.TradeName).NotEmpty().WithMessage(ResourceMessageExceptions.EMPTY_TRADE_NAME);
            RuleFor(company => company.LegalName).NotEmpty().WithMessage(ResourceMessageExceptions.EMPTY_LEGAL_NAME);
            RuleFor(company => company.Cnpj).NotEmpty().WithMessage(ResourceMessageExceptions.INVALID_DOCUMENT_NUMBER);
        }
    }
}
