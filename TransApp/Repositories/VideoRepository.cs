using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransApp.DAL;
using TransApp.Models;

namespace TransApp.Repositories
{
    public class VideoRepository
    {

        VideoContext videoDb = new VideoContext();

        public IEnumerable<Video> GetAllVideos()
        {
            return videoDb.videos;
        }

        public void Add(Video v)
        {
            videoDb.videos.Add(v);
            Save();
        }

        public void Save()
        {
            videoDb.SaveChanges();
        }

        public void UpdateVideoTime(Video v)
        {
            foreach (var item in videoDb.videos)
            {
                if (item.ID == v.ID)
                {
                    item.videoTime = v.videoTime;
                    break;
                }
            }
        }
    }
}