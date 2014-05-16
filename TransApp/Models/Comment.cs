using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransApp.Models
{
    /// <summary>
    /// This class keeps track of the users comments.
    /// </summary>
    public class Comment
    {
        public int ID { get; set; }
        
        public string userName { get; set; }
        
        public int tID { get; set; }
        
        public string commentText { get; set; }
        
        public DateTime commentTime { get; set; }
    }
}