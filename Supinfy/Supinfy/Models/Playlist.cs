using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Supinfy.Models
{
    public class Playlist
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Music> Musics { get; set; }

        public virtual User Owner { get; set; }

        [ForeignKey("Owner")]
        public Guid OwnerId { get; set; }

    }
}