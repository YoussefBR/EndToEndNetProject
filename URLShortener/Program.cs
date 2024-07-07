using URLShortener.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

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

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();