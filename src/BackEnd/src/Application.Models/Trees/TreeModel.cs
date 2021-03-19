namespace GrowATree.Application.Models.Trees
{
    using System;
    using System.Collections.Generic;
    using GrowATree.Application.Common.Mappings;
    using GrowATree.Application.Models.Images;
    using GrowATree.Application.Models.Users;
    using GrowATree.Domain.Entities;
    using GrowATree.Domain.Enums;

    public class TreeModel : IMapFrom<Tree>
    {
        public string Id { get; set; }

        public string Nickname { get; set; }

        public string Type { get; set; }

        public DateTime PlantedOn { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public TreeStatus Status { get; set; }

        public string City { get; set; }

        public string Category { get; set; }

        public string OwnerId { get; set; }

        public bool IsDeleted { get; set; }

        public UserModel Owner { get; set; }

        public IEnumerable<ImageModel> Images { get; set; } = new HashSet<ImageModel>();
    }
}
