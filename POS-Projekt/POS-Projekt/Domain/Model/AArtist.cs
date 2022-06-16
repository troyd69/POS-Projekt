using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public partial class AArtist
    {
        public AArtist()
        {
            SSongs = new HashSet<SSong>();
        }

        public int AId { get; set; }
        public string AName { get; set; } = null!;

        public virtual ICollection<SSong> SSongs { get; set; }

        public override string ToString()
        {
            return AName;
        }
    }
}
