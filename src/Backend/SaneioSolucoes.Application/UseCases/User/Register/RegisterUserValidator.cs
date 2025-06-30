using FluentValidation;
using SaneioSolucoes.Application.SharedValidators;
using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Domain.Extensions;
using SaneioSolucoes.Exceptions;

namespace SaneioSolucoes.Application.UseCases.User.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestRegisterUserJson>
    {
        public RegisterUserValidator() 
        {
            RuleFor(user => user.Name).NotEmpty().WithMessage(ResourceMessageExceptions.EMPTY_NAME);
            RuleFor(user => user.Email).NotEmpty().WithMessage(ResourceMessageExceptions.EMPTY_EMAIL);
            RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestRegisterUserJson>());
            When(user => user.Email.NotEmpty(), () =>
            {
                RuleFor(user => user.Email).EmailAddress().WithMessage(ResourceMessageExceptions.INVALID_EMAIL);
            });
        }
    }
}
