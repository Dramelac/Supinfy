using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Supinfy.Models;

namespace Supinfy.DAL
{
    public static class PlaylistDAO
    {
        public static List<Playlist> GetAllPlaylistsFromUserId(Guid userId)
        {
            return DataContext.Instance.Playlists.Where(p => p.OwnerId == userId).ToList();
        }

        public static void CreatePlaylist(Playlist playlist)
        {
            DataContext.Instance.Playlists.Add(playlist);
            DataContext.Instance.SaveChanges();
        }

        public static Playlist GetPlaylist(Guid id)
        {
            return DataContext.Instance.Playlists.Find(id);
        }

        public static bool RemovePlaylist(Playlist playlist)
        {
            try
            {
                DataContext.Instance.Playlists.Remove(playlist);
                DataContext.Instance.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public static bool RemovePlaylist(Guid id)
        {
            var playlist = GetPlaylist(id);
            if (playlist == null) return false;
            return RemovePlaylist(playlist);
        }

        public static bool AddMusicToPlaylist(Guid playlistId, int trackId)
        {
            var playlist = GetPlaylist(playlistId);
            var music = DataContext.Instance.Musics.First(m => m.TrackId == trackId);
            if (playlist == null || music == null)
            {
                return false;
            }
            playlist.Musics.Add(music);
            DataContext.Instance.SaveChanges();
            return true;
        }

        public static bool RemoveMusicFromPlaylist(Guid playlistId, int trackId)
        {
            var playlist = GetPlaylist(playlistId);
            var music = DataContext.Instance.Musics.First(m => m.TrackId == trackId);
            if (playlist == null || music == null)
            {
                return false;
            }
            playlist.Musics.Remove(music);
            DataContext.Instance.SaveChanges();
            return true;
        }
    }
}
