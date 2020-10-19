using System;
using System.Collections.Generic;
using System.Linq;
using Books.Domain.Entities;
using Books.Domain.Interfaces;
using Books.Domain.Interfaces.Repositores;
using Books.Domain.Shared.Extensions;
using Books.Domain.Shared.Models;
using Books.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Books.Infra.Data.Repositories
{
    public class FavoriteBookRepository : RepositoryBase<FavoriteBook>, IFavoriteBookRepository
    {
        private readonly IRequestScope _requestScope;

        public FavoriteBookRepository(BookContext context, IRequestScope requestScope) : base(context)
        {
            _requestScope = requestScope;
        }

        public override (int totalItems, IList<FavoriteBook> entities) Get(Filter filter)
        {
            var user = _requestScope.GetUser();
            var query = GetAll();
            var totalItems = 0;

            if (filter.Search.HasValue())
            {
                query = query.Where(x => x.Title.ToLower().Contains(filter.Search.ToLower()));
            }

            if (user != null)
            {
                query = query.Where(x => x.UserId == user.Id);
            }

            if (filter.TotalItems.HasValue)
            {
                totalItems = filter.TotalItems.Value;
            }
            else
            {
                totalItems = query.Count();
            }

            query = query.Paginate(filter.CurrentPage, filter.ItemsPerPage);

            query = query.Include(x => x.User);

            var entities = query.ToList();

            return (totalItems: totalItems, entities: entities);
        }
    }
}
