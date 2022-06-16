using System;
using System.Collections.Generic;

namespace Backend.Model
{
    public partial class UUser
    {
        public UUser()
        {
            PPlaylists = new HashSet<PPlaylist>();
        }

        public int UId { get; set; }
        public string UUsername { get; set; } = null!;
        public string UPassword { get; set; } = null!;
        public DateOnly? UBirthdate { get; set; }

        public virtual ICollection<PPlaylist> PPlaylists { get; set; }

        public override bool Equals(object? obj)
        {
            return UUsername.Equals((obj as UUser).UUsername);
        }
    }
}
