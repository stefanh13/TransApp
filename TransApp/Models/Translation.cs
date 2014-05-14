using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransApp.Models
{
    public class Translation
    {
        public int ID { get; set; }
        public int vID { get; set; }
        [Required(ErrorMessage = "Vinsamlegast veldu flokk")]
        public string translationCategory { get; set; }
        [Required(ErrorMessage = "Vinsamlegast veldur nafn")]
        public string translationName { get; set; }
        [Required(ErrorMessage = "Vinsamlegast sláðu inn lýsingu")]
        public string translationDescription { get; set; }
        
        public string translationText { get; set; }
        [Required(ErrorMessage = "Vinsamlegast veldu tungumál")]
        public string translationLanguage { get; set; }
        public DateTime translationTime { get; set; }
        public int overallVotes { get; set; }
        public int voteCount { get; set; }
        public double averageVotes { get; set; }
        public int downloadCount { get; set; }
        public Translation()
        {
            translationTime = DateTime.Now;
            voteCount = 0;
            overallVotes = 0;
            downloadCount = 0;
            averageVotes = 0;
        }
    }
}