using System.Linq;
using System.Threading.Tasks;
using Books.ApplicationService.Inferfaces;
using Books.Domain.Shared.Models;
using Books.Domain.Shared.Resources;
using FluentAssertions;
using Xunit;

namespace Books.Test.Integration.ApplicationService
{
    public class BookApplicationServiceTest : TestIntegrationBase
    {
        private readonly IBookApplicationService _bookApplicationService;

        public BookApplicationServiceTest() : base()
        {
            CreateScope();

            _bookApplicationService = GetIntanceScope<IBookApplicationService>();
        }

        [Fact]
        public async Task BookApplicationService_Get_with_error_at_serach()
        {
            var filter = new Filter
            {
                Search = null,
                CurrentPage = 0,
                ItemsPerPage = 10
            };

            var model = await _bookApplicationService.Get(filter);


            DomainNotificationHandler.HasNotifications().Should().BeTrue();
            DomainNotificationHandler.GetNotifications.First().Value.Should().Be(DomainError.searchIsRequired);
            model.Items.Should().HaveCount(0);
            model.TotalItems.Should().Be(0);


            filter = new Filter
            {
                Search = "te",
                CurrentPage = 0,
                ItemsPerPage = 10
            };

            model = await _bookApplicationService.Get(filter);

            DomainNotificationHandler.HasNotifications().Should().BeTrue();
            DomainNotificationHandler.GetNotifications[1].Value.Should().Be(string.Format(DomainError.SearchMustBeAtLeastCharacters, 3));
            model.Items.Should().HaveCount(0);
            model.TotalItems.Should().Be(0);
        }

        [Fact]
        public async Task BookApplicationService_Get_with_error_at_itemsPerPage()
        {
            var filter = new Filter
            {
                Search = "carro",
                CurrentPage = 0,
                ItemsPerPage = null
            };

            var model = await _bookApplicationService.Get(filter);


            DomainNotificationHandler.HasNotifications().Should().BeTrue();
            DomainNotificationHandler.GetNotifications.First().Value.Should().Be(DomainError.itemsPerPageIsRequired);
            model.Items.Should().HaveCount(0);
            model.TotalItems.Should().Be(0);


            filter = new Filter
            {
                Search = "carro",
                CurrentPage = 0,
                ItemsPerPage = 50
            };

            model = await _bookApplicationService.Get(filter);

            DomainNotificationHandler.HasNotifications().Should().BeTrue();
            DomainNotificationHandler.GetNotifications[1].Value.Should().Be(string.Format(DomainError.TheValueCannotBeGreaterThan, 40));
            model.Items.Should().HaveCount(0);
            model.TotalItems.Should().Be(0);
        }

        [Fact]
        public async Task BookApplicationService_Get_with_error_at_currentPage()
        {
            var filter = new Filter
            {
                Search = "carro",
                CurrentPage = null,
                ItemsPerPage = 10
            };

            var model = await _bookApplicationService.Get(filter);


            DomainNotificationHandler.HasNotifications().Should().BeTrue();
            DomainNotificationHandler.GetNotifications.First().Value.Should().Be(DomainError.currentPageIsRequired);
            model.Items.Should().HaveCount(0);
            model.TotalItems.Should().Be(0);
        }


        [Fact]
        public async Task BookApplicationService_Get()
        {
            var filter = new Filter
            {
                Search = "carro",
                CurrentPage = 0,
                ItemsPerPage = 10
            };

            var model = await _bookApplicationService.Get(filter);

            model.Items.Should().HaveCount(10);
            model.TotalItems.Should().NotBe(0);
        }

        [Fact]
        public async Task BookApplicationService_GetById()
        {
            var id = "b8Z5DwAAQBAJ";

            var model = await _bookApplicationService.GetById(id);

            model.Should().NotBeNull();
        }
    }
}
