using System.ComponentModel.DataAnnotations;

namespace GrowATree.Domain.Entities
{
    public class Image
    {
        [Key]
        public string Id { get; set; }

        public string Url { get; set; }

        public string TreeId { get; set; }

        public Tree Tree { get; set; }
    }
}