namespace GrowATree.Domain.Entities
{
    using System;

    public class LoginHistory
    {
        public string Id { get; set; }

        public string DeviceName { get; set; }

        public string Ip { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
