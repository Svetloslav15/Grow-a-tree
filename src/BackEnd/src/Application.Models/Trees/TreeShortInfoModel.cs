namespace GrowATree.Application.Models.Trees
{
    using System;
    using System.Linq;
    using AutoMapper;
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

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double MetresAway { get; set; }

        public UserShortInfoModel Owner { get; set; }

        public ImageModel Image { get; set; }

        public void Mapping(Profile profile)
        {
            profile.AllowNullCollections = true;
            profile.CreateMap<Tree, TreeShortInfoModel>()
                .ForMember(m => m.Image, o => o.MapFrom(s => s.Images.FirstOrDefault()));

        }
    }
}
