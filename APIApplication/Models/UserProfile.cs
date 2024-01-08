using System;
using System.Collections.Generic;

namespace APIApplication.Models
{
    public partial class UserProfile
    {
        public UserProfile()
        {
            Activities = new HashSet<Activity>();
            Archives = new HashSet<Archive>();
            Deletes = new HashSet<Delete>();
            Lists = new HashSet<List>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string? Description { get; set; }
        public string? PictureUrl { get; set; }
        public int? Difficulty { get; set; }
        public string? Location { get; set; }
        public DateTime? JoinedDate { get; set; }
        public string? Units { get; set; }
        public string Password { get; set; } = null!;
        public decimal? Height { get; set; }
        public decimal? Weight { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Language { get; set; }
        public string? ActivityTimeframe { get; set; }

        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<Archive> Archives { get; set; }
        public virtual ICollection<Delete> Deletes { get; set; }
        public virtual ICollection<List> Lists { get; set; }
    }
}
