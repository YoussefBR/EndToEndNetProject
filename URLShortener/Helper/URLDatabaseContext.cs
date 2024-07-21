using Microsoft.AspNetCore.Authentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using URLShortener.Models;

namespace URLShortener.Helper
{

    public class URLDatabaseContext : DbContext
    {

        public URLDatabaseContext(DbContextOptions<URLDatabaseContext> options) : base(options) {}

        public DbSet<ShortUrl> ShortUrls { get; set; }
        public DbSet<UserToID> UserToIDs { get; set; }

        public bool userExists(string email){ return UserToIDs.Any(u => u.Email == email); }
        public int getUserID(string email){ return UserToIDs.Single(u => u.Email == email).UserId; }

    }

}