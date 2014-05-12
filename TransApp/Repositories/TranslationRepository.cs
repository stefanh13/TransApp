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
            var translations = translationDb.translations.ToList();
            
            foreach(var item in translations)
            {
                if(item.ID == t.ID)
                {
                    item.translationText = t.translationText;
                    Save();
                    break;
                }
            }
        }

        public bool isIdValid(int? id)
        {
           if(id == null)
           {
               return false;
           }
           
           foreach(var translation in translationDb.translations)
           {
               if(translation.ID == id)
               {
                   return true;
               }
           }

           return false;
        }
    }
}