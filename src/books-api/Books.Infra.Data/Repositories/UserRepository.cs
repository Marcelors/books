using System;
using System.Collections.Generic;
using System.Linq;
using Books.Domain.Entities;
using Books.Domain.Interfaces.Repositores;
using Books.Domain.Shared.Extensions;
using Books.Domain.Shared.Models;
using Books.Infra.Data.Context;

namespace Books.Infra.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(BookContext context) : base(context)
        {
        }

        public override (int totalItems, IList<User> entities) Get(Filter filter)
        {
            var query = GetAll();
            var totalItems = 0;

            if (filter.Search.HasValue())
            {
                query = query.Where(x => x.Name.ToLower().StartsWith(filter.Search.ToLower()) || x.Email.ToLower().StartsWith(filter.Search.ToLower()));
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

            var entities = query.ToList();

            return (totalItems: totalItems, entities: entities);
        }

        public User GetByEmail(string email)
        {
            var query = GetAll();

            return query.FirstOrDefault(x => x.Email == email);
        }
    }
}
