using System;
using System.Collections.Generic;
using Books.ApplicationService.Model;
using Books.Domain.Shared.Models;

namespace Books.ApplicationService.Inferfaces
{
    public interface IUserApplicationService
    {
        void Add(UserModel model);
        void Register(UserModel model);
        void Update(Guid id, UserModel model);
        void Delete(Guid id);
        void Enable(Guid id);
        void Disable(Guid id);
        UserModel GetById(Guid id);
        PaginedModel<UserModel> Get(Filter filter);
        IList<EnumModel<short>> GetTypes();
    }
}
