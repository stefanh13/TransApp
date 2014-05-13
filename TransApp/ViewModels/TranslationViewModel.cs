using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransApp.Models;

namespace TransApp.ViewModels
{
    public class TranslationViewModel
    {
        public IEnumerable<Comment> Comments { get; set; }
        public Translation Translation { get; set; }
    }
}