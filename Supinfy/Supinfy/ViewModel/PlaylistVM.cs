using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Supinfy.ViewModel
{
    public class PlaylistVM
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string OwnerName { get; set; }

        public int MusicCount { get; set; }

        public DateTime CreationDate { get; set; }
    }
}