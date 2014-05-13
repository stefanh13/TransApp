using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransApp.Models;
using TransApp.Repositories;
using PagedList;
using System.IO;
<<<<<<< HEAD
using TransApp.ViewModels;
=======


>>>>>>> Autori-Branch


namespace TransApp.Controllers
{
    public class VideoController : Controller
    {
        VideoRepository videoRepo = new VideoRepository();
        TranslationRepository translationRepo = new TranslationRepository();
        CommentRepository commentRepo = new CommentRepository();
        
        private readonly IVideoRepository repo;
        private readonly ITranslationRepository repo2;
       
        const int PAGESIZE = 10;
        
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

            List<SelectListItem> languageList = new List<SelectListItem>();

            languageList.Add(new SelectListItem { Text = "Veldu Tungumál", Value = "" });
            languageList.Add(new SelectListItem { Text = "Enska", Value = "Enska" });
            languageList.Add(new SelectListItem { Text = "Franska", Value = "Franska" });
            languageList.Add(new SelectListItem { Text = "Íslenska", Value = "Íslenska" });
            languageList.Add(new SelectListItem { Text = "Þýska", Value = "Þýska" });
            ViewData["translationLanguage"] = languageList;

            List<SelectListItem> categoryList = new List<SelectListItem>();

            categoryList.Add(new SelectListItem { Text = "Veldu Flokk", Value = "" });
            categoryList.Add(new SelectListItem { Text = "Hasar", Value = "Hasar" });
            categoryList.Add(new SelectListItem { Text = "Ævintýra", Value = "Ævintýra" });
            categoryList.Add(new SelectListItem { Text = "Rómantík", Value = "Rómantík" });
            categoryList.Add(new SelectListItem { Text = "Gaman", Value = "Gaman" });
            ViewData["translationCategory"] = categoryList;
            
            return View(new Translation());
            
            /*var model = from t in repo.GetVideos()
                        select t;
            return View(model);*/
        }
       
        [HttpPost]
        public ActionResult AddTranslation(HttpPostedFileBase file, Translation translation)
        {
         
            string contentData = null;

            if (file.ContentLength > 1000000)
            {
                return View("FileView");
            }
            
            if (file != null)
            {
                using (StreamReader stream = new StreamReader(file.InputStream))
                {
                    contentData = stream.ReadToEnd();
                }
            }
           
            List<SelectListItem> languageList = new List<SelectListItem>();

            languageList.Add(new SelectListItem { Text = "Veldu Tungumál", Value = "" });
            languageList.Add(new SelectListItem { Text = "Enska", Value = "Enska" });
            languageList.Add(new SelectListItem { Text = "Franska", Value = "Franska" });
            languageList.Add(new SelectListItem { Text = "Íslenska", Value = "Íslenska" });
            languageList.Add(new SelectListItem { Text = "Þýska", Value = "Þýska" });
            ViewData["translationLanguage"] = languageList;

            List<SelectListItem> categoryList = new List<SelectListItem>();

            categoryList.Add(new SelectListItem { Text = "Veldu Flokk", Value = "" });
            categoryList.Add(new SelectListItem { Text = "Hasar", Value = "Hasar" });
            categoryList.Add(new SelectListItem { Text = "Ævintýra", Value = "Ævintýra" });
            categoryList.Add(new SelectListItem { Text = "Rómantík", Value = "Rómantík" });
            categoryList.Add(new SelectListItem { Text = "Gaman", Value = "Gaman" });
            ViewData["translationCategory"] = categoryList;
            
            if(ModelState.IsValid)
            {
                string transName = translation.translationName;
                
                if (!String.IsNullOrEmpty(contentData))
                {
                    translation.translationText = translation.translationText + contentData;
                }
                
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
                    

                        translationRepo.Add(translation);
                        return RedirectToAction("/GetVideos");
                    }
                }

                int videoId = videoNames.Last().ID + 1;
            
                videoRepo.AddVideo(translation);

                translation.vID = videoId;
                translationRepo.Add(translation);

