using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces
{
	public interface IPlaylistService
	{
		List<PPlaylist> PlayListvonUser(int user);
		PPlaylist AddPlayList(string name, int user, List<SSong> songs);
		PPlaylist ChangePlayList(string name, List<SSong> songs, PPlaylist playlist);

	}
}
