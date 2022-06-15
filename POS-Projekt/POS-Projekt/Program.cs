using Backend.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend
{
    public class Program
    {
        static void Main(string[] args)
        {
            var context = new MusicDBContext();
            Console.WriteLine("hey");
            context.AArtists.ToList().ForEach(e => Console.WriteLine(e.AName));

            (from playlist in context.PPlaylists
            where playlist.PId == 1
             select playlist.ISSongs).ToList().ForEach(x => x.ToList().ForEach(y => Console.WriteLine(y.STitel)));
        }

    }
}
