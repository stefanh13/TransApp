using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransApp.Models;
using TransApp.Repositories;

namespace TransApp.Tests.Mocks
{
    class MockVideoRepository : IVideoRepository
    {

        private readonly List<Video> videos;
        
        
        public MockVideoRepository(List<Video> vids)
        {
            videos = vids;
        }
        
        public IQueryable<Models.Video> GetVideos()
        {
            return videos.AsQueryable();
        }
    }
}
