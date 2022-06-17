using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces
{
	public interface ISongService
	{
		List<SSong> ListSongs();
		List<SSong> GetSongsfromPlaylist(PPlaylist playlist);
		SSong AddSong(string titel, CCategory cat, string pfad, AArtist artist);
	}
}
