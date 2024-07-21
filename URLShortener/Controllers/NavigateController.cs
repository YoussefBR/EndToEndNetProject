using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using URLShortener.Helper;

namespace URLShortener.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = $"{JwtBearerDefaults.AuthenticationScheme},BasicAuthentication")]
[Route("[controller]")]

public class NavigateController: ControllerBase{

    private readonly URLDatabaseContext _context;

    public NavigateController(URLDatabaseContext context){
        _context = context;
    }

    [AllowAnonymous]
    [HttpGet("/navigate/{shortUrl}")]
    public RedirectResult Get(string shortUrl){ 
        ShortUrl url = _context.ShortUrls.SingleOrDefault(u => u.ShortenedUrl == shortUrl) ?? throw new Exception("URL not found");       
        return Redirect(url.OriginalUrl);
    }
}