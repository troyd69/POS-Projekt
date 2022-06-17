using Backend.Domain.Interfaces;
using Backend.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services
{
	public class SongService : ISongService
	{
		private readonly MusicDBContext _dbContext;

		public SongService(
		MusicDBContext dbContext)
		{
			_dbContext = dbContext;
		}

		public List<SSong> ListSongs()
		{
			List<SSong> list = _dbContext.SSongs.Include(x => x.SCCategoryNavigation).Include(x => x.SAArtistNavigation).ToList();
			return list;
		}

		public List<SSong> GetSongsfromPlaylist(PPlaylist playlist)
		{

			return (from song in _dbContext.SSongs.Include(x => x.SAArtistNavigation)
					from songPlaylist in song.IPPlaylists
					where songPlaylist.Equals(playlist)
				select song).ToList();
		}

		public SSong AddSong(string titel, CCategory cat, string pfad, AArtist artist)
		{
			int songID = (from a in _dbContext.SSongs
						  select a).ToList().Max(x => x.SId);
			SSong b = null;
			b = new();
			b.SId = Interlocked.Increment(ref songID);
			b.STitel = titel;
			b.SCCategoryNavigation = cat;
			b.SAArtistNavigation = artist;
			b.SPath = pfad;
			if (!_dbContext.SSongs.Contains(b))
				_dbContext.SSongs.Add(b);
			else
				b = null;
			_dbContext.SaveChanges();
			return b;
		}
	}
}
