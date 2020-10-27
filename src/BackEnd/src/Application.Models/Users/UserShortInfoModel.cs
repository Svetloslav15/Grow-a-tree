namespace GrowATree.Application.Models.Users
{
    using GrowATree.Application.Common.Mappings;
    using GrowATree.Domain.Entities;

    public class UserShortInfoModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string City { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}
