using AutoMapper;
using SaneioSolucoes.Communication.Responses;
using SaneioSolucoes.Domain.Repositories.Company;
using SaneioSolucoes.Domain.Services.LoggedUser;
using SaneioSolucoes.Exceptions.ExceptionBase;
using SaneioSolucoes.Exceptions;

namespace SaneioSolucoes.Application.UseCases.Company.GetById
{
    public class GetCompanyByIdUseCase : IGetCompanyByIdUseCase
    {
        private readonly IMapper _mapper;
        private readonly ILoggedUser _loggedUser;
        private readonly ICompanyReadOnlyRepository _repository;

        public GetCompanyByIdUseCase(IMapper mapper, ILoggedUser loggedUser, ICompanyReadOnlyRepository repository)
        {
            _mapper = mapper;
            _loggedUser = loggedUser;
            _repository = repository;
        }

        public async Task<ResponseCompanyJson> Execute(Guid id)
        {
            var user = await _loggedUser.User();

            var company = await _repository.GetById(user.TenantId, id);

            if (company is null)
                throw new NotFoundException(ResourceMessageExceptions.COMPANY_NOT_FOUND);

            return _mapper.Map<ResponseCompanyJson>(company);
        }
    }
}
