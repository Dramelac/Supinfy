using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Supinfy.Models;

namespace Supinfy.ViewModel
{
    public class PlaylistVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string OwnerName { get; set; }

        public int MusicCount { get; set; }

        public DateTime CreationDate { get; set; }

        public List<MusicVM> MusicList { get; set; }

        public bool IsOwner { get; set; }

        public static PlaylistVM ToVM(Playlist model)
        {
            var result = new PlaylistVM
            {
                Id = model.Id,
                MusicCount = model.Musics.Count,
                Name = model.Name,
                OwnerName = model.Owner.Nickname,
                CreationDate = model.CreatedDate,
                IsOwner = false,
                MusicList = new List<MusicVM>()
            };
            foreach (var music in model.Musics)
            {
                result.MusicList.Add(MusicVM.ToVM(music));
            }
            return result;
        }
    }
}