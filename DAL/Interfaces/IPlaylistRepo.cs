using DAL.EF.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IPlaylistRepo : IRepo<playlist>
    {
        
        void AddSongToPlaylist(int playlistId, song song);
    }
}
