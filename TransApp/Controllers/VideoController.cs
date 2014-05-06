using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransApp.Models;
using TransApp.Repositories;

namespace TransApp.Controllers
{
    public class VideoController : Controller
    {

        private readonly IVideoRepository repo;
        private readonly ITranslationRepository repo2;

        public VideoController(IVideoRepository rep)
        {
            repo = rep;
        }
        
        public VideoController(ITranslationRepository reps)
        {
            repo2 = reps;
        }
        
        //
        // GET: /Video/
        public ActionResult FrontPage()
        {
            return View();
        }

        public ActionResult AddTranslation()
        {
            var model = from t in repo.GetVideos()
                        select t;
            return View(model);
        }

        public ActionResult GetTranslations()
        {
            var model = (from t in repo2.GetTranslations()
                         orderby t.translationTime descending
                         select t).Take(10);
            
            return View(model);
        }

        public ActionResult OrderByName()
        {
            if(IsNameAscending())
            {
                var model = (from t in repo.GetVideos()
                             orderby t.videoName descending
                             select t).Take(10);
                return View(model);
            }
            else
            {
                var model = (from t in repo.GetVideos()
                             orderby t.videoName ascending
                             select t).Take(10);
                return View(model);
            }
            
        }
        public ActionResult OrderByDate()
        {
            if(IsDateDescending())
            {
                var model = (from t in repo.GetVideos()
                             orderby t.videoTime ascending
                             select t).Take(10);
                return View(model);
            }
            else
            {
                var model = (from t in repo.GetVideos()
                             orderby t.videoTime descending
                             select t).Take(10);
                return View(model);
            }
            
        }

        public bool IsNameAscending()
        {
            var videos = (repo.GetVideos()).ToList();

            for(int i = 0; i < videos.Count - 1; i++)
            {
                int c = string.Compare(videos[i].videoName, videos[i + 1].videoName);
                if(c > 0)
                {
                    return false;
                }
            }
            
            return true;
        }

        public bool IsDateDescending()
        {
            var videos = (repo.GetVideos()).ToList();

            for (int i = 0; i < videos.Count - 1; i++)
            {
                if(videos[i].videoTime >= videos[i + 1].videoTime)
                {
                    return false;
                }
            }

            return true;
        }

        public ActionResult GetVideoByCategoryId(int id)
        {
            var model = (from t in repo.GetVideos()
                         where t.catID == id
                         orderby t.videoTime descending
                         select t).Take(10);

            return View(model);
        }

        public ActionResult GetTranslationsByVideoId(int id)
        {
            var model = (from t in repo2.GetTranslations()
                         where t.vID == id
                         orderby t.translationTime descending
                         select t).Take(10);

            return View(model);
        }

        public ActionResult GetVideoBySearchName(string searchString)
        {
            var model = (from t in repo.GetVideos()
                         where t.videoName == searchString
                         select t);

            return View(model);

        }
	}
}