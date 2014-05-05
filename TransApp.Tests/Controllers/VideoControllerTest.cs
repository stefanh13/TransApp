using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransApp.Models;
using System.Collections.Generic;
using TransApp.Controllers;

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

            // Assert:
        }
    }
}
