using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Supinfy.Models;
using Newtonsoft.Json.Linq;

namespace Supinfy.ViewModel
{
    public class MusicSearchVM
    {
        public HeadersAPIVM Headers { get; set; }

        public List<MusicAPIVM> Results { get; set; }

        public MusicVM ToVM(MusicAPIVM musicSearch)
        {
            return new MusicVM
            {
                Title = musicSearch.name,
                Artist = musicSearch.artist_name,
                TrackId = musicSearch.id,
                ArtworkUrl = musicSearch.image,
                TrackUrl = musicSearch.audio
            };
        }
    }
    public class HeadersAPIVM
    {
        public int code { get; set; }
        public string error_message { get; set; }
        public int results_count { get; set; }
        public string status { get; set; }
        public string warnings { get; set; }
    }

    public class MusicAPIVM
    {
        public int id { get; set; }
        public string name { get; set; }
        public string artist_name { get; set; }
        public string image { get; set; }
        public string audio { get; set; }
    }
}