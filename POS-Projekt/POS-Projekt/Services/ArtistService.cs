using Backend.Domain.Interfaces;
using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services
{
	public class ArtistService : IArtistService
	{
		private readonly MusicDBContext _dbContext;

		public ArtistService(
		MusicDBContext dbContext)
		{
			_dbContext = dbContext;
		}

		public List<AArtist> listArtists()
		{
			return (from a in _dbContext.AArtists
					select a).Distinct().ToList();
		}
	}
}
