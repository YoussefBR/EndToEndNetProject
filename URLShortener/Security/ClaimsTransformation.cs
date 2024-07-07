using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

using URLShortener.Helper;

namespace URLShortener.Security;

public class ClaimsTransformation : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var identity = principal.Identity as ClaimsIdentity;
        var email = principal.FindFirst(ClaimTypes.Email)?.Value ?? throw new UnauthorizedAccessException("Email not provided");
        var user = DataMock.GetUserByEmail(email) ?? throw new UnauthorizedAccessException("User not found");

        var newClaims = user.Roles.Select(role => new Claim(ClaimTypes.Role, role));
        identity!.AddClaims(newClaims);

        return Task.FromResult(principal);
    }
}
