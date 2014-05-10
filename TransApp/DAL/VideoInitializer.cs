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
                    videoCategory = "Hasar",
                    videoName = "Hackers",
                    videoTime = DateTime.Parse("2013-03-03")
                },
                new Video
                {
                    videoCategory = "Hasar",
                    videoName = "The Matrix",
                    videoTime = DateTime.Parse("2009-01-12")
                },
                new Video
                {
                    videoCategory = "Ævintýra",
                    videoName = "Hitchikers guide to the galaxy",
                    videoTime = DateTime.Parse("2010-10-23")
                },
                new Video
                {
                    videoCategory = "Ævintýra",
                    videoName = "Lord of the rings: Fellowship of the ring",
                    videoTime = DateTime.Parse("2006-01-01")
                },
                new Video
                {
                    videoCategory = "Rómantík",
                    videoName = "P.S I love you",
                    videoTime = DateTime.Parse("2003-03-13")
                },
                new Video
                {
                    videoCategory = "Rómantík",
                    videoName = "Notebook",
                    videoTime = DateTime.Parse("2003-03-13")
                },
                new Video
                {
                    videoCategory = "Rómantík",
                    videoName = "Titanic",
                    videoTime = DateTime.Parse("2003-03-13")
                },
                new Video
                {
                    videoCategory = "Rómantík",
                    videoName = "True lies",
                    videoTime = DateTime.Parse("2003-03-13")
                },
                new Video
                {
                    videoCategory = "Rómantík",
                    videoName = "Glaestar vonir",
                    videoTime = DateTime.Parse("2003-03-13")
                },
                new Video
                {
                    videoCategory = "Rómantík",
                    videoName = "P",
                    videoTime = DateTime.Parse("2003-03-13")
                },
                new Video
                {
                    videoCategory = "Rómantík",
                    videoName = "I love you",
                    videoTime = DateTime.Parse("2003-03-13")
                },
                new Video
                {
                    videoCategory = "Rómantík",
                    videoName = "P.S I you",
                    videoTime = DateTime.Parse("2003-03-13")
                },
                  new Video
                {
                    videoCategory = "Rómantík",
                    videoName = "P.S I you",
                    videoTime = DateTime.Parse("2003-03-13")
                },
                  new Video
                {
                    videoCategory = "Rómantík",
                    videoName = "P.S you",
                    videoTime = DateTime.Parse("2003-03-13")
                },
                  new Video
                {
                    videoCategory = "Rómantík",
                    videoName = "P.S I",
                    videoTime = DateTime.Parse("2003-03-13")
                },
            };

            vids.ForEach(a => context.videos.Add(a));
            context.SaveChanges();
        }
    }
}