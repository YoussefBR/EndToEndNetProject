using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace URLShortener;

public class ShortUrl{

        public ShortUrl(string urlId, int userId, string originalUrl, string shortenedUrl, int hits)
        {
            UrlId = urlId;
            UserId = userId;
            OriginalUrl = originalUrl;
            ShortenedUrl = shortenedUrl;
            Hits = hits;
        }

        [Required]
        [Key]
        [StringLength(50)]
        public string UrlId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(2000)]
        public string OriginalUrl { get; set; }

        [Required]
        [StringLength(50)]
        public string ShortenedUrl { get; set; }
        
        [Required]
        public int Hits { get; set; }
        
}

public class LongUrl{

    public LongUrl(string url)
    {
        Url = url;
    }
    
    [Required]
    public string Url { get; set; }

}