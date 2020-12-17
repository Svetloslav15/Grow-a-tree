namespace GrowATree.Application.Models.LoginHistory
{
    using System;
    using GrowATree.Application.Common.Mappings;
    using GrowATree.Domain.Entities;

    public class UserLoginModel : IMapFrom<LoginHistory>
    {
        public string Id { get; set; }

        public string DeviceName { get; set; }

        public string Ip { get; set; }

        public DateTime Date { get; set; }
    }
}
