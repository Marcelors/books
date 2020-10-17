using System;
namespace Books.Domain.Shared.Models
{
    public class Entity
    {
        public Guid Id { get; protected set; }

        public void SetId(Guid id)
        {
            Id = id;
        }

        public void WithId()
        {
            Id = Guid.NewGuid();
        }
    }
}
