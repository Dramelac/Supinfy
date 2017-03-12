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
            return DataContext.Instance.Playlist.Where(p => p.OwnerId == userId).ToList();
        }

        public static void CreatePlaylist(Playlist playlist)
        {
            DataContext.Instance.Playlist.Add(playlist);
            DataContext.Instance.SaveChanges();
        }
    }
}