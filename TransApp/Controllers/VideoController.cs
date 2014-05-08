﻿using System;
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
                    
                    translation.v = item.ID;
                    UpdateModel(translation);
                    translationRepo.Add(translation);
                    return RedirectToAction("/GetVideos");
                }
            }

            AddVideo(translation);
            translationRepo.Add(translation);

            return RedirectToAction("/GetVideos");
            //return View(translation);
        }

        public void AddVideo(Translation translation)
        {
            Video newVideo = new Video();
            newVideo.videoName = translation.translationName;
            newVideo.videoTime = translation.translationTime;
            newVideo.catID = 1;

            videoRepo.Add(newVideo);
        }

        public void UpdateVideoTime(Video v)
        {
            UpdateModel(v);
            videoRepo.UpdateVideoTime(v);
            videoRepo.Save();
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
            if(IsNameAscending())
            {
                var model = (from name in videoRepo.GetAllVideos()
                             orderby name.videoName descending
                             select name).Take(10);
                return View(model);
            }
            else
            {
                var model = (from name in videoRepo.GetAllVideos()
                             orderby name.videoName ascending
                             select name).Take(10);
                return View(model);
            }
            
        }
        
        public ActionResult OrderByDate()
        {
            if(IsDateDescending())
            {
                var model = (from date in videoRepo.GetAllVideos()
                             orderby date.videoTime ascending
                             select date).Take(10);
                return View(model);
            }
            else
            {
                var model = (from date in videoRepo.GetAllVideos()
                             orderby date.videoTime descending
                             select date).Take(10);
                return View(model);
            }
            
        }
        
        public bool IsNameAscending()
        {
            var videos = (videoRepo.GetAllVideos()).ToList();

            for(int i = 1; i < videos.Count; i++)
            {
                int areVideosAscending = string.Compare(videos[i - 1].videoName, videos[i].videoName);
                if(areVideosAscending > 0)
                {
                    return false;
                }
            }
            
            return true;
        }
        
        public bool IsDateDescending()
        {
            var videos = (videoRepo.GetAllVideos()).ToList();

            for (int i = 1; i < videos.Count; i++)
            {
                if(videos[i - 1].videoTime >= videos[i].videoTime)
                {
                    return false;
                }
            }

            return true;
        }
        /*
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
            searchString = searchString.ToLower();
            
            var model = (from t in repo.GetVideos()
                         where t.videoName.ToLower().Contains(searchString)
                         orderby t.videoTime descending
                         select t).Take(10);

            return View(model);

        }*/

        [HttpGet]
        public ActionResult GetVideos()
        {
            var model = videoRepo.GetAllVideos();
            return View(model);
        }
        
	}
}