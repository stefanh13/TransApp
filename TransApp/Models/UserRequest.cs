using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TransApp.Models
{
    /// <summary>
    /// This class keeps hold of the requests for that the user sends in.
    /// </summary>
    public class UserRequest
    {
        public int ID { get; set; }
        
        public string userName { get; set; }
        
        [Required(ErrorMessage="Settu inn titil")]
        public string requestName { get; set; }
        
        public string requestText { get; set; }
        
        [Required(ErrorMessage = "Veldu tungumál")]
        public string requestLanguage { get; set; }
        
        public int likes { get; set; }
        
        public DateTime requestTime { get; set; }

        public UserRequest()
        {
            requestTime = DateTime.Now;
        }
    }
}