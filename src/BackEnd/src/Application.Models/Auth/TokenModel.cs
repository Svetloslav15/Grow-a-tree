namespace Application.Models.Auth
{
    using System;

    public class TokenModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime Expires { get; set; }

        public string Id { get; set; }

        public bool IsStore { get; set; }

        public string Username { get; set; }

        public bool IsAdmin { get; set; }
    }
}
