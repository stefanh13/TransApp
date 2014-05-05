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
        public void TestAddingNewVideoInEmptyTable()
        {
            // Arrange:
            List<Video> video = new List<Video>();
            video.Add(new Video
            {
                vID = 1,
                catID = 1,
                videoName = "Kalli"
            });

            var mockRepo = new Mocks.MockVideoRepository(video);
            var controller = new VideoController(mockRepo);
            
            // Act:
            var result = controller.AddTranslation();

            // Assert:
            var viewResult = (ViewResult)result;
            List<Video> model = (viewResult.Model as IEnumerable<Video>).ToList();
            
            Assert.IsTrue(model.Count == 1);
        }

        [TestMethod]
        public void TestAddingManyVideosInTheTable()
        {
            // Arrange:
            List<Video> videos = new List<Video>();

            for(int i = 0; i < 10; i++)
            {
                videos.Add(new Video
                {
                    vID = i,
                    catID = i,
                    videoName = "GrumpyCat"
                });
            }
            
            var mockRepo = new Mocks.MockVideoRepository(videos);
            var controller = new VideoController(mockRepo);

            // Act:
            var result = controller.AddTranslation();
            
            // Assert:
            var viewResult = (ViewResult)result;
            List<Video> model = (viewResult.Model as IEnumerable<Video>).ToList();

            Assert.IsTrue(model.Count == 10);
        }

        [TestMethod]
        public void TryToGetMoreThan10Translations()
        {
            // Arrange:
            List<Translation> translations = new List<Translation>();

            for(int i = 0; i < 13; i++)
            {
                translations.Add(new Translation
                {
                    tID = i,
                    vID = i,
                    translationDescription = "Lorem ipsum",
                    translationLanguage = "English",
                    translationText = "Lorem ipsum",
                    translationTime = DateTime.Now.AddDays(i)
                });
            }

            var mockRepo = new Mocks.MockTranslationRepository(translations);
            var controller = new VideoController(mockRepo);

            // Act:
            var result = controller.GetTranslations();

            // Assert:
            var viewResult = (ViewResult)result;
            List<Translation> model = (viewResult.Model as IEnumerable<Translation>).ToList();

            Assert.IsTrue(model.Count == 10);
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
                    tID = i,
                    vID = i,
                    translationDescription = "Lorem ipsum",
                    translationLanguage = "English",
                    translationText = "Lorem ipsum",
                    translationTime = DateTime.Now.AddDays(i)
                });
            }
            var mockRepo = new Mocks.MockTranslationRepository(translations);
            var controller = new VideoController(mockRepo);
            // Act:
            var result = controller.GetTranslations();
            
            // Assert:
            var viewResult = (ViewResult)result;
            List<Translation> model = (viewResult.Model as IEnumerable<Translation>).ToList();

            Assert.IsTrue(model.Count == 7);
        }

        [TestMethod]
        public void TestIfOrderedByDateAndCheckIfExactly10()
        {
            // Arrange:
            List<Translation> translations = new List<Translation>();

            for (int i = 0; i < 10; i++)
            {
                translations.Add(new Translation
                {
                    tID = i,
                    vID = i,
                    translationDescription = "Lorem ipsum",
                    translationLanguage = "English",
                    translationText = "Lorem ipsum",
                    translationTime = DateTime.Now.AddDays(i)
                });
            }
            var mockRepo = new Mocks.MockTranslationRepository(translations);
            var controller = new VideoController(mockRepo);
        
            // Act:
            var result = controller.GetTranslations();

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
        public void TestIfVideosAreOrderedByName()
        {
            List<Video> videos = new List<Video>();

            for(int i = 0; i < 10; i++)
            {
                videos.Add(new Video
                {
                    vID = i,
                    catID = i,
                    videoName = "Video " + i.ToString()
                });
            }

            var mockRepo = new Mocks.MockVideoRepository(videos);
            var controller = new VideoController(mockRepo);

            // Act:
            var result = controller.OrderByName();

            // Assert:
            var viewResult = (ViewResult)result;
            List<Video> model = (viewResult.Model as IEnumerable<Video>).ToList();

            for(int i = 0; i < model.Count -1; i++)
            {
                int c = string.Compare(model[i].videoName, model[i + 1].videoName);
                Assert.IsTrue(c <= 0);
            }
        }
    }
}
