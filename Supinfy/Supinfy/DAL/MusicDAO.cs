using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Supinfy.Models;

namespace Supinfy.DAL
{
    public static class MusicDAO
    {
        public static Music GetMusic(Guid id)
        {
            return DataContext.Instance.Musics.Find(id);
        }

        public static void AddMusic(Music music)
        {
            DataContext.Instance.Musics.Add(music);
            DataContext.Instance.SaveChanges();
        }
    }
}