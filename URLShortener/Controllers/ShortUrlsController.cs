using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using URLShortener.Helper;
using URLShortener.Security;

namespace URLShortener.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = $"{JwtBearerDefaults.AuthenticationScheme},BasicAuthentication")]
[Authorize(Policy = "Read")]
[Route("[controller]")]

public class ShortUrlsController: ControllerBase{

    private readonly URLDatabaseContext _context;
    private readonly IClaimsTransformation _claimsTransformation;

    public ShortUrlsController(URLDatabaseContext context, IClaimsTransformation claimsTransformation){
        _context = context;
        _claimsTransformation = claimsTransformation;
    }

    [HttpGet("{id}")]
    public string Get(string id){
        ShortUrl? url = _context.ShortUrls.SingleOrDefault(u => u.UrlId == id);
        return url?.ShortenedUrl ?? "URL not found";
    }

    [Authorize(Policy = "Create")]
    [HttpGet("{id}/hits")]
    public int GetHits(string id){

        // grab the user email from the claims of this request, unique value with every request to API
        string user_email = _claimsTransformation.TransformAsync(User).Result.FindFirstValue("emails") ?? throw new UnauthorizedAccessException("Email not provided");

        if(!_context.userExists(user_email)) throw new Exception("Current user not in system");
        int user_id = _context.getUserID(user_email);

        // only allow user to see their own urls
        ShortUrl? url = _context.ShortUrls.Single(u => u.UrlId == id && u.UserId == user_id);
        return url?.Hits ?? -1;
    }

    [Authorize(Policy = "Update")]
    [HttpPut("{id}")]
    public string Put(string id, [FromBody] LongUrl longUrl){
        // TODO: maybe regen short url?
        return "Successfully updated: " + longUrl.Url;
    }

    [Authorize(Policy = "Delete")]
    [HttpDelete("{id}")]
    public string Delete(string id){
        ShortUrl? url = _context.ShortUrls.SingleOrDefault(u => u.UrlId == id);
        string user_email = _claimsTransformation.TransformAsync(User).Result.FindFirstValue("emails") ?? throw new UnauthorizedAccessException("Email not provided");
        int user_id = _context.getUserID(user_email);

        if(url != null && url.UserId == user_id){
            _context.ShortUrls.Remove(url);
            _context.SaveChanges();
        }
        else if(url != null && url.UserId != user_id) throw new UnauthorizedAccessException("User does not have permission to delete this URL");
        else return "URL not found";

        return "Successfully deleted: " + id;
    }

    [Authorize(Policy = "Create")]
    [HttpPost("{id}")]
    public string Post(string id, [FromBody] LongUrl longUrl){

        string shortUrl = Cryptography.GenShortUrl(longUrl.Url, id);
        string user_email = _claimsTransformation.TransformAsync(User).Result.FindFirstValue("emails") ?? throw new UnauthorizedAccessException("Email not provided");
        int user_id = _context.getUserID(user_email);

        ShortUrl? url = _context.ShortUrls.SingleOrDefault(u => u.UrlId == id || u.ShortenedUrl == shortUrl);

        if(url == null){
            _context.ShortUrls.Add(new ShortUrl(id, user_id, longUrl.Url, shortUrl, 0));
            _context.SaveChanges();
        }
        else{
            if(url.UrlId == id) return "URL ID already in use";
            else return "Short URL already in use";
        }

        return "Successfully posted: " + id;

    }

}