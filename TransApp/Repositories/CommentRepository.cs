using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransApp.DAL;
using TransApp.Models;

namespace TransApp.Repositories
{
    public class CommentRepository
    {
        CommentContext commentDb = new CommentContext();

        public IEnumerable<Comment> GetAllComments()
        {
            return commentDb.comments;
        }
    }
}