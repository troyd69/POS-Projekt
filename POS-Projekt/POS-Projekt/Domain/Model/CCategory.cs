using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public partial class CCategory
    {
        public CCategory()
        {
            SSongs = new HashSet<SSong>();
        }

        public string CId { get; set; } = null!;
        public string CName { get; set; } = null!;

        public virtual ICollection<SSong> SSongs { get; set; }
    }
}
