using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransApp.DAL;
using TransApp.Models;

namespace TransApp.Repositories
{
    public class TranslationRepository
    {
        TranslationContext translationDb = new TranslationContext();

        public IEnumerable<Translation> GetAllTranslations()
        {
            return translationDb.translations;
        }
    }
}