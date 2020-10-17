using Books.Infra.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Books.Infra.Data.Context
{
    public class BookContext : DbContext
    {
        public BookContext()
        {
        }

        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new FavoriteBookMapping());   
        }
    }
}
