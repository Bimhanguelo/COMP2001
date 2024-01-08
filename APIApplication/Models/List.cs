using System;
using System.Collections.Generic;

namespace APIApplication.Models
{
    public partial class List
    {
        public int ListId { get; set; }
        public int? ListUserId { get; set; }
        public string? ListName { get; set; }

        public virtual UserProfile? ListUser { get; set; }
    }
}
