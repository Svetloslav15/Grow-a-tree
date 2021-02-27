namespace GrowATree.Domain.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class UnknownTrees
    {
        [Key]
        public string Id { get; set; }

        public string TreeName { get; set; }

        public string ClosestResults { get; set; }

        public string Votes { get; set; }
    }
}
