using System;
using System.Collections.Generic;

namespace POS_Projekt.Model
{
    public partial class PPlaylist
    {
        public PPlaylist()
        {
            ISSongs = new HashSet<SSong>();
        }

        public int PId { get; set; }
        public string PName { get; set; } = null!;
        public int? PUUser { get; set; }

        public virtual UUser? PUUserNavigation { get; set; }

        public virtual ICollection<SSong> ISSongs { get; set; }
    }
}
