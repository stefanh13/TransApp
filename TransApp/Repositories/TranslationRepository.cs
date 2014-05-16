using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransApp.Models;

namespace TransApp.Repositories
{
    public class TranslationRepository
    {
        ApplicationDbContext translationDb = new ApplicationDbContext();

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

        public void Update(string translationText, int? id)
        {
            var translations = translationDb.translations.ToList();
            
            foreach(var item in translations)
            {
                if(item.ID == id)
                {
                    item.translationText = translationText;
                    item.translationTime = DateTime.Now;
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

        public void UpdateVotes(int votes, int id)
        {
            var translations = translationDb.translations.ToList();
            
            foreach(var item in translations)
            {
                if(item.ID == id)
                {
                    item.overallVotes += votes;
                    item.voteCount++;
                    item.averageVotes = Math.Round(Convert.ToDouble(item.overallVotes) / item.voteCount, 1);
                    Save();
                    break;
                }
            }
        }

        public void RaiseDownloads(int id)
        {
            var translations = translationDb.translations.ToList();

            foreach(var item in translations)
            {
                if(item.ID == id)
                {
                    item.downloadCount++;
                    Save();
                    break;
                }
            }
        }

        public Translation GetTranslationById(int? id)
        {
            Translation t = null;
            
            foreach(var item in translationDb.translations.ToList())
            {
                if(item.ID == id)
                {
                    t = item;
                }
            }

            return t;
        }
    }
}