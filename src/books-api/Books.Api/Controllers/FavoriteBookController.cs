using System;
using System.Threading.Tasks;
using Books.ApplicationService.Inferfaces;
using Books.ApplicationService.Model;
using Books.Domain.Shared.Models;
using Books.Domain.Shared.Nofication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Books.Api.Controllers
{
    [Route("v1/favorite-books")]
    [ApiController]
    public class FavoriteBookController : ApiController
    {
        private readonly IFavoriteBookApplicationService _favoriteBookApplicationService;

        public FavoriteBookController(IMediator bus,
            INotificationHandler<DomainNotification> notifications,
            IFavoriteBookApplicationService favoriteBookApplicationService) : base(bus, notifications)
        {
            _favoriteBookApplicationService = favoriteBookApplicationService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] Filter filter)
        {
            var result = _favoriteBookApplicationService.Get(filter);
            return Response(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _favoriteBookApplicationService.GetById(id);
            return Response(result);
        }

        [HttpPost]
        public IActionResult Add([FromBody] FavoriteBookModel model)
        {
            _favoriteBookApplicationService.Add(model);
            return Response();
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            _favoriteBookApplicationService.Delete(id);
            return Response();
        }
    }
}
