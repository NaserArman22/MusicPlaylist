using DAL.EF.Table;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF
{
    public class MusicDBContext : DbContext
    {
        public DbSet<playlist> Playlists { get; set; }
        public DbSet<song> Songs { get; set; }
    }
}
