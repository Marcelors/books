using Books.Domain.Entities;
using Books.Domain.Shared.Parameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.Infra.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Password).IsRequired().HasMaxLength(DomainParameters.MaxLenghtOfFifty);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(DomainParameters.MaxLenghtOfTwoHundred);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(DomainParameters.MaxLenghtOfTwoHundred);
        }
    }
}
