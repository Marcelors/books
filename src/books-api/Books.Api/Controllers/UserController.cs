using System;
using Books.ApplicationService.Inferfaces;
using Books.ApplicationService.Model;
using Books.Domain.Shared.Models;
using Books.Domain.Shared.Nofication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    [Route("v1/users")]
    [ApiController]
    public class UserController : ApiController
    {
        private readonly IUserApplicationService _userApplicationService;

        public UserController(IMediator bus, INotificationHandler<DomainNotification> notifications, IUserApplicationService userApplicationService) : base(bus, notifications)
        {
            _userApplicationService = userApplicationService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Filter filter)
        {
            var result = _userApplicationService.Get(filter);
            return Response(result);
        }

        [HttpGet("types")]
        public IActionResult GetTypes()
        {
            var result = _userApplicationService.GetTypes();
            return Response(result);
        }

        [HttpGet("{id:guid}")]
        public IActionResult GetById(Guid id)
        {
            var result = _userApplicationService.GetById(id);
            return Response(result);
        }

        [HttpPost]
        public IActionResult Add([FromBody] UserModel model)
        {
            _userApplicationService.Add(model);
            return Response();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserModel model)
        {
            _userApplicationService.Register(model);
            return Response();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            _userApplicationService.Delete(id);
            return Response();
        }

        [HttpPost("{id:guid}")]
        public IActionResult Enable(Guid id)
        {
            _userApplicationService.Enable(id);
            return Response();
        }

        [HttpPost("{id:guid}")]
        public IActionResult Disable(Guid id)
        {
            _userApplicationService.Disable(id);
            return Response();
        }

        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] UserModel model)
        {
            _userApplicationService.Update(id, model);
            return Response();
        }
    }
}
