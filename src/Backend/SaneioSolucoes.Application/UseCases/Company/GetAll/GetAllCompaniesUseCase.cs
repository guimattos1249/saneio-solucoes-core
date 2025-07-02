using AutoMapper;
using SaneioSolucoes.Communication.Responses;
using SaneioSolucoes.Domain.Repositories.Company;
using SaneioSolucoes.Domain.Services.LoggedUser;

namespace SaneioSolucoes.Application.UseCases.Company.GetAll
{
    public class GetAllCompaniesUseCase : IGetAllCompaniesUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly ICompanyReadOnlyRepository _repository;

        public GetAllCompaniesUseCase(IMapper mapper, ILoggedUser loggedUser, ICompanyReadOnlyRepository repository)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
        }

        public async Task<ResponseCompaniesJson> Execute()
        {
            var loggedUser = await _loggedUser.User();

            var companies = await _repository.GetAll(loggedUser.TenantId);

            return new ResponseCompaniesJson
            {
                Companies = _mapper.Map<List<ResponseCompanyJson>>(companies),
            };
        }
    }
}
