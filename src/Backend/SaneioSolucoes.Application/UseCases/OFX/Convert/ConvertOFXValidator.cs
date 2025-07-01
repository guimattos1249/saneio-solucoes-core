using FluentValidation;
using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Exceptions;

namespace SaneioSolucoes.Application.UseCases.OFX.Convert
{
    public class ConvertOFXValidator : AbstractValidator<RequestOFXFileConverter>
    {
        public ConvertOFXValidator()
        {
            RuleFor(file => file.File).NotNull().WithMessage(ResourceMessageExceptions.FILE_NEEDS_TO_BE_SENT);
        }
    }
}
