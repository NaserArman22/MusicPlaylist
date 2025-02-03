using BLL.DTOs;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace musicPlayer.Controllers
{
    public class PlaylistController : ApiController
    {
        [HttpGet]
        [Route("api/playlist/all")]
        public HttpResponseMessage GetAll()
        {
            var playlists = PlaylistService.Get();
            return Request.CreateResponse(HttpStatusCode.OK, playlists);
        }

        
        [HttpGet]
        [Route("api/playlist/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            var playlist = PlaylistService.GetById(id);
            if (playlist == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Playlist not found");

            return Request.CreateResponse(HttpStatusCode.OK, playlist);
        }

        
        [HttpPost]
        [Route("api/playlist/create")]
        public HttpResponseMessage Create([FromBody] PlaylistDTO playlistDTO)
        {
            if (playlistDTO == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid playlist data");

            PlaylistService.Create(playlistDTO);
            return Request.CreateResponse(HttpStatusCode.OK, "Playlist created successfully");
        }

        
        [HttpDelete]
        [Route("api/playlist/delete/{id}")]
        public HttpResponseMessage Delete(int id)
        {
            var playlist = PlaylistService.GetById(id);
            if (playlist == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Playlist not found");

            PlaylistService.Delete(id);
            return Request.CreateResponse(HttpStatusCode.OK, "Playlist deleted successfully");
        }

        
        [HttpPost]
        [Route("api/playlist/{playlistId}/addsong")]
        public HttpResponseMessage AddSongToPlaylist(int playlistId, [FromBody] SongDTO songDTO)
        {
            if (songDTO == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid song data");

            var playlist = PlaylistService.GetById(playlistId);
            if (playlist == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Playlist not found");

            PlaylistService.AddSongToPlaylist(playlistId, songDTO);
            return Request.CreateResponse(HttpStatusCode.OK, "Song added to playlist successfully");
        }

        
        [HttpGet]
        [Route("api/playlist/searchbyname/{name}")]
        public HttpResponseMessage SearchByName(string name)
        {
            var playlists = PlaylistService.SearchByName(name);
            if (!playlists.Any())
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No playlists found matching the name");

            return Request.CreateResponse(HttpStatusCode.OK, playlists);
        }

        
        [HttpGet]
        [Route("api/playlist/{playlistId}/searchsongs/{title}")]
        public HttpResponseMessage SearchSongsInPlaylist(int playlistId, string title)
        {
            var songs = PlaylistService.SearchSongsInPlaylist(playlistId, title);
            if (!songs.Any())
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No songs found matching the title in the playlist");

            return Request.CreateResponse(HttpStatusCode.OK, songs);
        }
        [HttpGet]
        [Route("song/byname/{songName}")]
        public HttpResponseMessage GetSongDetailsByName(string songName)
        {
            var song = PlaylistService.GetSongDetailsByName(songName);
            if (song == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Song not found");

            return Request.CreateResponse(HttpStatusCode.OK, song);
        }

        [HttpPost]
        [Route("share/email/{playlistId}")]
        public HttpResponseMessage SharePlaylistViaEmail(int playlistId, [FromBody] string email)
        {
            try
            {
                PlaylistService.SharePlaylistViaEmail(playlistId, email);
                return Request.CreateResponse(HttpStatusCode.OK, "Playlist shared via email successfully");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [HttpGet]
        [Route("song/{songId}/lyrics")]
        public HttpResponseMessage GetSongLyrics(int songId)
        {
            try
            {
                var lyrics = PlaylistService.GetSongLyrics(songId);
                return Request.CreateResponse(HttpStatusCode.OK, new { Lyrics = lyrics });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
        [HttpGet]
        [Route("song/lyrics/title/{title}")]
        public HttpResponseMessage GetSongLyricsByTitle(string title)
        {
            try
            {
                var lyrics = PlaylistService.GetSongLyricsByTitle(title);
                return Request.CreateResponse(HttpStatusCode.OK, new { Lyrics = lyrics });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }
        [HttpGet]
        [Route("shuffle/{playlistId}")]
        public HttpResponseMessage ShufflePlaylist(int playlistId)
        {
            try
            {
                var shuffledSongs = PlaylistService.ShufflePlaylist(playlistId);
                return Request.CreateResponse(HttpStatusCode.OK, shuffledSongs);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpGet]
        [Route("repeat/playlist/{playlistId}")]
        public HttpResponseMessage RepeatPlaylist(int playlistId)
        {
            try
            {
                var repeatedPlaylist = PlaylistService.RepeatPlaylist(playlistId);
                return Request.CreateResponse(HttpStatusCode.OK, repeatedPlaylist);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }

        [HttpGet]
        [Route("repeat/song/{songId}")]
        public HttpResponseMessage RepeatSong(int songId)
        {
            try
            {
                var repeatedSong = PlaylistService.RepeatSong(songId);
                return Request.CreateResponse(HttpStatusCode.OK, repeatedSong);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex.Message);
            }
        }



    }
}
