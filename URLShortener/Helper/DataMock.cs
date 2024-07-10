using URLShortener.Models;
// using URLShortener.Security;

namespace URLShortener.Helper
{
    public class DataMock
    {
        public static readonly List<User> Users =
        [
            new User { FirstName = "Youssef", LastName = "Admin", Email = "youssef.boshrariad+test1@gmail.com", Password = "tiQmax-6cewdi-kebjyp", PasswordSalt = "47325dc7cac362ad0bc57808c71c61", Roles = ["Admin"] },
            new User { FirstName = "Youssef", LastName = "User", Email = "youssef.boshrariad+test2@gmail.com", Password = "kozkak-wupzov-Sybqu6", PasswordSalt = "4db039f1d58079bc28c7558daccdd9", Roles = ["User"] },
        ];

        public static readonly List<String> Roles = 
        [
            "Admin",
            "User",
        ];

        public static readonly List<string> Permissions = 
        [
            "Create",
            "Read",
            "Update",
            "Delete",
        ];

        public static readonly List<(string Role, string Permission)> RolePermissionMatrix =
        [
            ("Admin", "Create"),
            ("Admin", "Read"),
            ("Admin", "Update"),
            ("Admin", "Delete"),
            ("User", "Read")
        ];

        public static User? GetUserByEmail(string email)
        {
            return Users.FirstOrDefault(user => user.Email!.Equals(email, StringComparison.OrdinalIgnoreCase));
        }
    }
}