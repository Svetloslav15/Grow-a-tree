namespace GrowATree.Application.Models.Trees
{
    using System;
    using GrowATree.Application.Common.Mappings;
    using GrowATree.Application.Models.Images;
    using GrowATree.Application.Models.Users;
    using GrowATree.Domain.Entities;
    using GrowATree.Domain.Enums;

    public class TreeShortInfoModel : IMapFrom<Tree>
    {
        public string Id { get; set; }

        public string Nickname { get; set; }

        public DateTime PlantedOn { get; set; }

        public TreeStatus Status { get; set; }

        public string City { get; set; }

        public UserShortInfoModel Owner { get; set; }

        public ImageModel Image { get; set; }
    }
}
