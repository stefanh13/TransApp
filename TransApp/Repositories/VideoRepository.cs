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

    }
}