using DAL.EF.Table;
using DAL.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;

namespace DAL.Repos
{
    public class PlaylistRepo : IRepo<playlist>
    {
        private  MusicDBContext db;

        public PlaylistRepo()
        {
            db = new MusicDBContext();
        }

        public List<playlist> GetAll()
        {
            return db.Playlists.Include("Songs").ToList(); 
        }

        public playlist GetById(int id)
        {
            return db.Playlists.Include("Songs").FirstOrDefault(p => p.PlaylistId == id);
        }

        public void Create(playlist playlist)
        {
            db.Playlists.Add(playlist);
            db.SaveChanges();
        }

        public void view(playlist playlist, int id)
        {
            
            var existingPlaylist = GetById(id);
            if (existingPlaylist != null)
            {
                playlist.PlaylistId = existingPlaylist.PlaylistId;
                playlist.Name = existingPlaylist.Name;
                playlist.Songs = existingPlaylist.Songs;
            }
        }

        public void Delete(int id)
        {
            var existingPlaylist = GetById(id);
            if (existingPlaylist != null)
            {
                db.Playlists.Remove(existingPlaylist);
                db.SaveChanges();
            }
        }
        public void AddSongToPlaylist(int playlistId, song song)
        {
            var playlist = GetById(playlistId);
            if (playlist != null)
            {
                playlist.Songs.Add(song); 
                db.SaveChanges();       
            }
        }

            public void Save()
        {
            db.SaveChanges();
        }

    }
}
