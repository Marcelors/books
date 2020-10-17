using Books.Domain.Entities;
using Books.Domain.Shared.Parameters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Books.Infra.Data.Mappings
{
    public class FavoriteBookMapping : IEntityTypeConfiguration<FavoriteBook>
    {
        public void Configure(EntityTypeBuilder<FavoriteBook> builder)
        {
            builder.Property(x => x.Title).IsRequired().HasMaxLength(DomainParameters.MaxLenghtOfFourHundred);
            builder.Property(x => x.Thumbnail).HasMaxLength(DomainParameters.MaxLenghtOfOneThousand);
            builder.Property(x => x.BookId).IsRequired().HasMaxLength(DomainParameters.MaxLenghtOfFifty);
            builder.Property(x => x.Description).HasMaxLength(DomainParameters.MaxLenghtEightThousand);
            builder.Property(x => x.Authors).IsRequired().HasMaxLength(DomainParameters.MaxLenghtOfOneThousand);
            builder.Property(x => x.Link).IsRequired().HasMaxLength(DomainParameters.MaxLenghtOfOneThousand);

            builder.HasOne(x => x.User).WithMany(x => x.FavoriteBooks).HasForeignKey(x => x.UserId).IsRequired();

        }
    }
}
