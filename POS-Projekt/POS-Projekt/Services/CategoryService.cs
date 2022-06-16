using Backend.Domain.Interfaces;
using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services
{
	public class CategoryService : ICategoryService
	{
		public List<CCategory> ListCategorys()
		{
			return (from a in _dbContext.CCategories
					select a).Distinct().ToList();
		}

		private readonly MusicDBContext _dbContext;

		public CategoryService(
		MusicDBContext dbContext)
		{
			_dbContext = dbContext;
		}

	
	}
}
