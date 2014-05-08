using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransApp.Models;
using TransApp.Repositories;
using PagedList;


namespace TransApp.Controllers
{
    public class VideoController : Controller
    {
        VideoRepository videoRepo = new VideoRepository();
        TranslationRepository translationRepo = new TranslationRepository();
        
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

        public VideoController()
        {

        }
        
        public ActionResult FrontPage()
        {
            return View();
        }

        [HttpGet]
        public ActionResult AddTranslation()
        {
           
            return View(new Translation());
            
            /*var model = from t in repo.GetVideos()
                        select t;
            return View(model);*/
        }

        [HttpPost]
        public ActionResult AddTranslation(Translation translation)
        {
            string transName = translation.translationName;

            IEnumerable<Video> videoNames = videoRepo.GetAllVideos().ToList();

            foreach(var item in videoNames)
            {
                if(transName == item.videoName)
                {
                    
                    item.videoTime = translation.translationTime;
                    UpdateModel(item);
                    videoRepo.UpdateVideoTime(item);
                    videoRepo.Save();
                    
                    translation.vID = item.ID;
                    UpdateModel(translation);
                    translationRepo.Add(translation);
                    return RedirectToAction("/GetVideos");
                }
            }

            videoRepo.AddVideo(translation);
            translationRepo.Add(translation);

            return RedirectToAction("/GetVideos");
            //return View(translation);
        }


        /*public ActionResult GetTranslations()
        {
            var model = (from t in repo2.GetTranslations()
                         orderby t.translationTime descending
                         select t).Take(10);
            
            return View(model);
        }
        */
        public ActionResult OrderByName()
        {
            var model = (from name in videoRepo.GetAllVideos()
                         orderby name.videoName ascending
                         select name).Take(10);
            return View(model);
            
        }
        
        public ActionResult OrderByDate()
        {
            var model = (from date in videoRepo.GetAllVideos()
                         orderby date.videoTime descending
                         select date).Take(10);
            return View(model);
            
            
        }
        
        
        
        public ActionResult GetVideoByCategory(string category)
        {
            var model = (from t in videoRepo.GetAllVideos()
                         where t.videoCategory == category
                         orderby t.videoTime descending
                         select t).Take(10);

            return View(model);
        }

        public ActionResult GetTranslationsByVideoId(int id)
        {

            var model = (from t in translationRepo.GetAllTranslations()
                         where t.vID == id
                         orderby t.translationTime descending
                         select t).Take(10);
            
            /*var model = (from t in repo2.GetTranslations()
                         where t.vID == id
                         orderby t.translationTime descending
                         select t).Take(10);*/

            return View(model);
        }

        public ActionResult GetVideoBySearchName(string searchString)
        {
            searchString = searchString.ToLower();
            
            var model = (from t in repo.GetVideos()
                         where t.videoName.ToLower().Contains(searchString)
                         orderby t.videoTime descending
                         select t).Take(10);

            return View(model);

        }

        [HttpGet]
        public ActionResult GetVideos(string sort)
        {
       
                         var model = (from videos in videoRepo.GetAllVideos()
                         orderby videos.videoTime descending
                         select videos).Take(10);

            return View(model);
        }
        
	}
}