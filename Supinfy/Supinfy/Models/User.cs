using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Supinfy.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Nickname { get; set; }

        public string Password { get; set; }

        [StringLength(450)]
        [Index(IsUnique = true)]
        public string Email { get; set; }

        public DateTime CreationDateTime { get; set; }

        public Role Role { get; set; }

        public virtual ICollection<Playlist> Playlists { get; set; }

        public virtual ICollection<User> Friends { get; set; }

        public virtual ICollection<User> PendingFriends { get; set; }
    }
}