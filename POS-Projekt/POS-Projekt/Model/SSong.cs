using System;
using System.Collections.Generic;

namespace POS_Projekt.Model
{
    public partial class SSong
    {
        public SSong()
        {
            IPPlaylists = new HashSet<PPlaylist>();
        }

        public int SId { get; set; }
        public string STitel { get; set; } = null!;
        public int? SAArtist { get; set; }
        public string SPath { get; set; } = null!;
        public TimeOnly? STimespan { get; set; }
        public string? SCCategory { get; set; }

        public virtual AArtist? SAArtistNavigation { get; set; }
        public virtual CCategory? SCCategoryNavigation { get; set; }

        public virtual ICollection<PPlaylist> IPPlaylists { get; set; }
    }
}
