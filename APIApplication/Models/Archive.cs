using System;
using System.Collections.Generic;

namespace APIApplication.Models
{
    public partial class Archive
    {
        public int ArchiveId { get; set; }
        public int? ArchiveUserId { get; set; }
        public DateTime? ArchiveDate { get; set; }

        public virtual UserProfile? ArchiveUser { get; set; }
    }
}
