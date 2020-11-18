namespace GrowATree.Application.Models.Waterings
{
    using System;
    using GrowATree.Application.Common.Mappings;
    using GrowATree.Domain.Entities;

    public class WateringModel : IMapFrom<TreeWatering>
    {
        public string Id { get; set; }

        public string UserUserName { get; set; }

        public string UserProfilePictureUrl { get; set; }

        public DateTime WateredOn { get; set; }
    }
}
