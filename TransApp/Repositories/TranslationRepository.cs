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

        public void Add(Translation t)
        {
            translationDb.translations.Add(t);
            Save();
        }

        public void Save()
        {
            translationDb.SaveChanges();
        }

        public void Update(Translation t)
        {
            foreach(var item in translationDb.translations)
            {
                if(item.ID == t.ID)
                {
                    item.vID = t.vID;
                }
            }
        }
    }
}