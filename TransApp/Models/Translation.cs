using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransApp.Models
{
    public class Translation
    {
        public int tID { get; set; }
        public int vID { get; set; }
        public string translationDescription { get; set; }
        public string translationText { get; set; }
        public string translationLanguage { get; set; }
        public DateTime translationTime { get; set; }
    }
}