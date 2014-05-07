using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransApp.Models
{
    public class UserRequest
    {
        public int ID { get; set; }
        public int uID { get; set; }
        public string requestName { get; set; }
        public string requestText { get; set; }
        public string requestLanguage { get; set; }
        public int likes { get; set; }
        public DateTime requestTime { get; set; }
    }
}