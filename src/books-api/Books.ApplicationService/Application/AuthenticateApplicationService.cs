using AutoMapper;
using Books.ApplicationService.Inferfaces;
using Books.ApplicationService.Model;
using Books.Domain.DTO;
using Books.Domain.Interfaces.Services;

namespace Books.ApplicationService.Application
{
    public class AuthenticateApplicationService : IAuthenticateApplicationService
    {
        private readonly IMapper _mapper;
        private readonly IAuthenticateService _authenticateService;

        public AuthenticateApplicationService(IAuthenticateService authenticateService, IMapper mapper)
        {
            _authenticateService = authenticateService;
            _mapper = mapper;
        }

        public UserResultModel Authenticate(AuthenticateModel model)
        {
            var dto = _mapper.Map<AuthenticateDto>(model);
            var result = _authenticateService.Authenticate(dto);

            return new UserResultModel
            {
                Token = result.token,
                User = _mapper.Map<UserModel>(result.user)
            };
        }
    }
}
