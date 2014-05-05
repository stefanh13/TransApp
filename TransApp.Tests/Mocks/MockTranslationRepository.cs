using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransApp.Repositories;

namespace TransApp.Tests.Mocks
{
    class MockTranslationRepository : ITranslationRepository
    {

        public IQueryable<Models.Translation> GetTranslations()
        {
            throw new NotImplementedException();
        }
    }
}
