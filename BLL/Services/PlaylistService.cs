using AutoMapper;
using BLL.DTOs;
using DAL.EF.Table;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PlaylistService
    {
        public static Mapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<playlist, PlaylistDTO>().ReverseMap(); 
                cfg.CreateMap<song, SongDTO>().ReverseMap(); 
            });

            return new Mapper(config);
        }

        // Get all playlists
        public static List<PlaylistDTO> Get()
        {
            var repo = new PlaylistRepo();
            var data = repo.GetAll();
            var ret = GetMapper().Map<List<PlaylistDTO>>(data);
            return ret;
        }

        // Get a playlist by ID
        public static PlaylistDTO GetById(int id)
        {
            var repo = new PlaylistRepo();
            var data = repo.GetById(id);
            return data != null ? GetMapper().Map<PlaylistDTO>(data) : null;
        }

        // Create a new playlist
        public static void Create(PlaylistDTO playlistDTO)
        {
            var playlist = GetMapper().Map<playlist>(playlistDTO);
            var repo = new PlaylistRepo();
            repo.Create(playlist);
        }

        // Delete a playlist by ID
        public static void Delete(int id)
        {
            var repo = new PlaylistRepo();
            repo.Delete(id);
        }

        // Add a song to a playlist
        public static void AddSongToPlaylist(int playlistId, SongDTO songDTO)
        {
            var song = GetMapper().Map<song>(songDTO);
            var repo = new PlaylistRepo();
            repo.AddSongToPlaylist(playlistId, song);
        }

        // Search for playlists by name
        public static IEnumerable<PlaylistDTO> SearchByName(string name)
        {
            var repo = new PlaylistRepo();
            var data = repo.GetAll()
                .Where(p => p.Name != null &&
                            p.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(p => GetMapper().Map<PlaylistDTO>(p));
            return data;
        }

        // Search songs by title within a playlist
        public static IEnumerable<SongDTO> SearchSongsInPlaylist(int playlistId, string title)
        {
            var repo = new PlaylistRepo();
            var playlist = repo.GetById(playlistId);

            if (playlist == null) return Enumerable.Empty<SongDTO>();

            var songs = playlist.Songs
                .Where(s => s.Title != null &&
                            s.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(s => GetMapper().Map<SongDTO>(s));

            return songs;
        }
        public static SongDTO GetSongDetailsByName(string songName)
        {
            var repo = new PlaylistRepo();

            // Use LINQ to search for the song by its name (case-insensitive)
            var song = repo.GetAll()
                .SelectMany(p => p.Songs) // Flatten all songs from playlists
                .Where(s => s.Title != null &&
                            s.Title.Equals(songName, StringComparison.OrdinalIgnoreCase)) // Match by name
                .Select(s => new SongDTO // Dynamically map only the required fields
                {
                    SongId = s.SongId,
                    Title = s.Title,
                    Artist = s.Artist,
                    Album = s.Album
                })
                .FirstOrDefault(); // Get the first matching song or null

            return song;
        }
    }
}
