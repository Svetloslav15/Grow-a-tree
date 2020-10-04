﻿namespace Common.Constants
{
    /// <summary>
    /// Constants such as numeric values,
    /// urls, etc.
    /// </summary>
    public static class Constants
    {
        public const int JwtExpirationTimeInHours = 12;

        public const int PasswordMinLength = 5;

        public const int PasswordMaxLength = 20;

        public const int UsernameMinLength = 3;

        public const int UsernameMaxLength = 15;

        public const string StoreRoleName = "Store";

        public const string AdminFirstName = "Grow";

        public const string AdminLastName = "Tree";

        public const string ConfirmEmailLink = "https://localhost:44312/api/Auth/";
    }
}
