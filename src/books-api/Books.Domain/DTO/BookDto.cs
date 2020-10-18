using System.Collections.Generic;

namespace Books.Domain.DTO
{
    public class BookDto
    {
        public string Id { set; get; }
        public string SelfLink { get; set; }
        public VolumeInfoDto VolumeInfo { get; set; }
    }

    public class VolumeInfoDto
    {
        public string Title { get; set; }
        public IList<string> Authors { get; set; } = new List<string>();
        public string Publisher { get; set; }
        public string Description { get; set; }
        public ImageLinksDto ImageLinks { get; set; }

    }

    public class ImageLinksDto
    {
        public string Thumbnail { get; set; }
    }
}
