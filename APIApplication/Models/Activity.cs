using System;
using System.Collections.Generic;

namespace APIApplication.Models
{
    public partial class Activity
    {
        public int ActivityId { get; set; }
        public int? ActivityUserId { get; set; }
        public string? ActivityType { get; set; }
        public DateTime? ActivityDate { get; set; }

        public virtual UserProfile? ActivityUser { get; set; }
    }
}
