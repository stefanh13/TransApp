using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransApp.Models;

namespace TransApp.Repositories
{
    public class CommentRepository
    {
        ApplicationDbContext commentDb = new ApplicationDbContext();

        public IEnumerable<Comment> GetAllComments()
        {
            return commentDb.comments;
        }
    
        public void AddComment(int id, string commentText, string userName)
        {
            Comment c = new Comment();

            c.commentText = commentText;
            c.tID = id;
            c.commentTime = DateTime.Now;
            c.userName = userName;

            commentDb.comments.Add(c);
            Save();

        }

        public void Save()
        {
            commentDb.SaveChanges();
        }
    }

}