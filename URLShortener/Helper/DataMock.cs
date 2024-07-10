using URLShortener.Models;
// using URLShortener.Security;

namespace URLShortener.Helper
{
    public class DataMock
    {
        public static readonly List<User> Users =
        [
            // tiQmax-6cewdi-kebjyp
            new User { FirstName = "Youssef", LastName = "Admin", Email = "youssef.boshrariad+test1@gmail.com", Password = "2faecc5c098d61c9b57dda5006cd0a6e734fa1dfb47c8c50f19d8c4b4b086466af67c1b1d70edc4e840d773e4dd2311303302a04b03f3e37dde6e84c19f29a5c", PasswordSalt = "47325dc7cac362ad0bc57808c71c61", Roles = ["Admin"] },
            // kozkak-wupzov-Sybqu6
            new User { FirstName = "Youssef", LastName = "User", Email = "youssef.boshrariad+test2@gmail.com", Password = "9cca26cdfde31f6e5fce1eca4b8350aacdc533ccef8e1380a40b58398496ebd7810dab1621d5da81ba89ab542b5258b03a3a1d9790b68f17dee34ddae7997809", PasswordSalt = "4db039f1d58079bc28c7558daccdd9", Roles = ["User"] },
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