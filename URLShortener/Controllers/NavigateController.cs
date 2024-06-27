using Microsoft.AspNetCore.Mvc;

namespace URLShortener.Controllers;

[ApiController]
[Route("[controller]")]

public class NavigateController: ControllerBase{

    [HttpGet("/navigate/{shortUrl}")]
    public string Get(string shortUrl){ 
        // use id to grab full address
        string full_addr = "https://true-address.com";
        return full_addr;
    }
}