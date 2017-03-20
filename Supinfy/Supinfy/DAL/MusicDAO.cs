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
        public static Music GetMusicByTrackId(int trackId)
        {
            return DataContext.Instance.Musics.FirstOrDefault(m => m.TrackId == trackId);
        }

        public static void AddMusic(Music music)
        {
            music.CreatedDate = DateTime.Now;
            DataContext.Instance.Musics.Add(music);
            DataContext.Instance.SaveChanges();
        }
        public static void IncrementMusic(int trackId)
        {
            Music music = DataContext.Instance.Musics.First(m => m.TrackId == trackId);
            music.PlayCount += 1;
            DataContext.Instance.SaveChanges();
        }
    }
}