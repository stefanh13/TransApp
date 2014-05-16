namespace TransApp.Migrations
{
    using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using TransApp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TransApp.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TransApp.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            /*var vids = new List<Video>
            {
                new Video
                {
                    videoCategory = "Hasar",
                    videoName = "Hackers",
                    videoTime = DateTime.Parse("2013-03-03")
                },
                new Video
                {
                    videoCategory = "Hryllings",
                    videoName = "The Matrix",
                    videoTime = DateTime.Parse("2009-01-12")
                },
                new Video
                {
                    videoCategory = "Gaman",
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
            
            };

            vids.ForEach(a => context.videos.Add(a));
            context.SaveChanges();

            var translations = new List<Translation>
            {
                new Translation
                {
                    vID = 1,
                    translationDescription = "Lorem ipsum",
                    translationName = "Hackers",
                    translationCategory = "Hasar",
                    translationText = "Lorem ipsum",
                    translationLanguage = "Enska",
                    translationTime = DateTime.Parse("2013-03-03")

                },
                new Translation
                {
                    vID = 1,
                    translationDescription = "Lorem ipsum Loreum",
                    translationText = "Lorem ipsum ipsum",
                    translationName = "Hackers",
                    translationCategory = "Hasar",
                    translationLanguage = "Franska",
                    translationTime = DateTime.Parse("2013-08-03")

                },
                 new Translation
                {
                    vID = 2,
                    translationDescription = "Lorem ipsum",
                    translationText = "Lorem ipsum",
                    translationName = "The Matrix",
                    translationCategory = "Hryllings",
                    translationLanguage = "Franska",
                    translationTime = DateTime.Parse("2013-03-03")

                },
                 new Translation
                {
                    vID = 3,
                    translationDescription = "Lorem ipsum",
                    translationText = "Lorem ipsum",
                    translationName = "Hitchikers guide to the galaxy",
                    translationCategory = "Gaman",
                    translationLanguage = "Enska",
                    translationTime = DateTime.Parse("2013-03-03")

                },
                 new Translation
                {
                    vID = 4,
                    translationDescription = "Lorem ipsum",
                    translationText = "Lorem ipsum",
                    translationName = "Lord of the rings: Fellowship of the ring",
                    translationCategory = "Ævintýra",
                    translationLanguage = "Enska",
                    translationTime = DateTime.Parse("2013-03-03")

                },
                 new Translation
                {
                    vID = 5,
                    translationDescription = "Lorem ipsum",
                    translationText = "Lorem ipsum",
                    translationName = "P.S I love you",
                    translationCategory = "Rómantík",
                    translationLanguage = "Íslenska",
                    translationTime = DateTime.Parse("2013-03-03")

                },
            };

            translations.ForEach(t => context.translations.Add(t));
            context.SaveChanges();

            var comments = new List<Comment>
            {
                new Comment
                {
                    tID = 1,
                    userName = "Stefán",
                    commentText = "Wow this is amazing",
                    commentTime = DateTime.Parse("2014-01-17")
                },
                new Comment
                {
                    tID = 1,
                    userName = "Stefán",
                    commentText = "No I stand corrected this sux",
                    commentTime = DateTime.Parse("2014-02-17")
                },
                new Comment
                {
                    tID = 2,
                    userName = "Rikki",
                    commentText = "I think this is amazing",
                    commentTime = DateTime.Parse("2014-01-17")
                },
                new Comment
                {
                    tID = 2,
                    userName = "Rikki",
                    commentText = "Fluck you Rikki",
                    commentTime = DateTime.Parse("2014-03-17")
                },
                new Comment
                {
                    tID = 3,
                    userName = "Stefán",
                    commentText = "HAHAHAHAHAH",
                    commentTime = DateTime.Parse("2014-01-17")
                },
                new Comment
                {
                    tID = 4,
                    userName = "Daníel Brandur",
                    commentText = "Smuuuuu",
                    commentTime = DateTime.Parse("2013-01-17")
                },
                new Comment
                {
                    tID = 5,
                    userName = "Stefán",
                    commentText = "Wow this is amazing",
                    commentTime = DateTime.Parse("2014-01-17")
                },
                new Comment
                {
                    tID = 3,
                    userName = "Kristján",
                    commentText = "Þá fer ég bara heim og drep mig!",
                    commentTime = DateTime.Parse("2014-01-17")
                },
                new Comment
                {
                    tID = 3,
                    userName = "Kalli",
                    commentText = "Ég er ekki mikið fyrir þetta",
                    commentTime = DateTime.Parse("2014-01-17")
                },
                new Comment
                {
                    tID = 2,
                    userName = "Sæli",
                    commentText = "miiiuiuiui",
                    commentTime = DateTime.Parse("2014-01-17")
                },
            };

            comments.ForEach(c => context.comments.Add(c));
            context.SaveChanges();*/

            var req = new List<UserRequest>
            {
                new UserRequest{
                    userName = "Stefán",
                    requestName = "James Bond",
                    requestText = "mig vantar hjálp",
                    requestLanguage = "Enska",
                    likes = 8,
                    requestTime = DateTime.Parse("2014-02-28 15:00:00")
                },
                new UserRequest{
                    userName = "Rikki",
                    requestName = "Hobbit",
                    requestText = "Halló mig vantar hjálp",
                    requestLanguage = "Enska",
                    likes = 32,
                    requestTime = DateTime.Parse("2014-02-27 15:00:00")
                },
                new UserRequest{
                    userName = "Kristján",
                    requestName = "Men in Black",
                    requestText = "mig vantar hjálp",
                    requestLanguage = "Íslenska",
                    likes = 100,
                    requestTime = DateTime.Parse("2014-02-26 15:00:00")
                },
                new UserRequest{
                    userName = "Hafþór",
                    requestName = "Hobbit",
                    requestText = "mig vantar hjálp",
                    requestLanguage = "Franska",
                    likes = 10,
                    requestTime = DateTime.Parse("2014-02-25 15:00:00")
                },
                new UserRequest{
                    userName = "Sæli",
                    requestName = "Lord of the rings",
                    requestText = "what mig vantar hjálp",
                    requestLanguage = "Þýska",
                    likes = 2,
                    requestTime = DateTime.Parse("2014-02-24 15:00:00")
                },
            };

            req.ForEach(s => context.userRequest.Add(s));
            context.SaveChanges();
        }
    }
}
