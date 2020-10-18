using System.Collections.Generic;

namespace Books.Domain.DTO
{
    public class BookResultDto
    {
        public int TotalItems { get; set; }
        public IList<BookDto> Items { get; set; } = new List<BookDto>();
    }
}
