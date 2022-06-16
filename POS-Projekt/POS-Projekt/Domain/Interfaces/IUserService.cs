using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Domain.Interfaces
{
	public interface IUSerService
	{
		UUser Anmelden(string user, string pw);
		UUser Registrieren(string user, string password, DateOnly date);
	}
}
