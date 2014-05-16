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

        ApplicationDbContext videoDb = new ApplicationDbContext();

        public IEnumerable<Video> GetAllVideos()
        {
            return videoDb.videos;
        }

        public void AddVideo(Translation t)
        {
            Video newVideo = new Video();
            newVideo.videoName = t.translationName;
            newVideo.videoTime = t.translationTime;
            newVideo.videoCategory = t.translationCategory;
            videoDb.videos.Add(newVideo);
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

        public bool ContainsCategory(string category)
        {
            foreach(var item in videoDb.videos)
            {
                if(item.videoCategory == category)
                {
                    return true;
                }
            }

            return false;
        }

        public bool isIdValid(int? id)
        {
            
            foreach(var item in videoDb.videos)
            {
                if(item.ID == id)
                {
                    return true;
                }
            }


            return false;
        }
    }
}