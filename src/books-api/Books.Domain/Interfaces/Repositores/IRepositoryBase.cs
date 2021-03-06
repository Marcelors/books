﻿using System;
using System.Collections.Generic;
using Books.Domain.Shared.Models;

namespace Books.Domain.Interfaces.Repositores
{
    public interface IRepositoryBase<T> : IDisposable
        where T : Entity
    {
        void Add(T obj);
        T GetById(Guid id);
        void Update(T obj);
        void Remove(Guid id);
        (int totalItems, IList<T> entities) Get(Filter filter);
    }
}
