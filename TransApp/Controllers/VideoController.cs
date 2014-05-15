using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransApp.Models;
using TransApp.Repositories;
using PagedList;
using System.IO;

using TransApp.ViewModels;
using System.Text;






namespace TransApp.Controllers
{
    public class VideoController : Controller
    {
        VideoRepository videoRepo = new VideoRepository();
        TranslationRepository translationRepo = new TranslationRepository();
        CommentRepository commentRepo = new CommentRepository();
        
        private readonly IVideoRepository videoTestRepo;
        private readonly ITranslationRepository translationTestRepo;
       
        const int PAGESIZE = 10;
        
        public VideoController(IVideoRepository repo)
        {
            videoTestRepo = repo;
        }
        
        public VideoController(ITranslationRepository repo)
        {
            translationTestRepo = repo;
        }

        public VideoController()
        {

        }
        
        public ActionResult FrontPage()
        {
            return View();
        }

        public ActionResult CommonQuestions()
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
 
            if (file != null)
            {
                using (StreamReader stream = new StreamReader(file.InputStream, Encoding.Default, true))
                {
                    contentData = System.Environment.NewLine + stream.ReadToEnd();
                }
                string extension = Path.GetExtension(file.FileName);

                if (extension != ".srt")
                {
                    return View("WrongFileType");
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
            if(String.IsNullOrEmpty(category) || !videoRepo.ContainsCategory(category))
            {
                return View("NotFound");
            }
            
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            
      
            var videos = from v in videoRepo.GetAllVideos()
                         where v.videoCategory == category
                         select v;
            
            if(Math.Ceiling(Convert.ToDouble(videos.Count())/PAGESIZE) < page )
            {
                return View("NotFound");
            }
            
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
            
            pageNumber = pageNumber < 0 ? 1 : pageNumber;
            
            return View(videos.ToPagedList(pageNumber, pageSize));
        }

         public ActionResult GetTranslationsByVideoId(int? id, int? page, string sortOrder)
        {
            if (id == null || !videoRepo.isIdValid(id))
            {
                return View("NotFound");
            }
             
            ViewBag.ID = id;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.LanguageSortParm = sortOrder == "Language" ? "lang_desc" : "Language";
            ViewBag.DownloadCountSortParm = sortOrder == "Count" ? "count_desc" : "Count";
            ViewBag.AverageSortParm = sortOrder == "Average" ? "aver_desc" : "Average";
            
             var translations = from v in translationRepo.GetAllTranslations()
                         where v.vID == id
                         select v;

             if (Math.Ceiling(Convert.ToDouble(translations.Count()) / PAGESIZE) < page)
             {
                 return View("NotFound");
             }
            
            switch (sortOrder)
            {
                case "Date":
                    translations = translations.OrderBy(t => t.translationTime);
                    break;
                case "Language":
                    translations = translations.OrderBy(t => t.translationLanguage);
                    break;
                case "Count":
                    translations = translations.OrderBy(t => t.downloadCount);
                    break;
                case "Average":
                    translations = translations.OrderBy(t => t.averageVotes);
                    break;
                case "aver_desc":
                    translations = translations.OrderByDescending(t => t.averageVotes);
                    break;
                case "count_desc":
                    translations = translations.OrderByDescending(t => t.downloadCount);
                    break;
                case "lang_desc":
                    translations = translations.OrderByDescending(t => t.translationLanguage);
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
            pageNumber = pageNumber < 0 ? 1 : pageNumber;

            /*var testTranslations = translations;

            return View(testTranslations.ToPagedList(pageNumber, pageSize));           */ 

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

            if (Math.Ceiling(Convert.ToDouble(videos.Count()) / PAGESIZE) < page)
            {
                return View("NotFound");
            }

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

            pageNumber = pageNumber < 0 ? 1 : pageNumber;

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
            
            if (Math.Ceiling(Convert.ToDouble(searchVideos.Count()) / PAGESIZE) < page)
            {
                return View("NotFound");
            }
            
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

            pageNumber = pageNumber < 0 ? 1 : pageNumber;
            
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
        public ActionResult GetTranslationById(TranslationViewModel t, int? id)
        {
            if (t == null)
            {
                return View("NotFound");
            }
            
            if(ModelState.IsValid)
            {
                translationRepo.Update(t.Translation.translationText, id);
                return RedirectToAction("/GetVideos");
            }

            return View(t);
        }

        public ActionResult Download(string translationText, string fileName, int? id)
        {
            if(id == null || !translationRepo.isIdValid(id) || String.IsNullOrEmpty(fileName))
            {
                return View("NotFound");
            }
            
            MemoryStream mStream = new MemoryStream();
            TextWriter tWriter = new StreamWriter(mStream);
            tWriter.WriteLine(translationText);
            tWriter.Flush();
            byte[] bytes = mStream.ToArray();
            mStream.Close();

            string enCodeFileName = Server.UrlEncode(fileName);

            Response.Clear();
            Response.ContentType = "application/force-download";
            Response.AddHeader("content-disposition", "attachment;    filename=" + enCodeFileName + ".srt");
            Response.BinaryWrite(bytes);
            Response.End();

            int translationId = id.Value;
            
            translationRepo.RaiseDownloads(translationId);

            string returnUrl = "/GetTranslationById/" + id.ToString();
            
            return View(returnUrl);
        }

        [HttpPost]
        public ActionResult AddComment(int? id, string commentText)
        {
            
            if(id == null || !translationRepo.isIdValid(id))
            {
                return View("NotFound");
            }

            string returnUrl = "/GetTranslationById/" + id.ToString();
            if(String.IsNullOrEmpty(commentText))
            {
                return RedirectToAction(returnUrl);

            }
            
            int translationId = id.Value;
            
            commentRepo.AddComment(translationId, commentText, User.Identity.Name);

            

            return RedirectToAction(returnUrl);
        }

        [HttpPost]
        public ActionResult UpdateVotes(int? vote, int? id)
        {
            
            if(!vote.HasValue || !id.HasValue || !translationRepo.isIdValid(id))
            {
                return View("NotFound");
            }
            
            translationRepo.UpdateVotes(vote.Value, id.Value);

            string returnUrl = "/GetTranslationById/" + id.ToString();
            
            return RedirectToAction(returnUrl);
        }

        public ActionResult UploadTooLarge()
        {
            return View();
        }
        
	}
}