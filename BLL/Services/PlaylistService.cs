using AutoMapper;
using BLL.DTOs;
using BLL.Utilities;
using DAL;
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

        
        public static List<PlaylistDTO> Get()
        {
            var repo = dataAccessFactory.GetRepo();
            var data = repo.GetAll();
            var ret = GetMapper().Map<List<PlaylistDTO>>(data);
            return ret;
        }

        
        public static PlaylistDTO GetById(int id)
        {
            var repo = dataAccessFactory.GetRepo();
            var data = repo.GetById(id);
            return data != null ? GetMapper().Map<PlaylistDTO>(data) : null;
        }

        
        public static void Create(PlaylistDTO playlistDTO)
        {
            var playlist = GetMapper().Map<playlist>(playlistDTO);
            var repo = dataAccessFactory.GetRepo();
            repo.Create(playlist);
        }

        
        public static void Delete(int id)
        {
            var repo = dataAccessFactory.GetRepo();
            repo.Delete(id);
        }

       
        public static void AddSongToPlaylist(int playlistId, SongDTO songDTO)
        {
            var song = GetMapper().Map<song>(songDTO);
            var repo = dataAccessFactory.GetRepo();
            repo.AddSongToPlaylist(playlistId, song);
        }

        
        public static IEnumerable<PlaylistDTO> SearchByName(string name)
        {
            var repo = dataAccessFactory.GetRepo();
            var data = repo.GetAll()
                .Where(p => p.Name != null &&
                            p.Name.IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0)
                .Select(p => GetMapper().Map<PlaylistDTO>(p));
            return data;
        }

        
        public static IEnumerable<SongDTO> SearchSongsInPlaylist(int playlistId, string title)
        {
            var repo = dataAccessFactory.GetRepo();
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
            var repo =  dataAccessFactory.GetRepo();


            var song = repo.GetAll()
                .SelectMany(p => p.Songs) 
                .Where(s => s.Title != null &&
                            s.Title.Equals(songName, StringComparison.OrdinalIgnoreCase)) // Match by name
                .Select(s => new SongDTO 
                {
                    SongId = s.SongId,
                    Title = s.Title,
                    Artist = s.Artist,
                    Album = s.Album
                })
                .FirstOrDefault(); 

            return song;
        }

        public static void SharePlaylistViaEmail(int playlistId, string email)
        {
            var playlistRepo = dataAccessFactory.GetRepo();

            
            var playlist = playlistRepo.GetById(playlistId);
            if (playlist == null)
                throw new Exception("Playlist not found");

            
            var message = new StringBuilder();
            message.AppendLine($"Playlist Name: {playlist.Name}");
            message.AppendLine("Songs:");
            foreach (var song in playlist.Songs)
            {
                message.AppendLine($"- {song.Title} by {song.Artist} ({song.Album})");
            }

            
            EmailHelper.ShareViaEmail(email, "A Playlist Shared With You", message.ToString());
        }

        public static string GetSongLyrics(int songId)
        {
            var repo = dataAccessFactory.GetRepo();

            
            var song = repo.GetAll()
                .SelectMany(p => p.Songs) 
                .FirstOrDefault(s => s.SongId == songId); 

            if (song == null)
                throw new Exception("Song not found");

            return song.Lyrics; 
        }
        public static string GetSongLyricsByTitle(string title)
        {
            var repo = dataAccessFactory.GetRepo();


            var song = repo.GetAll()
                .SelectMany(p => p.Songs)
                .FirstOrDefault(s => s.Title != null &&
                                     s.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (song == null)
                throw new Exception("Song not found");

            return song.Lyrics;
        }
        public static List<SongDTO> ShufflePlaylist(int playlistId)
        {
            var repo = dataAccessFactory.GetRepo();
            var playlist = repo.GetById(playlistId);

            if (playlist == null)
                throw new Exception("Playlist not found");

            
            var shuffledSongs = playlist.Songs.OrderBy(s => Guid.NewGuid()).ToList();

            
            return shuffledSongs.Select(s => new SongDTO
            {
                SongId = s.SongId,
                Title = s.Title,
                Artist = s.Artist,
                Album = s.Album
            }).ToList();
        }

        
        public static List<SongDTO> RepeatPlaylist(int playlistId)
        {
            var repo =  dataAccessFactory.GetRepo();
            var playlist = repo.GetById(playlistId);

            if (playlist == null)
                throw new Exception("Playlist not found");

            
            return playlist.Songs.Select(s => new SongDTO
            {
                SongId = s.SongId,
                Title = s.Title,
                Artist = s.Artist,
                Album = s.Album
            }).ToList();
        }

        
        public static SongDTO RepeatSong(int songId)
        {
            var repo = dataAccessFactory.GetRepo();
            var song = repo.GetAll()
                .SelectMany(p => p.Songs)
                .FirstOrDefault(s => s.SongId == songId);

            if (song == null)
                throw new Exception("Song not found");

            
            return new SongDTO
            {
                SongId = song.SongId,
                Title = song.Title,
                Artist = song.Artist,
                Album = song.Album
            };
        }
    }
}
