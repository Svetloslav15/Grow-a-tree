namespace Common.Constants
{
    /// <summary>
    /// Constants such as numeric values,
    /// urls, etc.
    /// </summary>
    public static class Constants
    {
        public const int JwtExpirationTimeInMinutes = 10;

        public const int JwtExpirationTimeInDays = 7;

        public const int PasswordMinLength = 5;

        public const int PasswordMaxLength = 20;

        public const int UsernameMinLength = 3;

        public const int UsernameMaxLength = 15;

        public const int NameMinLength = 3;

        public const int NameMaxLength = 30;

        public const int TreeNicknameMinLength = 3;

        public const int TreeNicknameMaxLength = 20;

        public const string PhoneNumberRegEx = @"/\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/";

        public const string StoreRoleName = "Store";

        public const string AdminFirstName = "Grow";

        public const string AdminLastName = "Tree";

        public const string DefaultProfilePictureUrl = "https://res.cloudinary.com/dzivpr6fj/image/upload/v1602432685/GrowATree/avatar_dpskn1.png";

        public const string ConfirmEmailLink = "https://localhost:44312/api/auth/confirm/";

        public const string ResetPasswordLink = "https://localhost:44312/api/auth/reset-password/";
    }
}
