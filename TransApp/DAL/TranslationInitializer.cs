using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransApp.Models;

namespace TransApp.DAL
{
    /*public class TranslationInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TranslationContext>
    {
        protected override void Seed(TranslationContext context)
        {
            var translations = new List<Translation>
            {
                new Translation
                {
                    vID = 1,
                    translationDescription = "Lorem ipsum",
                    translationName = "Hackers",
                    translationCategory = "Hasar",
                    translationText = "Lorem ipsum",
                    translationLanguage = "Enska",
                    translationTime = DateTime.Parse("2013-03-03")

                },
                new Translation
                {
                    vID = 1,
                    translationDescription = "Lorem ipsum Loreum",
                    translationText = "Lorem ipsum ipsum",
                    translationName = "Hackers",
                    translationCategory = "Hasar",
                    translationLanguage = "Franska",
                    translationTime = DateTime.Parse("2013-03-03")

                },
                 new Translation
                {
                    vID = 1,
                    translationDescription = "Lorem ipsum",
                    translationText = "Lorem ipsum",
                    translationName = "Hackers",
                    translationCategory = "Hasar",
                    translationLanguage = "Íslenska",
                    translationTime = DateTime.Parse("2013-03-03")

                },
                 new Translation
                {
                    vID = 2,
                    translationDescription = "Lorem ipsum",
                    translationText = "Lorem ipsum",
                    translationName = "The Matrix",
                    translationCategory = "Hasar",
                    translationLanguage = "Íslenska",
                    translationTime = DateTime.Parse("2013-03-03")

                },
                 new Translation
                {
                    vID = 3,
                    translationDescription = "Lorem ipsum",
                    translationText = "Lorem ipsum",
                    translationName = "Hitchikers guide to the galaxy",
                    translationCategory = "Ævintýra",
                    translationLanguage = "Enska",
                    translationTime = DateTime.Parse("2013-03-03")

                },
                 new Translation
                {
                    vID = 4,
                    translationDescription = "Lorem ipsum",
                    translationText = "Lorem ipsum",
                    translationName = "Lord of the rings: Fellowship of the ring",
                    translationCategory = "Ævintýra",
                    translationLanguage = "Íslenska",
                    translationTime = DateTime.Parse("2013-03-03")

                },
            };

            translations.ForEach(t => context.translations.Add(t));
            context.SaveChanges();
        }
    }*/
}