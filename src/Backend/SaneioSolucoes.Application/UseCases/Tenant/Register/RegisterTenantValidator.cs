using FluentValidation;
using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Domain.Extensions;
using SaneioSolucoes.Exceptions;

namespace SaneioSolucoes.Application.UseCases.Tenant.Register
{
    public class RegisterTenantValidator : AbstractValidator<RequestRegisterTenantJson>
    {
        public RegisterTenantValidator()
        {
            RuleFor(tenant => tenant.TradeName).NotEmpty().WithMessage(ResourceMessageExceptions.TRADE_NAME_EMPTY);
            RuleFor(tenant => tenant.Description).NotEmpty().WithMessage(ResourceMessageExceptions.DESCRIPTION_EMPTY);
            RuleFor(tenant => tenant.Email).NotEmpty().WithMessage(ResourceMessageExceptions.EMPTY_EMAIL);
            RuleFor(tenant => tenant.DocumentNumber).NotEmpty().WithMessage(ResourceMessageExceptions.INVALID_DOCUMENT_NUMBER);
            RuleFor(tenant => tenant.Slug).NotEmpty().WithMessage(ResourceMessageExceptions.EMPTY_SLUG);
            RuleFor(tenant => tenant.Plan).IsInEnum().WithMessage(ResourceMessageExceptions.PLAN_NOT_SUPPORTED);
            When(tenant => tenant.Email.NotEmpty(), () =>
            {
                RuleFor(tenant => tenant.Email).EmailAddress().WithMessage(ResourceMessageExceptions.INVALID_EMAIL);
            });
        }
    }
}
