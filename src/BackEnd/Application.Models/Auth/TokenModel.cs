﻿namespace Application.Models.Auth.ViewModels
{
    using System;

    public class TokenModel
    {
        public string Token { get; set; }

        public DateTime Expires { get; set; }

        public string Id { get; set; }
    }
}
