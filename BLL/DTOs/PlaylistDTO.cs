using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs
{
    public class PlaylistDTO
    {
        public int PlaylistId { get; set; }
        public string Name { get; set; }

      
        public List<SongDTO> Songs { get; set; }

        public PlaylistDTO()
        {
            Songs = new List<SongDTO>();
        }
    }
}
