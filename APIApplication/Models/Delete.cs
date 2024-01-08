using System;
using System.Collections.Generic;

namespace APIApplication.Models
{
    public partial class Delete
    {
        public int RequestId { get; set; }
        public int? RequestUserId { get; set; }
        public DateTime? RequestDate { get; set; }

        public virtual UserProfile? RequestUser { get; set; }
    }
}
