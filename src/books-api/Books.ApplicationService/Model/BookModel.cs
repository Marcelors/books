using System.Collections.Generic;

namespace Books.ApplicationService.Model
{
    public class BookModel
    {
        public string Id { set; get; }
        public string SelfLink { get; set; }
        public VolumeInfoModel VolumeInfo { get; set; }
    }

    public class VolumeInfoModel
    {
        public string Title { get; set; }
        public IList<string> Authors { get; set; } = new List<string>();
        public string Publisher { get; set; }
        public string Description { get; set; }
        public ImageLinksModel ImageLinks { get; set; }

    }

    public class ImageLinksModel
    {
        public string Thumbnail { get; set; }
    }
}
