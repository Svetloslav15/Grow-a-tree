namespace GrowATree.Domain.Entities
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImageUrl { get; set; }

        public string City { get; set; }

        public ICollection<Tree> Trees { get; set; } = new List<Tree>();

        public ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();

        public ICollection<TreePostReaction> TreePostReactions { get; set; } = new List<TreePostReaction>();

        public ICollection<TreeReport> TreeReports { get; set; } = new List<TreeReport>();

        public ICollection<TreeWatering> TreeWaterings { get; set; } = new List<TreeWatering>();
    }
}