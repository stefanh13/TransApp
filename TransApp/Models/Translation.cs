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
        [Required(ErrorMessage = "Vinsamlegast sláðu inn þýðingu")]
        public string translationText { get; set; }
        [Required(ErrorMessage = "Vinsamlegast veldu tungumál")]
        public string translationLanguage { get; set; }
        public DateTime translationTime { get; set; }

         public Translation()
         {
             translationTime = DateTime.Now;
         }
    }
}