using System;
using System.Collections.Generic;
using AutoMapper;
using Books.ApplicationService.Inferfaces;
using Books.ApplicationService.Model;
using Books.Domain.DTO;
using Books.Domain.Interfaces.Repositores;
using Books.Domain.Interfaces.Services;
using Books.Domain.Shared.Enums;
using Books.Domain.Shared.Extensions;
using Books.Domain.Shared.Models;

namespace Books.ApplicationService.Application
{
    public class UserApplicationService : IUserApplicationService
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public UserApplicationService(IMapper mapper, IUserService userService, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userService = userService;
            _userRepository = userRepository;
        }

        public void Add(UserModel model)
        {
            var dto = _mapper.Map<UserDto>(model);
            _userService.Add(dto);
        }

        public void Delete(Guid id)
        {
            _userService.Delete(id);
        }

        public void Disable(Guid id)
        {
            _userService.Disable(id);
        }

        public void Enable(Guid id)
        {
            _userService.Enable(id);
        }

        public PaginedModel<UserModel> Get(Filter filter)
        {
            var (totalItems, entities) = _userRepository.Get(filter);
            return new PaginedModel<UserModel>(totalItems, _mapper.Map<IList<UserModel>>(entities));
        }

        public UserModel GetById(Guid id)
        {
            var entity = _userRepository.GetById(id);
            return _mapper.Map<UserModel>(entity);
        }

        public IList<EnumModel<short>> GetTypes()
        {
            var list = EnumExtension.EnumToModel<short, ProfileType>();
            return list;
        }

        public void Register(UserModel model)
        {
            var dto = _mapper.Map<UserDto>(model);
            dto.Profile = ProfileType.Standard;
            _userService.Add(dto);
        }

        public void Update(Guid id, UserModel model)
        {
            var dto = _mapper.Map<UserDto>(model);
            _userService.Update(id, dto);
        }
    }
}
