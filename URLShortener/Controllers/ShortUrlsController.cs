using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace URLShortener.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = $"{JwtBearerDefaults.AuthenticationScheme},BasicAuthentication")]
[Authorize(Policy = "Read")]
[Route("[controller]")]

public class ShortUrlsController: ControllerBase{

    [HttpGet]
    public string Get(){
        return "http://localhost:5097/navigate/ge123";
    }

    [HttpGet("{shortUrl}")]
    public string Get(string shortUrl){
        // just an example but use id to pull shortened url
        return "http://localhost:5097/navigate/ge123";
    }

    [Authorize(Policy = "Create")]
    [HttpGet("{shortUrl}/hits")]
    public int GetHits(string shortUrl){
        return 4;
    }

    [Authorize(Policy = "Update")]
    [HttpPost("{shortUrl}")]
    public string Post(string shortUrl, string? keyword, [FromBody] ShortUrl url){
        return "Successfully posted: " + url.Url;
        // generate a random url an attach it to the request id
        // generateURL(id)
    }

    [Authorize(Policy = "Delete")]
    [HttpDelete("{shortUrl}")]
    public string Delete(string shortUrl){
        // deleteURL(id)
        return "Successfully deleted: " + shortUrl;
    }

    [Authorize(Policy = "Create")]
    [HttpPut("{shortUrl}")]
    public string Put(string shortUrl){
        // regenURL(id);
        return "Successfully put: " + shortUrl;
    }

}