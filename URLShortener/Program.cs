using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using URLShortener.Helper;
using URLShortener.Security;
using Microsoft.EntityFrameworkCore;

namespace URLShortener;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();

        // builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        // builder.Configuration.GetConnectionString("DefaultConnection")
        
        builder.Services.AddDbContext<URLDatabaseContext>(options =>
        options.UseSqlServer("Server=tcp:cap-youssef-db.database.windows.net,1433;Initial Catalog=capyoussefdb;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;Authentication=Active Directory Default;"));

        builder.Services.AddAuthentication(options =>
        {
            // This defaults that the upcoming request is Bearer {JWT}
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        // This adds the JWT bearer setup for OAuth 2.0. Using the values in the
        // app settings, it validates that the incoming bearer token is valid by sending
        // it back to the OAuth IdP
        .AddJwtBearer(options =>
        {
            options.Authority = builder.Configuration["AzureAdB2C:Authority"];
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = builder.Configuration["AzureAdB2C:Issuer"],
                ValidAudience = builder.Configuration["AzureAdB2C:Audience"]
            };
        })
        // This adds the Basic authentication setup, which expects login info as "username:password" in Base64 in the header
        // Authentication is implemented in BasicAuthenticationHandler.cs
        .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

        // This sets up basic authentication and sets up the roles and permissions relationship
        builder.Services.AddAuthorization(options =>
        {
            var permissionsByRoles = DataMock.RolePermissionMatrix
                .GroupBy(rolePermission => rolePermission.Permission)
                .ToDictionary(group => group.Key, group => group.Select(rolePermission => rolePermission.Role).ToArray());

            foreach (var keyValuePair in permissionsByRoles){
                options.AddPolicy(keyValuePair.Key, policy => policy.RequireRole(keyValuePair.Value));
            }
        }
);


        // When somebody is authenticated, we have to determine what are their roles
        // This is why we add "ClaimsTransformation". We use AddTransient as want to call this
        // for "each single API request" (Zero Trust)
        builder.Services.AddTransient<IClaimsTransformation, ClaimsTransformation>();

        var app = builder.Build();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}