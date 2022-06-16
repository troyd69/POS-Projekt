using Backend.Domain.Interfaces;
using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Services
{
	public class UserService : IUserService
	{
		private readonly MusicDBContext _dbContext;

		public UserService(
		MusicDBContext dbContext)
		{
			_dbContext = dbContext;
		}

		public UUser Anmelden(string user, string pw)
		{
			UUser b = null;
			List<UUser> list = new((from a in _dbContext.UUsers
									where a.UUsername == user && a.UPassword == pw
									select a).ToList());
			if (list.Count > 0)
				b = list[0];
			return b;
		}


		public UUser Registrieren(string user, string password, DateOnly date){
			int userID = (from a in _dbContext.UUsers
					  select a).ToList().Max(x=>x.UId);
			UUser b = null;
			b= new();
			b.UId = Interlocked.Increment(ref userID);
			b.UUsername = user;
			b.UPassword = password;
			b.UBirthdate = date;
			if (!_dbContext.UUsers.Contains(b))
				_dbContext.UUsers.Add(b);
			else
				b = null;
			_dbContext.SaveChanges();
			return b;
		}
	}
}
