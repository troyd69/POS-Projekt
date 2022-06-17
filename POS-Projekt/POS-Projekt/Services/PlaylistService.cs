using Backend.Domain.Interfaces;
using Backend.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services
{
	public class PlaylistService : IPlaylistService
	{
		private readonly MusicDBContext _dbContext;

		public PlaylistService(
		MusicDBContext dbContext)
		{
			_dbContext = dbContext;
		}

		public PPlaylist AddPlayList(string name, int user, List<SSong> songs)
		{
			int playlistID = (from a in _dbContext.PPlaylists
							  select a).ToList().Max(x => x.PId);
			PPlaylist b = null;
			b = new();
			b.PId = Interlocked.Increment(ref playlistID);
			b.PName = name;
			b.PUUser = user;
			b.ISSongs = songs;
			if (!_dbContext.PPlaylists.Contains(b) && (from a in _dbContext.PPlaylists
													   where a.PName == b.PName
													   select a).ToList().Count == 0)
				_dbContext.PPlaylists.Add(b);
			else
				b = null;
			_dbContext.SaveChanges();
			return b;
		}

		public PPlaylist ChangePlayList(string name, List<SSong> songs, int id)
		{
			//(from a in _dbContext.PPlaylists
			// where a.PId == playlist
			// select a).ToList().ForEach(x=> { 
			//    x.PName=name;
			//     x.ISSongs = songs;
			// });
			//_dbContext.SaveChanges();
			//return null;

			var playList = (from a in _dbContext.PPlaylists.Include(x => x.ISSongs)
							where a.PId == id
							select a).FirstOrDefault();

			if (playList == null) return null;


			playList.PName = name;
			playList.ISSongs = songs;

			_dbContext.SaveChanges();
			return null;
		}

		public List<PPlaylist> PlayListvonUser(int user)
		{
			List<PPlaylist> p = (List<PPlaylist>)(from a in _dbContext.PPlaylists.Include(x => x.PUUserNavigation)
												  where a.PUUserNavigation.UId == user
												  select a).ToList();

			return p;
		}
	}
}