                return RedirectToAction("/GetVideos");
            
            }
            
            return View(translation);
        }


        /*public ActionResult GetTranslations()
        {
            var model = (from t in repo2.GetTranslations()
                         orderby t.translationTime descending
                         select t).Take(10);
            
            return View(model);
        }
        */
       
       
        
        
        
        public ActionResult GetVideoByCategory(string category, int? page, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            
      
            var videos = from v in videoRepo.GetAllVideos()
                         where v.videoCategory == category
                         select v;

            switch (sortOrder)
            {
                case "name_desc":
                    videos = videos.OrderByDescending(v => v.videoName);
                    break;
                case "Date":
                    videos = videos.OrderBy(v => v.videoTime);
                    break;
                case "Name":
                    videos = videos.OrderBy(v => v.videoName);
                    break;
                default:
                    videos = videos.OrderByDescending(v => v.videoTime);
                    break;
            }
            int pageSize = PAGESIZE;
            int pageNumber = (page ?? 1);
            
            return View(videos.ToPagedList(pageNumber, pageSize));
        }

         public ActionResult GetTranslationsByVideoId(int? id, int? page, string sortOrder)
        {
            ViewBag.ID = id;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.LanguageSortParm = sortOrder == "Language" ? "lang_desc" : "Language";
            var translations = from v in translationRepo.GetAllTranslations()
                         where v.vID == id
                         select v;
            switch (sortOrder)
            {
                case "Date":
                    translations = translations.OrderBy(t => t.translationTime);
                    break;
                case "lang_desc":
                    translations = translations.OrderByDescending(t => t.translationLanguage);
                    break;
                case "Language":
                    translations = translations.OrderBy(t => t.translationLanguage);
                    break;
                default:
                    translations = translations.OrderByDescending(t => t.translationTime);
                    break;
            }

            int pageSize = PAGESIZE;
            int pageNumber = (page ?? 1);
            
            /*var model = (from t in translationRepo.GetAllTranslations()
                         where t.vID == id
                         orderby t.translationTime descending
                         select t).Take(10);*/
            
            /*var model = (from t in repo2.GetTranslations()
                         where t.vID == id
                         orderby t.translationTime descending
                         select t).Take(10);*/

            return View(translations.ToPagedList(pageNumber, pageSize));
        }

        /*public ActionResult GetVideoBySearchName(string searchString)
        {
            searchString = searchString.ToLower();
            
            var model = (from t in repo.GetVideos()
                         where t.videoName.ToLower().Contains(searchString)
                         orderby t.videoTime descending
                         select t).Take(10);

            return View(model);

        }*/

        [HttpGet]
        public ActionResult GetVideos(int? page, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            var videos = from v in videoRepo.GetAllVideos()
                           select v;
            switch (sortOrder)
            {
                case "name_desc":
                    videos = videos.OrderByDescending(v => v.videoName);
                    break;
                case "Date":
                    videos = videos.OrderBy(v => v.videoTime);
                    break;
                case "Name":
                    videos = videos.OrderBy(v => v.videoName);
                    break;
                default:
                    videos = videos.OrderByDescending(v => v.videoTime);
                    break;
            }

            int pageSize = PAGESIZE;
            int pageNumber = (page ?? 1);
 
            /*var model = (from videos in videoRepo.GetAllVideos()
                         orderby videos.videoTime descending
                         select videos);*/

            

            return View(videos.ToPagedList(pageNumber, pageSize));
        }

        
        [HttpGet]
        public ViewResult SearchEngine(string searchString, string currentFilter, int? page, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            
            var searchVideos = (from a in videoRepo.GetAllVideos()
                                select a);
            if (!String.IsNullOrEmpty(searchString))
            {
                searchVideos = searchVideos.Where(a => a.videoName.ToLower().Contains(searchString.ToLower()));
            }
            

            switch (sortOrder)
            {
                case "name_desc":
                    searchVideos = searchVideos.OrderByDescending(v => v.videoName);
                    break;
                case "Date":
                    searchVideos = searchVideos.OrderBy(v => v.videoTime);
                    break;
                case "Name":
                    searchVideos = searchVideos.OrderBy(v => v.videoName);
                    break;
                default:
                    searchVideos = searchVideos.OrderByDescending(v => v.videoTime);
                    break;
            }
            
            int pageSize = PAGESIZE;
            int pageNumber = (page ?? 1);

            
            
            return View(searchVideos.ToPagedList(pageNumber, pageSize));
        }

       
        [HttpGet]
        public ActionResult GetTranslationById(int? id)
        {
            if(id == null || !translationRepo.isIdValid(id))
            {
                return View("NotFound");
            }
            
            var model = (from translation in translationRepo.GetAllTranslations()
                         where translation.ID == id
                         select translation).SingleOrDefault();

            /*if (model == null) {
                return View("NotFound");
            }*/

            //model = model.SingleOrDefault();

            
            // Fetch comments for the transtion in question.
            var comments = (from comm in commentRepo.GetAllComments()
                            where comm.tID == id
                            select comm);

            TranslationViewModel tViewModel = new TranslationViewModel();

            tViewModel.Translation = model;
            tViewModel.Comments = comments;
            return View(tViewModel);
        }

        [HttpPost]
        public ActionResult GetTranslationById(Translation t)
        {
            if(ModelState.IsValid)
            {
                translationRepo.Update(t);
                return RedirectToAction("/GetVideos");
            }

            return View(t);
        }

        public ActionResult Download(string translation, string fileName)
        {

            MemoryStream mStream = new MemoryStream();
            TextWriter tWriter = new StreamWriter(mStream);
            tWriter.WriteLine(translation);
            tWriter.Flush();
            byte[] bytes = mStream.ToArray();
            mStream.Close();

            string enCodeFileName = Server.UrlEncode(fileName);

            Response.Clear();
            Response.ContentType = "application/force-download";
            Response.AddHeader("content-disposition", "attachment;    filename=" + enCodeFileName + ".srt");
            Response.BinaryWrite(bytes);
            Response.End();
            
            return View();
        }

        [HttpPost]
        public ActionResult AddComment(int? id, string commentText)
        {
            int translationId = id.Value;
            
            commentRepo.AddComment(translationId, commentText, User.Identity.Name);

            string returnUrl = "/GetTranslationById/" + id.ToString();

            return RedirectToAction(returnUrl);
        }
        
	}
}