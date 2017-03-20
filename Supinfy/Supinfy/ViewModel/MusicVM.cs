using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Supinfy.Models;

namespace Supinfy.ViewModel
{
    public class MusicVM
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Artist { get; set; }

        public int TrackId { get; set; }

        public string TrackUrl { get; set; }

        public string ArtworkUrl { get; set; }

        public DateTime CreatedDate { get; set; }

        public static MusicVM ToVM(Music model)
        {
            return new MusicVM
            {
                Id = model.Id,
                Title = model.Title,
                Artist = model.Artist,
                CreatedDate = model.CreatedDate,
                TrackId = model.TrackId,
                ArtworkUrl = model.ArtworkUrl,
                TrackUrl = model.TrackUrl
            };
        }
    }
}