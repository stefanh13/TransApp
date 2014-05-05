using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransApp.Models;
using TransApp.Repositories;

namespace TransApp.Tests.Mocks
{
    class MockTranslationRepository : ITranslationRepository
    {

        private readonly List<Translation> translations;
        
        public MockTranslationRepository(List<Translation> transl)
        {
            translations = transl;
        }

        public IQueryable<Models.Translation> GetTranslations()
        {
            return translations.AsQueryable();
        }
    }
}
