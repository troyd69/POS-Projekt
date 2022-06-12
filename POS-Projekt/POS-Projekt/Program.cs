using POS_Projekt.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Projekt
{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new MusicDBContext();
            Console.WriteLine("hey");
            context.AArtists.ToList().ForEach(e => Console.WriteLine(e.AName));
        }
    }
}
