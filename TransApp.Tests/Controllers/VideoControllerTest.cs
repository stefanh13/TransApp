using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransApp.Models;
using System.Collections.Generic;
using TransApp.Controllers;
using System.Web.Mvc;
using System.Linq;

namespace TransApp.Tests.Controllers
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TryToGetMoreThan10Translations()
        {
            // Arrange:
            List<Translation> translations = new List<Translation>();

            for(int i = 0; i < 13; i++)
            {
                translations.Add(new Translation
                {
                    ID = i,
                    vID = i % 3,
                    translationName = "Hackers",
                    translationCategory = "Hasar",
                    translationDescription = "Lorem ipsum",
                    translationLanguage = "English",
                    translationText = "Lorem ipsum",
                    translationTime = DateTime.Now.AddDays(i)
                });
            }

            var mockRepo = new Mocks.MockTranslationRepository(translations);
            var controller = new MockVideoController(mockRepo);

            // Act:
            // Get the translations with vId == 2.
            var result = controller.GetTranslationByVideoId(2, null, "");

            // Assert:
            var viewResult = (ViewResult)result;
            List<Translation> model = (viewResult.Model as IEnumerable<Translation>).ToList();

            foreach(var item in model)
            {
                Assert.IsTrue(item.vID == 2);
            }

            Assert.IsTrue(model.Count == 4);
        }

        [TestMethod]
        public void TryToGetLessThan10Translations()
        {
            // Arrange:
            List<Translation> translations = new List<Translation>();

            for(int i = 0; i < 7; i++)
            {
                translations.Add(new Translation
                {
                    ID = i,
                    vID = i % 3,
                    translationName = "Hackers",
                    translationCategory = "Hasar",
                    translationDescription = "Lorem ipsum",
                    translationLanguage = "English",
                    translationText = "Lorem ipsum",
                    translationTime = DateTime.Now.AddDays(i)
                });
            }
            var mockRepo = new Mocks.MockTranslationRepository(translations);
            var controller = new MockVideoController(mockRepo);
            // Act:
            // Get translations with vId == 2.
            var result = controller.GetTranslationByVideoId(2, null, "");
            
            // Assert:
            var viewResult = (ViewResult)result;
            List<Translation> model = (viewResult.Model as IEnumerable<Translation>).ToList();

            foreach(var item in model)
            {
                Assert.IsTrue(item.vID == 2);
            }

            Assert.IsTrue(model.Count == 2);
        }

        [TestMethod]
        public void TestIfTranslationsAreOrderedByDateDescendingAndCheckIfExactly10()
        {
            // Arrange:
            List<Translation> translations = new List<Translation>();

            for (int i = 0; i < 30; i++)
            {
                translations.Add(new Translation
                {
                    ID = i,
                    vID = i % 3,
                    translationName = "Hackers",
                    translationCategory = "Hasar",
                    translationDescription = "Lorem ipsum",
                    translationLanguage = "English",
                    translationText = "Lorem ipsum",
                    translationTime = DateTime.Now.AddDays(i)
                });
            }
            var mockRepo = new Mocks.MockTranslationRepository(translations);
            var controller = new MockVideoController(mockRepo);
        
            // Act:
            // Get translations ordered by date descending.
            var result = controller.GetTranslationByVideoId(1, null, "");

            // Assert:
            var viewResult = (ViewResult)result;
            List<Translation> model = (viewResult.Model as IEnumerable<Translation>).ToList();

            Assert.IsTrue(model.Count == 10);
          
            for(int i = 0; i < model.Count - 1; i++)
            {
                Assert.IsTrue(model[i].translationTime >= model[i + 1].translationTime);
            }
        }

        [TestMethod]
        public void TestIfTranslationsAreOrderedByDateAscendingAndCheckIfExactly10()
        {
            // Arrange:
            List<Translation> translations = new List<Translation>();

            for (int i = 0; i < 30; i++)
            {
                translations.Add(new Translation
                {
                    ID = i,
                    vID = i % 3,
                    translationName = "Hackers",
                    translationCategory = "Hasar",
                    translationDescription = "Lorem ipsum",
                    translationLanguage = "English",
                    translationText = "Lorem ipsum",
                    translationTime = DateTime.Now.AddDays(i)
                });
            }
            var mockRepo = new Mocks.MockTranslationRepository(translations);
            var controller = new MockVideoController(mockRepo);

            // Act:
            // Get translations ordered by date ascending.
            var resultAscending = controller.GetTranslationByVideoId(1, null, "Date");

            // Assert:
            var viewResultAscending = (ViewResult)resultAscending;
            List<Translation> modelAscending = (viewResultAscending.Model as IEnumerable<Translation>).ToList();
            
            Assert.IsTrue(modelAscending.Count == 10);
            for (int i = 0; i < modelAscending.Count - 1; i++)
            {
                Assert.IsTrue(modelAscending[i].translationTime <= modelAscending[i + 1].translationTime);
            }
        }

        [TestMethod]
        public void TestIfTranslationsAreOrderedByLanguageAscending()
        {
            // Arrange:
            List<Translation> translations = new List<Translation>();

            for (int i = 0; i < 30; i++)
            {
                translations.Add(new Translation
                {
                    ID = i,
                    vID = i % 3,
                    translationName = "Hackers",
                    translationCategory = "Hasar",
                    translationDescription = "Lorem ipsum",
                    translationLanguage = "English" + i.ToString(),
                    translationText = "Lorem ipsum",
                    translationTime = DateTime.Now.AddDays(i)
                });
            }
            var mockRepo = new Mocks.MockTranslationRepository(translations);
            var controller = new MockVideoController(mockRepo);

            // Act:
            // Get translations ordered by language ascending.
            var resultAscending = controller.GetTranslationByVideoId(1, null, "Language");

            // Assert:
            var viewResultAscending = (ViewResult)resultAscending;
            List<Translation> modelAscending = (viewResultAscending.Model as IEnumerable<Translation>).ToList();

            for (int i = 0; i < modelAscending.Count - 1; i++)
            {
                int compare = String.Compare(modelAscending[i].translationLanguage, modelAscending[i + 1].translationLanguage);
                Assert.IsTrue(compare <= 0);
            }
        }

        [TestMethod]
        public void TestIfTranslationsAreOrderedByLanguageDescending()
        {
            // Arrange:
            List<Translation> translations = new List<Translation>();

            for (int i = 0; i < 30; i++)
            {
                translations.Add(new Translation
                {
                    ID = i,
                    vID = i % 3,
                    translationName = "Hackers",
                    translationCategory = "Hasar",
                    translationDescription = "Lorem ipsum",
                    translationLanguage = "English" + i.ToString(),
                    translationText = "Lorem ipsum",
                    translationTime = DateTime.Now.AddDays(i)
                });
            }
            var mockRepo = new Mocks.MockTranslationRepository(translations);
            var controller = new MockVideoController(mockRepo);

            // Act:
            // Get translations ordered by language descending.
            var result = controller.GetTranslationByVideoId(1, null, "lang_desc");

            // Assert:
            var viewResult = (ViewResult)result;
            List<Translation> model = (viewResult.Model as IEnumerable<Translation>).ToList();

            for (int i = 0; i < model.Count - 1; i++)
            {
                int compare = String.Compare(model[i].translationLanguage, model[i + 1].translationLanguage);
                Assert.IsTrue(compare >= 0);
            }
        }

        [TestMethod]
        public void TestIfTranslationsAreOrderedByDownloadCountAscending()
        {
            // Arrange:
            List<Translation> translations = new List<Translation>();

            for (int i = 0; i < 30; i++)
            {
                translations.Add(new Translation
                {
                    ID = i,
                    vID = i % 3,
                    translationName = "Hackers",
                    translationCategory = "Hasar",
                    translationDescription = "Lorem ipsum",
                    translationLanguage = "English" + i.ToString(),
                    translationText = "Lorem ipsum",
                    downloadCount = i,
                    translationTime = DateTime.Now.AddDays(i)
                });
            }
            var mockRepo = new Mocks.MockTranslationRepository(translations);
            var controller = new MockVideoController(mockRepo);

            // Act:
            // Get translations ordered by count ascending.
            var resultAscending = controller.GetTranslationByVideoId(1, null, "Count");

            // Assert:
            var viewResultAscending = (ViewResult)resultAscending;
            List<Translation> modelAscending = (viewResultAscending.Model as IEnumerable<Translation>).ToList();

            for (int i = 0; i < modelAscending.Count - 1; i++)
            {
                Assert.IsTrue(modelAscending[i].downloadCount <= modelAscending[i + 1].downloadCount);
            }
        }

        [TestMethod]
        public void TestIfTranslationsAreOrderedByDownloadCountDescending()
        {
            // Arrange:
            List<Translation> translations = new List<Translation>();

            for (int i = 0; i < 30; i++)
            {
                translations.Add(new Translation
                {
                    ID = i,
                    vID = i % 3,
                    translationName = "Hackers",
                    translationCategory = "Hasar",
                    translationDescription = "Lorem ipsum",
                    translationLanguage = "English" + i.ToString(),
                    translationText = "Lorem ipsum",
                    downloadCount = i,
                    translationTime = DateTime.Now.AddDays(i)
                });
            }
            var mockRepo = new Mocks.MockTranslationRepository(translations);
            var controller = new MockVideoController(mockRepo);

            // Act:
            // Get translations ordered by count descending.
            var result = controller.GetTranslationByVideoId(1, null, "count_desc");

            // Assert:
            var viewResult = (ViewResult)result;
            List<Translation> model = (viewResult.Model as IEnumerable<Translation>).ToList();

            for (int i = 0; i < model.Count - 1; i++)
            {
                Assert.IsTrue(model[i].downloadCount >= model[i + 1].downloadCount);
            }
        }

        [TestMethod]
        public void TestIfTranslationsAreOrderedByAverageVotesAscending()
        {
            // Arrange:
            List<Translation> translations = new List<Translation>();

            for (int i = 0; i < 30; i++)
            {
                translations.Add(new Translation
                {
                    ID = i,
                    vID = i % 3,
                    translationName = "Hackers",
                    translationCategory = "Hasar",
                    translationDescription = "Lorem ipsum",
                    translationLanguage = "English" + i.ToString(),
                    translationText = "Lorem ipsum",
                    averageVotes = Convert.ToDouble(i),
                    translationTime = DateTime.Now.AddDays(i)
                });
            }
            var mockRepo = new Mocks.MockTranslationRepository(translations);
            var controller = new MockVideoController(mockRepo);

            // Act:
            // Get translations ordered by average votes ascending.
            var resultAscending = controller.GetTranslationByVideoId(1, null, "Average");

            // Assert:
            var viewResultAscending = (ViewResult)resultAscending;
            List<Translation> modelAscending = (viewResultAscending.Model as IEnumerable<Translation>).ToList();

            for (int i = 0; i < modelAscending.Count - 1; i++)
            {
                Assert.IsTrue(modelAscending[i].averageVotes <= modelAscending[i + 1].averageVotes);
            }
        }

        [TestMethod]
        public void TestIfTranslationsAreOrderedByAverageVotesDescending()
        {
            // Arrange:
            List<Translation> translations = new List<Translation>();

            for (int i = 0; i < 30; i++)
            {
                translations.Add(new Translation
                {
                    ID = i,
                    vID = i % 3,
                    translationName = "Hackers",
                    translationCategory = "Hasar",
                    translationDescription = "Lorem ipsum",
                    translationLanguage = "English" + i.ToString(),
                    translationText = "Lorem ipsum",
                    averageVotes = Convert.ToDouble(i),
                    translationTime = DateTime.Now.AddDays(i)
                });
            }
            var mockRepo = new Mocks.MockTranslationRepository(translations);
            var controller = new MockVideoController(mockRepo);

            // Act:
            // Get translations ordered by average votes descending.
            var result = controller.GetTranslationByVideoId(1, null, "aver_desc");

            // Assert:
            var viewResult = (ViewResult)result;
            List<Translation> model = (viewResult.Model as IEnumerable<Translation>).ToList();

            for (int i = 0; i < model.Count - 1; i++)
            {
                Assert.IsTrue(model[i].averageVotes >= model[i + 1].averageVotes);
            }
        }

        [TestMethod]
        public void TestGettingTranslationById()
        {
            // Arrange:
            List<Translation> translations = new List<Translation>();

            for (int i = 0; i < 15; i++)
            {
                translations.Add(new Translation
                {
                    ID = i,
                    vID = i % 3,
                    translationName = "Hackers",
                    translationCategory = "Hasar",
                    translationDescription = "Lorem ipsum",
                    translationLanguage = "English" + i.ToString(),
                    translationText = "Lorem ipsum",
                    averageVotes = Convert.ToDouble(i),
                    translationTime = DateTime.Now.AddDays(i)
                });
            }
            var mockRepo = new Mocks.MockTranslationRepository(translations);
            var controller = new MockVideoController(mockRepo);

            // Act:
            // Get translation with id == 7.
            var result = controller.GetTranslationById(7);

            // Assert:
            var viewResult = (ViewResult)result;
            List<Translation> model = (viewResult.Model as IEnumerable<Translation>).ToList();

            Assert.IsTrue(model.Count == 1);
            Assert.IsTrue(model[0].ID == 7);
        }

        [TestMethod]
        public void TestGettingMoreThan10Videos()
        {
            // Arrange:
            List<Video> videos = new List<Video>();

            for(int i = 0; i < 13; i++)
            {
                videos.Add(new Video
                {
                    ID = i,
                    videoCategory = "Hasar",
                    videoTime = DateTime.Now.AddDays(i),
                    videoName = "Video " + i.ToString()
                });
            }

            var mockRepo = new Mocks.MockVideoRepository(videos);
            var controller = new MockVideoController(mockRepo);

            // Act:
            var result = controller.GetVideos(null, "");

            // Assert:
            var viewResult = (ViewResult)result;
            List<Video> model = (viewResult.Model as IEnumerable<Video>).ToList();

            Assert.IsTrue(model.Count == 10);
        }

        [TestMethod]
        public void TestGettingLessThan10Videos()
        {
            // Arrange:
            List<Video> videos = new List<Video>();

            for (int i = 0; i < 7; i++)
            {
                videos.Add(new Video
                {
                    ID = i,
                    videoCategory = "Hasar",
                    videoTime = DateTime.Now.AddDays(i),
                    videoName = "Video " + i.ToString()
                });
            }

            var mockRepo = new Mocks.MockVideoRepository(videos);
            var controller = new MockVideoController(mockRepo);

            // Act:
            var result = controller.GetVideos(null, "");

            // Assert:
            var viewResult = (ViewResult)result;
            List<Video> model = (viewResult.Model as IEnumerable<Video>).ToList();

            Assert.IsTrue(model.Count == 7);
        }

        [TestMethod]
        public void TestOrderingVideosByDateDescending()
        {
            // Arrange:
            List<Video> videos = new List<Video>();

            for (int i = 0; i < 10; i++)
            {
                videos.Add(new Video
                {
                    ID = i,
                    videoCategory = "Hasar",
                    videoTime = DateTime.Now.AddDays(i),
                    videoName = "Video " + i.ToString()
                });
            }

            var mockRepo = new Mocks.MockVideoRepository(videos);
            var controller = new MockVideoController(mockRepo);

            // Act:
            // Get videos ordered by date descending.
            var result = controller.GetVideos(null, "");

            // Assert:
            var viewResult = (ViewResult)result;
            List<Video> model = (viewResult.Model as IEnumerable<Video>).ToList();

            for(int i = 0; i < model.Count - 1; i++)
            {
                Assert.IsTrue(model[i].videoTime >= model[i + 1].videoTime);
            }
        }

        [TestMethod]
        public void TestOrderingVideosByDateAscending()
        {
            // Arrange:
            List<Video> videos = new List<Video>();

            for (int i = 0; i < 10; i++)
            {
                videos.Add(new Video
                {
                    ID = i,
                    videoCategory = "Hasar",
                    videoTime = DateTime.Now.AddDays(i),
                    videoName = "Video " + i.ToString()
                });
            }

            var mockRepo = new Mocks.MockVideoRepository(videos);
            var controller = new MockVideoController(mockRepo);

            // Act:
            // Get videos ordered by date ascending.
            var result = controller.GetVideos(null, "Date");

            // Assert:
            var viewResult = (ViewResult)result;
            List<Video> model = (viewResult.Model as IEnumerable<Video>).ToList();

            for (int i = 0; i < model.Count - 1; i++)
            {
                Assert.IsTrue(model[i].videoTime <= model[i + 1].videoTime);
            }
        }

        [TestMethod]
        public void TestOrderingVideosByNameDescending()
        {
            // Arrange:
            List<Video> videos = new List<Video>();

            for (int i = 0; i < 10; i++)
            {
                videos.Add(new Video
                {
                    ID = i,
                    videoCategory = "Hasar",
                    videoTime = DateTime.Now.AddDays(i),
                    videoName = "Video " + i.ToString()
                });
            }

            var mockRepo = new Mocks.MockVideoRepository(videos);
            var controller = new MockVideoController(mockRepo);

            // Act:
            // Get videos ordered by name descending.
            var result = controller.GetVideos(null, "name_desc");

            // Assert:
            var viewResult = (ViewResult)result;
            List<Video> model = (viewResult.Model as IEnumerable<Video>).ToList();

            for (int i = 0; i < model.Count - 1; i++)
            {
                int compare = String.Compare(model[i].videoName, model[i + 1].videoName);
                Assert.IsTrue(compare >= 0);
            }
        }

        [TestMethod]
        public void TestOrderingVideosByNameAscending()
        {
            // Arrange:
            List<Video> videos = new List<Video>();

            for (int i = 0; i < 10; i++)
            {
                videos.Add(new Video
                {
                    ID = i,
                    videoCategory = "Hasar",
                    videoTime = DateTime.Now.AddDays(i),
                    videoName = "Video " + i.ToString()
                });
            }

            var mockRepo = new Mocks.MockVideoRepository(videos);
            var controller = new MockVideoController(mockRepo);

            // Act:
            // Get videos ordered by name ascending.
            var result = controller.GetVideos(null, "Name");

            // Assert:
            var viewResult = (ViewResult)result;
            List<Video> model = (viewResult.Model as IEnumerable<Video>).ToList();

            for (int i = 0; i < model.Count - 1; i++)
            {
                int compare = String.Compare(model[i].videoName, model[i + 1].videoName);
                Assert.IsTrue(compare <= 0);
            }
        }

        [TestMethod]
        public void TestGettingVideoByCategory()
        {
            // Arrange:
            List<Video> videos = new List<Video>();

            for (int i = 0; i < 20; i++)
            {
                videos.Add(new Video
                {
                    ID = i,
                    videoCategory = "Hasar" + (i % 3).ToString(),
                    videoTime = DateTime.Now.AddDays(i),
                    videoName = "Video " + i.ToString()
                });
            }

            var mockRepo = new Mocks.MockVideoRepository(videos);
            var controller = new MockVideoController(mockRepo);

            // Act:
            // Get videos with category == Hasar1.
            var result = controller.GetVideoByCategory("Hasar1", null, "");

            // Assert:
            var viewResult = (ViewResult)result;
            List<Video> model = (viewResult.Model as IEnumerable<Video>).ToList();

            foreach(var item in model)
            {
                Assert.IsTrue(item.videoCategory == "Hasar1");
            }
        }

        [TestMethod]
        public void TestSearchingForExactName()
        {
            // Arrange:
            List<Video> videos = new List<Video>();

            for (int i = 0; i < 20; i++)
            {
                videos.Add(new Video
                {
                    ID = i,
                    videoCategory = "Hasar" + (i % 3).ToString(),
                    videoTime = DateTime.Now.AddDays(i),
                    videoName = "Video " + (i % 3).ToString()
                });
            }

            var mockRepo = new Mocks.MockVideoRepository(videos);
            var controller = new MockVideoController(mockRepo);

            // Act:
            var result = controller.SearchEngine("Video 1", "", null, "");

            // Assert:
            var viewResult = (ViewResult)result;
            List<Video> model = (viewResult.Model as IEnumerable<Video>).ToList();

            foreach (var item in model)
            {
                Assert.IsTrue(item.videoName == "Video 1");
            }
        }

        [TestMethod]
        public void TestSearchingForUpperAndLowerCaseNames()
        {
            // Arrange:
            List<Video> videos = new List<Video>();

            for (int i = 0; i < 20; i++)
            {
                videos.Add(new Video
                {
                    ID = i,
                    videoCategory = "Hasar" + (i % 3).ToString(),
                    videoTime = DateTime.Now.AddDays(i),
                    videoName = i % 2 == 0 ? "Hackers" : "hackers"

                });
            }

            var mockRepo = new Mocks.MockVideoRepository(videos);
            var controller = new MockVideoController(mockRepo);

            // Act:
            var result = controller.SearchEngine("hackers", "", null, "");

            // Assert:
            var viewResult = (ViewResult)result;
            List<Video> model = (viewResult.Model as IEnumerable<Video>).ToList();

            foreach (var item in model)
            {
                Assert.IsTrue(item.videoName.ToLower() == "hackers");
            }
        }

    }
}
