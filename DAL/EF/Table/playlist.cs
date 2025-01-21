using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Table
{
    public class playlist
    {
        public int PlaylistId { get; set; }
        public string Name { get; set; }

        
        public virtual List<song> Songs { get; set; }

        
        public playlist()
        {
            Songs = new List<song>();
        }
    }
}
