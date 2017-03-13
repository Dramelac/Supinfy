using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Supinfy.Models
{
    public class Music
    {
        public Music()
        {
            Playlists = new List<Playlist>();
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Title { get; set; }

        public int TrackId { get; set; }

        public string ArtworkUrl { get; set; }

        public int PlayCount { get; set; }

        public virtual ICollection<Playlist> Playlists { get; set; }
        
        public DateTime CreatedDate { get; set; }

    }
}