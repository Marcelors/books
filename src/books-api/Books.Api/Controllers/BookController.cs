using System.Threading.Tasks;
using Books.ApplicationService.Application;
using Books.Domain.Shared.Models;
using Books.Domain.Shared.Nofication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    [Route("v1/books")]
    [ApiController]
    public class BookController : ApiController
    {
        private readonly BookApplicationService _bookApplicationService;

        public BookController(IMediator bus, INotificationHandler<DomainNotification> notifications, BookApplicationService bookApplicationService) : base(bus, notifications)
        {
            _bookApplicationService = bookApplicationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Filter filter)
        {
            var result = await _bookApplicationService.Get(filter);
            return Response(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _bookApplicationService.GetById(id);
            return Response(result);
        }
    }
}
