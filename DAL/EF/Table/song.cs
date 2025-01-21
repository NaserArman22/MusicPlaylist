using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.EF.Table
{
    public class song
    {
        public int SongId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Artist { get; set; }
        public string Album { get; set; }

       
        [ForeignKey("Playlist")]
        public int PlaylistId { get; set; }

        
        public virtual playlist Playlist { get; set; }

        public string Lyrics { get; set; }
    }
}
