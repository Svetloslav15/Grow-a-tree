namespace GrowATree.Application.Models.Users
{
    using GrowATree.Application.Common.Mappings;
    using GrowATree.Domain.Entities;

    public class UserModel : IMapFrom<User>
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfilePictureUrl { get; set; }
    }
}
