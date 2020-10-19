using Books.ApplicationService.Inferfaces;
using Books.ApplicationService.Model;
using Books.Domain.Shared.Nofication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    [Route("v1/authenticate")]
    [ApiController]
    public class AuthenticateController : ApiController
    {
        private readonly IAuthenticateApplicationService _authenticateApplicationService;

        public AuthenticateController(IMediator bus, INotificationHandler<DomainNotification> notifications, IAuthenticateApplicationService authenticateApplicationService) : base(bus, notifications)
        {
            _authenticateApplicationService = authenticateApplicationService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Add([FromBody] AuthenticateModel model)
        {
            var result = _authenticateApplicationService.Authenticate(model);
            return Response(result);
        }
    }
}
