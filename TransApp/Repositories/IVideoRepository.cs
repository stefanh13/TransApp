using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransApp.Models;

namespace TransApp.Repositories
{
    public interface IVideoRepository
    {
        IQueryable<Video> GetVideos();
    }
}
