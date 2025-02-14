﻿namespace GrowATree.Domain.Entities
{
    using System;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Identity;

    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string City { get; set; }

        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }

        public string ReferrerId { get; set; }

        public User Referer { get; set; }

        public ICollection<Tree> Trees { get; set; } = new List<Tree>();

        public ICollection<TreeReaction> Reactions { get; set; } = new List<TreeReaction>();

        public ICollection<TreePostReaction> TreePostReactions { get; set; } = new List<TreePostReaction>();

        public ICollection<TreeReport> TreeReports { get; set; } = new List<TreeReport>();

        public ICollection<TreeWatering> TreeWaterings { get; set; } = new List<TreeWatering>();

        public ICollection<TreePost> TreePosts { get; set; } = new List<TreePost>();

        public ICollection<TreePostReply> TreePostReplies { get; set; } = new List<TreePostReply>();

        public ICollection<User> Referals { get; set; } = new List<User>();

        public ICollection<LoginHistory> Logins { get; set; } = new List<LoginHistory>();
    }
}