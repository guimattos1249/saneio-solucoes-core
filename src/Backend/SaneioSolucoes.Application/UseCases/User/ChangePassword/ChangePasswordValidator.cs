using FluentValidation;
using SaneioSolucoes.Application.SharedValidators;
using SaneioSolucoes.Communication.Requests;

namespace SaneioSolucoes.Application.UseCases.User.ChangePassword
{
    public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordJson>
    {
        public ChangePasswordValidator()
        {
            RuleFor(x => x.NewPassword).SetValidator(new PasswordValidator<RequestChangePasswordJson>());
        }
    }
}
