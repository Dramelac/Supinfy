using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Supinfy.Models;

namespace Supinfy.ViewModel
{
    public class PlaylistIndexVM
    {
        public PlaylistIndexVM()
        {
            Playlists = new List<PlaylistVM>();
        }
        public List<PlaylistVM> Playlists { get; set; }

        public static PlaylistIndexVM ToVM(ICollection<Playlist> model)
        {
            var vm = new PlaylistIndexVM();
            foreach (var playlist in model)
            {
                vm.Playlists.Add(new PlaylistVM
                {
                    Id = playlist.Id,
                    Name = playlist.Name,
                    MusicCount = playlist.Musics.Count,
                    OwnerName = playlist.Owner.Nickname,
                    CreationDate = playlist.CreatedDate
                });
            }
            return vm;
        }

    }
}