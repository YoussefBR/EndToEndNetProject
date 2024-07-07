using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace URLShortener.Controllers;

[ApiController]
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

    [HttpGet("{shortUrl}/hits")]
    public int GetHits(string shortUrl){
        return 4;
    }

    [Authorize(Policy = "Update")]
    [HttpPut("{shortUrl}")]
    public string Put(string shortUrl, string? keyword, [FromBody] ShortUrl url){
        return "Successfully put: " + url.Url;
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
    [HttpPost("{shortUrl}")]
    public string Post(string shortUrl){
        // regenURL(id);
        return "Successfully posted: " + shortUrl;
    }

}