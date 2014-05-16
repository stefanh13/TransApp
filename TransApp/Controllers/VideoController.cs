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

        const int PAGESIZE = 10;

        public ActionResult FrontPage()
        {
            return View();
        }

        public ActionResult CommonQuestions()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult AddTranslation()
        {
            // Declare a list that will contain all values for the language drop down list.
            List<SelectListItem> languageList = new List<SelectListItem>();

            languageList.Add(new SelectListItem { Text = "Veldu Tungumál", Value = string.Empty });
            languageList.Add(new SelectListItem { Text = "Enska", Value = "Enska" });
            languageList.Add(new SelectListItem { Text = "Franska", Value = "Franska" });
            languageList.Add(new SelectListItem { Text = "Íslenska", Value = "Íslenska" });
            languageList.Add(new SelectListItem { Text = "Þýska", Value = "Þýska" });
            
            ViewData["translationLanguage"] = languageList;

            // Declare a list that will contain all values for the category drop down list.
            List<SelectListItem> categoryList = new List<SelectListItem>();

            categoryList.Add(new SelectListItem { Text = "Veldu Flokk", Value = string.Empty });
            categoryList.Add(new SelectListItem { Text = "Gaman", Value = "Gaman" });
            categoryList.Add(new SelectListItem { Text = "Hasar", Value = "Hasar" });
            categoryList.Add(new SelectListItem { Text = "Hryllings", Value = "Hryllings" });
            categoryList.Add(new SelectListItem { Text = "Rómantík", Value = "Rómantík" });
            categoryList.Add(new SelectListItem { Text = "Ævintýra", Value = "Ævintýra" });  
            
            ViewData["translationCategory"] = categoryList;
            
            return View(new Translation());
        }
       
        [HttpPost]
        public ActionResult AddTranslation(HttpPostedFileBase file, Translation translation)
        {
            string contentData = string.Empty;
            // If the file is not empty, then we can read from the file and set contentData to that string.
            if (file != null)
            {
                using (StreamReader stream = new StreamReader(file.InputStream, Encoding.Default, true))
                {
                    contentData = System.Environment.NewLine + stream.ReadToEnd();
                }
                
                string extension = Path.GetExtension(file.FileName);

                // Just in case Javascript fails to recognize the file type.
                if (extension != ".srt")
                {
                    return View("WrongFileType");
                }
            }
           
            List<SelectListItem> languageList = new List<SelectListItem>();

            languageList.Add(new SelectListItem { Text = "Veldu Tungumál", Value = string.Empty });
            languageList.Add(new SelectListItem { Text = "Enska", Value = "Enska" });
            languageList.Add(new SelectListItem { Text = "Franska", Value = "Franska" });
            languageList.Add(new SelectListItem { Text = "Íslenska", Value = "Íslenska" });
            languageList.Add(new SelectListItem { Text = "Þýska", Value = "Þýska" });
            
            ViewData["translationLanguage"] = languageList;

            List<SelectListItem> categoryList = new List<SelectListItem>();

            categoryList.Add(new SelectListItem { Text = "Veldu Flokk", Value = string.Empty });
            categoryList.Add(new SelectListItem { Text = "Gaman", Value = "Gaman" });
            categoryList.Add(new SelectListItem { Text = "Hasar", Value = "Hasar" });
            categoryList.Add(new SelectListItem { Text = "Hryllings", Value = "Hryllings" });
            categoryList.Add(new SelectListItem { Text = "Rómantík", Value = "Rómantík" });
            categoryList.Add(new SelectListItem { Text = "Ævintýra", Value = "Ævintýra" }); 
            
            ViewData["translationCategory"] = categoryList;
            
            if(ModelState.IsValid)
            {
                string transName = translation.translationName;
                
                // If contentData is empty, then we don't want to set translationText as the uploaded file text.
                if (!String.IsNullOrEmpty(contentData))
                {
                    translation.translationText = translation.translationText + contentData;
                }
                
                IEnumerable<Video> videoNames = videoRepo.GetAllVideos().ToList();

                // Loop that checks if the new translation coming in has the same name as any video in the database.
                foreach(var item in videoNames)
                {
                    // If the video is already in the database, then we update the time and create new translation with the name of the video.
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
                
                // Else create a new video and translation with the same name.
                videoRepo.AddVideo(translation);
                translation.vID = videoId;
                translationRepo.Add(translation);

                return RedirectToAction("/GetVideos");
            }
            
            return View(translation);
        }

        [HttpGet]
        public ActionResult GetVideoByCategory(string category, int? page, string sortOrder)
        {
            // If a user tries to click a category that containts no film.
            if(!videoRepo.ContainsCategory(category) && videoRepo.CategoryRepo(category))
            {
                return View("Error");
            }
            
            // Prevents the user to crash the site.
            if (!videoRepo.ContainsCategory(category)|| String.IsNullOrEmpty(category))
            {
                return View("NotFound");
            }
            
            // Depends on what category the user wants to order the list by what if statement is runned here.
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            
            var videos = from item in videoRepo.GetAllVideos()
                         where item.videoCategory == category
                         select item;
            
            // If the user tries to access a page in the list that doesn't excist.
            if(Math.Ceiling(Convert.ToDouble(videos.Count())/PAGESIZE) < page )
            {
                return View("NotFound");
            }
            
            // Depends on what sortOrder is, which order the list will be in.
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
            
            // If the user tries to access a page less than 0, then send him to page 1.
            pageNumber = pageNumber < 0 ? 1 : pageNumber;
            
            return View(videos.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult GetTranslationsByVideoId(int? id, int? page, string sortOrder)
        {
            // Prevents the user from crashing the site.
            if (id == null || !videoRepo.isIdValid(id))
            {
                return View("NotFound");
            }
             
            // Same logic here below as in the method above but getting all translations with vID == id.
            ViewBag.ID = id;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.LanguageSortParm = sortOrder == "Language" ? "lang_desc" : "Language";
            ViewBag.DownloadCountSortParm = sortOrder == "Count" ? "count_desc" : "Count";
            ViewBag.AverageSortParm = sortOrder == "Average" ? "aver_desc" : "Average";
            
            var translations = from item in translationRepo.GetAllTranslations()
                               where item.vID == id
                               select item;

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
           
            pageNumber = pageNumber < 0 ? 1 : pageNumber;
            
            return View(translations.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public ActionResult GetVideos(int? page, string sortOrder)
        {
            // Same logic here below as in the method above but getting all videos here.
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.CategorySortParm = sortOrder == "Category" ? "cat_desc" : "Category";
            
            var videos = from item in videoRepo.GetAllVideos()
                         select item;

            if (Math.Ceiling(Convert.ToDouble(videos.Count()) / PAGESIZE) < page)
            {
                return View("NotFound");
            }

            switch (sortOrder)
            {
                case "name_desc":
                    videos = videos.OrderByDescending(v => v.videoName);
                    break;
                case "cat_desc":
                    videos = videos.OrderByDescending(v => v.videoCategory);
                    break;
                case "Date":
                    videos = videos.OrderBy(v => v.videoTime);
                    break;
                case "Name":
                    videos = videos.OrderBy(v => v.videoName);
                    break;
                case "Category":
                    videos = videos.OrderBy(v => v.videoCategory);
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

        
        [HttpGet]
        public ViewResult SearchEngine(string searchString, string currentFilter, int? page, string sortOrder)
        {
            // Same logic here below as in the method above but getting all the videos with names that contain the search string.
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
            
            var searchVideos =  from item in videoRepo.GetAllVideos()
                                select item;
            
            if (Math.Ceiling(Convert.ToDouble(searchVideos.Count()) / PAGESIZE) < page)
            {
                return View("NotFound");
            }
            
            // If search string is not empty, then select all the videos that contain the search string in their name.
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
            // Prevents the user from crashing the site
            if(id == null || !translationRepo.isIdValid(id))
            {
                return View("NotFound");
            }
            
            var model =  (from translation in translationRepo.GetAllTranslations()
                         where translation.ID == id
                         select translation).SingleOrDefault();

            
            // Fetch comments for the translation in question.
            var comments = (from comm in commentRepo.GetAllComments()
                            where comm.tID == id
                            select comm);

            TranslationViewModel tViewModel = new TranslationViewModel();

            tViewModel.Translation = model;
            tViewModel.Comments = comments;
            return View(tViewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult GetTranslationById(TranslationViewModel t, int? id)
        {
            if (t == null)
            {
                return View("NotFound");
            }
            
            // If model is valid, then update the model.
            if(ModelState.IsValid)
            {
                translationRepo.Update(t.Translation.translationText, id);
                
                string returnUrl = "/GetTranslationById/" + id.ToString();
                
                return RedirectToAction(returnUrl);
            }

            return View(t);
        }

        [HttpGet]
        public ActionResult Download(int? id)
        {
            // Prevents the user from crashing the site.
            if(id == null || !translationRepo.isIdValid(id))
            {
                return View("NotFound");
            }

            var translation = translationRepo.GetTranslationById(id);
            
            MemoryStream mStream = new MemoryStream();
            TextWriter tWriter = new StreamWriter(mStream);
            
            // Write the translation text into the text writer.
            tWriter.WriteLine(translation.translationText);
            // Clear all buffers.
            tWriter.Flush();
            
            byte[] bytes = mStream.ToArray();
            mStream.Close();

            string enCodeFileName = Server.UrlEncode(translation.translationName);

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

        [Authorize]
        [HttpPost]
        public ActionResult AddComment(int? id, string commentText)
        {
            // Prevents the user from crashing the site.
            if(id == null || !translationRepo.isIdValid(id))
            {
                return View("NotFound");
            }

            string returnUrl = "/GetTranslationById/" + id.ToString();
            
            // Just in case the Javascript fails.
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
            // Prevent the user from crashing the site.
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