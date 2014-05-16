using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransApp.Models
{
    /// <summary>
    /// This class keeps track of the videos that keep hold of the translations.
    /// </summary>
    public class Video
    {
        public int ID { get; set; }
        //public int catID { get; set; }

        public string videoCategory { get; set; }

        public string videoName { get; set; }
        public DateTime videoTime { get; set; }

    }
}