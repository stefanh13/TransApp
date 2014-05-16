using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransApp.Models;

namespace TransApp.DAL
{
   /* public class CommentInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<CommentContext>
    {
        protected override void Seed(CommentContext context)
        {
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
                    tID = 6,
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
                    userName = "Rassfés",
                    commentText = "miiiuiuiui",
                    commentTime = DateTime.Parse("2014-01-17")
                },
            };

            comments.ForEach(c => context.comments.Add(c));
            context.SaveChanges();
        }
    }*/
}