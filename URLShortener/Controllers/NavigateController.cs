using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace URLShortener.Controllers;

[ApiController]
[Authorize(AuthenticationSchemes = $"{JwtBearerDefaults.AuthenticationScheme},BasicAuthentication")]
[Route("[controller]")]

public class NavigateController: ControllerBase{

    [AllowAnonymous]
    [HttpGet("/navigate/{shortUrl}")]
    public RedirectResult Get(string shortUrl){ 
        // use id to grab full address
        string full_addr = "https://true-address.com";
        return Redirect("https://www.google.com");
    }
}