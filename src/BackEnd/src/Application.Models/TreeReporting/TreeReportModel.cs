namespace GrowATree.WebAPI.Controllers
{
    using GrowATree.Application.Common.Mappings;
    using GrowATree.Domain.Entities;
    using GrowATree.Domain.Enums;

    public class TreeReportModel : IMapFrom<TreeReport>
    {
        public string Id { get; set; }

        public string Message { get; set; }

        public string ImageUrl { get; set; }

        public TreeReportType Type { get; set; }

        public string TreeId { get; set; }

        public string TreeNickname { get; set; }

        public string UserId { get; set; }

        public string UserUserName { get; set; }

        public string UserProfilePictureUrl { get; set; }
    }
}
