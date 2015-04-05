using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using ModernHttpClient;
using System.Collections.Generic;

namespace YouTubeSearch.Core
{
    public static class YouTubeFeeds
    {
        public static async Task<List<YouTubeFeed>> Search(string keyword)
        {
            var httpClient = new HttpClient(new NativeMessageHandler());

            var xmlString = await httpClient.GetStringAsync(new Uri("http://gdata.youtube.com/feeds/api/videos?orderby=updated&vq=JAXA"));

            var ns = (XNamespace)"http://www.w3.org/2005/Atom";
            var mediaNs = (XNamespace)"http://search.yahoo.com/mrss/";
            var doc = XDocument.Parse(xmlString);

            return doc.Descendants(ns + "entry")
                .Select(x => new YouTubeFeed
                {
                    Id = x.Descendants(ns + "id").First().Value,
                    Title = x.Descendants(ns + "title").First().Value,
                    Content = x.Descendants(ns + "content").First().Value,
                    Url = x.Descendants(ns + "link").Where(l =>
                        { 
                            var rel = l.Attribute("rel"); 
                            return rel != null && rel.Value == "alternate"; 
                        })
                        .Select(l => l.Attribute("href").Value).FirstOrDefault(),
                    Thumbnails = x.Descendants(mediaNs + "thumbnail").Select(t => new YouTubeThumbnail
                        {
                            Url = t.Attribute("url").Value,
                            Height = int.Parse(t.Attribute("height").Value),
                            Width = int.Parse(t.Attribute("width").Value)
                        }).ToList()
                })
                .ToList();
        }
    }

    public class YouTubeFeed 
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public List<YouTubeThumbnail> Thumbnails { get; set; }

        public YouTubeThumbnail LargeThumbnail
        {
            get
            {
                return _largeThumbnail ?? (_largeThumbnail = Thumbnails.OrderByDescending(t => t.Height).First());
            }
        }
        YouTubeThumbnail _largeThumbnail;

        public YouTubeThumbnail SmallThumbnail
        {
            get
            {
                return _smallThumbnail ?? (_smallThumbnail = Thumbnails.OrderBy(t => t.Height).First());
            }
        }
        YouTubeThumbnail _smallThumbnail;
    }

    public class YouTubeThumbnail 
    {
        public string Url { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
    }
}

