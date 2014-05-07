using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransApp.Models;

namespace TransApp.DAL
{
    public class VideoInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<VideoContext>
    {
        protected override void Seed(VideoContext context)
        {
            var vids = new List<Video>
            {
                new Video
                {
                    catID = 1,
                    videoName = "Hackers",
                    videoTime = DateTime.Parse("2013-03-03")
                },
                new Video
                {
                    catID = 1,
                    videoName = "The Matrix",
                    videoTime = DateTime.Parse("2009-01-12")
                },
                new Video
                {
                    catID = 2,
                    videoName = "Hitchikers guide to the galaxy",
                    videoTime = DateTime.Parse("2010-10-23")
                },
                new Video
                {
                    catID = 3,
                    videoName = "Lord of the rings: Fellowship of the ring",
                    videoTime = DateTime.Parse("2006-01-01")
                },
                new Video
                {
                    catID = 4,
                    videoName = "P.S I love you",
                    videoTime = DateTime.Parse("2003-03-13")
                },
            };

            vids.ForEach(a => context.videos.Add(a));
            context.SaveChanges();
        }
    }
}