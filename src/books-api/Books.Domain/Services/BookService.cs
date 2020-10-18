using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Books.Domain.DTO;
using Books.Domain.Interfaces;
using Books.Domain.Interfaces.Services;
using Books.Domain.Shared.Models;
using Books.Domain.Shared.Nofication;
using Books.Domain.Shared.Resources;
using MediatR;
using Newtonsoft.Json;

namespace Books.Domain.Services
{
    public class BookService : Service, IBookService
    {
        private const string resource = "www.googleapis.com/books/v1/volumes";

        protected BookService(IUnitOfWork uow, IMediator bus, INotificationHandler<DomainNotification> notifications) : base(uow, bus, notifications)
        {
        }

        public async Task<(int totalItems, IList<BookDto> books)> Get(Filter filter)
        {
            if (string.IsNullOrWhiteSpace(filter.Search))
            {
                NotifyError(DomainError.searchIsRequired);
                return (0, new List<BookDto>());
            }

            if(filter.Search.Length < 3)
            {
                NotifyError(string.Format(DomainError.SearchMustBeAtLeastCharacters, 3));
                return (0, new List<BookDto>());
            }

            if (!filter.ItemsPerPage.HasValue)
            {
                NotifyError(DomainError.itemsPerPageIsRequired);
                return (0, new List<BookDto>());
            }

            if(filter.ItemsPerPage.Value > 40)
            {
                NotifyError(string.Format(DomainError.TheValueCannotBeGreaterThan, 40));
                return (0, new List<BookDto>());
            }

            if (!filter.CurrentPage.HasValue)
            {
                NotifyError(DomainError.currentPageIsRequired);
                return (0, new List<BookDto>());
            }


            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(resource);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    var query = $"?q={filter.Search}&startIndex={filter.CurrentPage}&maxResults={filter.ItemsPerPage}";

                    HttpResponseMessage response = await client.GetAsync(query);
                    if (!response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        if (string.IsNullOrWhiteSpace(content))
                        {
                            return (0, new List<BookDto>());
                        }

                        var result = JsonConvert.DeserializeObject<BookResultDto>(content);

                        return (result.TotalItems, result.Items);
                    }
                }
            }
            catch(Exception ex)
            {
                NotifyError(ex.Message);
                return (0, new List<BookDto>());
            }

            return(0, new List<BookDto>());
        }

        public async Task<BookDto> GetById(string id)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(resource);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    HttpResponseMessage response = await client.GetAsync($"/{id}");
                    if (!response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        if (string.IsNullOrWhiteSpace(content))
                        {
                            return null;
                        }

                        var result = JsonConvert.DeserializeObject<BookDto>(content);

                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                NotifyError(ex.Message);
                return null;
            }

            return null;
        }
    }
}
