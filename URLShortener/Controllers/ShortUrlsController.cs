using Microsoft.AspNetCore.Mvc;

namespace URLShortener.Controllers;

[ApiController]
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


    [HttpPut("{shortUrl}")]
    public string Put(string shortUrl, string? keyword, [FromBody] ShortUrls url){
        return "http://localhost:5097/navigate/ge123";
        // generate a random url an attach it to the request id
        // generateURL(id)
    }


    [HttpDelete("{shortUrl}")]
    public void Delete(string shortUrl){
        // deleteURL(id)
    }

    [HttpPost("{shortUrl}")]
    public void Post(string shortUrl){
        // regenURL(id);
    }

}