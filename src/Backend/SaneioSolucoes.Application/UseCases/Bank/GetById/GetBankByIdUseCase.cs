using AutoMapper;
using SaneioSolucoes.Communication.Responses;
using SaneioSolucoes.Domain.Repositories.Bank;
using SaneioSolucoes.Domain.Services.LoggedUser;
using SaneioSolucoes.Exceptions.ExceptionBase;
using SaneioSolucoes.Exceptions;

namespace SaneioSolucoes.Application.UseCases.Bank.GetById
{
    public class GetBankByIdUseCase : IGetBankByIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly IBankReadOnlyRepository _repository;

        public GetBankByIdUseCase(IMapper mapper, ILoggedUser loggedUser, IBankReadOnlyRepository repository)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
        }

        public async Task<ResponseRegisteredBankJson> Execute(Guid id)
        {
            var user = await _loggedUser.User();

            var bank = await _repository.GetById(user.TenantId, id);

            if (bank is null)
                throw new NotFoundException(ResourceMessageExceptions.BANK_NOT_FOUND);

            return _mapper.Map<ResponseRegisteredBankJson>(bank);
        }
    }
}
