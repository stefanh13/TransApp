using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransApp.Models;

namespace TransApp.DAL
{
    public class TranslationInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TranslationContext>
    {
        protected override void Seed(TranslationContext context)
        {
            var translations = new List<Translation>
            {
                new Translation
                {
                    v = 1,
                    translationDescription = "Lorem ipsum",
                    translationText = "Lorem ipsum",
                    translationLanguage = "Enska",
                    translationTime = DateTime.Parse("2013-03-03")

                },
                new Translation
                {
                    v = 1,
                    translationDescription = "Lorem ipsum Loreum",
                    translationText = "Lorem ipsum ipsum",
                    translationLanguage = "Franska",
                    translationTime = DateTime.Parse("2013-03-03")

                },
                 new Translation
                {
                    v = 1,
                    translationDescription = "Lorem ipsum",
                    translationText = "Lorem ipsum",
                    translationLanguage = "Íslenska",
                    translationTime = DateTime.Parse("2013-03-03")

                },
                 new Translation
                {
                    v = 2,
                    translationDescription = "Lorem ipsum",
                    translationText = "Lorem ipsum",
                    translationLanguage = "Íslenska",
                    translationTime = DateTime.Parse("2013-03-03")

                },
                 new Translation
                {
                    v = 3,
                    translationDescription = "Lorem ipsum",
                    translationText = "Lorem ipsum",
                    translationLanguage = "Enska",
                    translationTime = DateTime.Parse("2013-03-03")

                },
                 new Translation
                {
                    v = 4,
                    translationDescription = "Lorem ipsum",
                    translationText = "Lorem ipsum",
                    translationLanguage = "Íslenska",
                    translationTime = DateTime.Parse("2013-03-03")

                },
            };

            translations.ForEach(t => context.translations.Add(t));
            context.SaveChanges();
        }
    }
}