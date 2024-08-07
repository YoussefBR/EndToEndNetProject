using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using URLShortener.Helper;

namespace URLShortener.Security;

public class BasicAuthenticationHandler(
    IOptionsMonitor<AuthenticationSchemeOptions> options,
    ILoggerFactory logger,
    UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            // The API will receive Basic username:password in Base 64. We will decode it
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers.Authorization!);
            var credentialBytes = Convert.FromBase64String(authHeader.Parameter!);
            // This splits the "username:password" to an array of [username,password]
            var credentials = Encoding.UTF8.GetString(credentialBytes).Split(':');
            var email = credentials[0];
            var password = credentials[1];

            // This checks against the database and determine if the username/email and password
            // exist in it. "Any" functions returns true/false if one is found
            if (DataMock.Users.Any(user => user.Email!.Equals(email, StringComparison.OrdinalIgnoreCase)
                                        && user.Password == Cryptography.HashPassword(password, user.PasswordSalt!)))
            {
                // This is how C# middleware captures the email which will be
                // used later by authorization via ClaimsTransformer
                // I am using "emails" instead of "email" claim to match how jwt sends it
                var claims = new[] { new Claim("emails", email) };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            else
            {
                return AuthenticateResult.Fail("Invalid Username or Password");
            }
        }
        catch
        {
            return AuthenticateResult.Fail("Invalid Authorization Header");
        }
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
}