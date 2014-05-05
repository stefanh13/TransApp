using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            var model = (from t in repo.GetVideos()
                         orderby t.videoName ascending
                         select t).Take(10);
            return View(model);
        }
	}
}