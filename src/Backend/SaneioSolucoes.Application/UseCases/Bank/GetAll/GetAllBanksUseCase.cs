using AutoMapper;
using SaneioSolucoes.Communication.Responses;
using SaneioSolucoes.Domain.Repositories.Bank;
using SaneioSolucoes.Domain.Services.LoggedUser;

namespace SaneioSolucoes.Application.UseCases.Bank.GetAll
{
    public class GetAllBanksUseCase : IGetAllBanksUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly IBankReadOnlyRepository _repository;

        public GetAllBanksUseCase(IMapper mapper, ILoggedUser loggedUser, IBankReadOnlyRepository repository)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
        }

        public async Task<ResponseBanksJson> Execute()
        {
            var loggedUser = await _loggedUser.User();

            var banks = await _repository.GetAll(loggedUser.TenantId);

            return new ResponseBanksJson
            {
                Banks = _mapper.Map<List<ResponseRegisteredBankJson>>(banks),
            };
        }
    }
}
