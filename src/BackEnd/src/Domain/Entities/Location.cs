using System.ComponentModel.DataAnnotations;

namespace GrowATree.Domain.Entities
{
    public class Location
    {
        [Key]
        public string Id { get; set; }

        public string Latitude { get; set; }
        
        public string Longitude { get; set; }
    }
}