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

        // Get a playlist by ID
        [HttpGet]
        [Route("api/playlist/{id}")]
        public HttpResponseMessage GetById(int id)
        {
            var playlist = PlaylistService.GetById(id);
            if (playlist == null)
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Playlist not found");

            return Request.CreateResponse(HttpStatusCode.OK, playlist);
        }

        // Create a new playlist
        [HttpPost]
        [Route("api/playlist/create")]
        public HttpResponseMessage Create([FromBody] PlaylistDTO playlistDTO)
        {
            if (playlistDTO == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid playlist data");

            PlaylistService.Create(playlistDTO);
            return Request.CreateResponse(HttpStatusCode.OK, "Playlist created successfully");
        }

        // Delete a playlist by ID
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

        // Add a song to a playlist
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

        // Search playlists by name
        [HttpGet]
        [Route("api/playlist/searchbyname/{name}")]
        public HttpResponseMessage SearchByName(string name)
        {
            var playlists = PlaylistService.SearchByName(name);
            if (!playlists.Any())
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No playlists found matching the name");

            return Request.CreateResponse(HttpStatusCode.OK, playlists);
        }

        // Search songs in a specific playlist by title
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

    }
}
