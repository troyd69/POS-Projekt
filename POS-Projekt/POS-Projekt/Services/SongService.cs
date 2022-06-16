using Backend.Domain.Interfaces;
using Backend.Model;
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
			List<SSong> list = _dbContext.SSongs.ToList();
			return list;
		}
	}
}
