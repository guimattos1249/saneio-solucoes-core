using SaneioSolucoes.Communication.Requests;
using SaneioSolucoes.Domain.Extensions;
using SaneioSolucoes.Domain.Repositories;
using SaneioSolucoes.Domain.Repositories.User;
using SaneioSolucoes.Domain.Security.Cryptography;
using SaneioSolucoes.Domain.Services.LoggedUser;
using SaneioSolucoes.Exceptions;
using SaneioSolucoes.Exceptions.ExceptionBase;

namespace SaneioSolucoes.Application.UseCases.User.ChangePassword
{
    public class ChangePasswordUseCase : IChangePasswordUseCase
    {
        private readonly ILoggedUser _loggedUser;
        private readonly IUserUpdateOnlyRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordEncripter _passwordEncripter;

        public ChangePasswordUseCase(
            ILoggedUser loggedUser,
            IUserUpdateOnlyRepository repository,
            IUnitOfWork unitOfWork,
            IPasswordEncripter passwordEncripter)
        {
            _loggedUser = loggedUser;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _passwordEncripter = passwordEncripter;
        }

        public async Task Execute(RequestChangePasswordJson request)
        {
            var loggedUser = await _loggedUser.User();

            Validate(request, loggedUser);

            var user = await _repository.GetById(loggedUser.Id, loggedUser.TenantId);

            user.Password = _passwordEncripter.Encrypt(request.NewPassword);

            _repository.Update(user);

            await _unitOfWork.Commit();
        }

        private void Validate(RequestChangePasswordJson request, Domain.Entities.User loggedUser)
        {
            var result = new ChangePasswordValidator().Validate(request);

            var currentPasswordEncripted = _passwordEncripter.Encrypt(request.Password);

            if (currentPasswordEncripted.Equals(loggedUser.Password).IsFalse())
                result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty, ResourceMessageExceptions.PASSWORD_DIFFRENT_CURRENT_PASSWORD));

            if (result.IsValid.IsFalse())
                throw new ErrorOnValidationException(result.Errors.Select(e => e.ErrorMessage).ToList());
        }
    }
}
