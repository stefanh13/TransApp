using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransApp.Repositories;
using PagedList;

namespace TransApp.Controllers
{
    public class MockVideoController : Controller
    {

        private readonly IVideoRepository videoTestRepo;
        private readonly ITranslationRepository translationTestRepo;

        const int PAGESIZE = 10;
        
        public MockVideoController(IVideoRepository repo)
        {
            videoTestRepo = repo;
        }
        
        public MockVideoController(ITranslationRepository repo)
        {
            translationTestRepo = repo;
        }

        public MockVideoController()
        {

        }
        //
        // GET: /MockVideo/
        public ActionResult GetTranslationByVideoId(int? id, int? page, string sortOrder)
        {

            /*ViewBag.ID = id;
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.LanguageSortParm = sortOrder == "Language" ? "lang_desc" : "Language";
            ViewBag.DownloadCountSortParm = sortOrder == "Count" ? "count_desc" : "Count";
            ViewBag.AverageSortParm = sortOrder == "Average" ? "aver_desc" : "Average";*/

            var translations = from v in translationTestRepo.GetTranslations()
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

            pageNumber = pageNumber < 0 ? 1 : pageNumber;

            return View(translations.ToPagedList(pageNumber, pageSize));
        
        }

        public ActionResult GetTranslationById(int? id)
        {

            var model = (from translation in translationTestRepo.GetTranslations()
                         where translation.ID == id
                         select translation);

   
            return View(model);
        }

        public ActionResult GetVideos(int? page, string sortOrder)
        {
            /*ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";*/

            var videos = from v in videoTestRepo.GetVideos()
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

            pageNumber = pageNumber < 0 ? 1 : pageNumber;

            return View(videos.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetVideoByCategory(string category, int? page, string sortOrder)
        {
  

            var videos = from v in videoTestRepo.GetVideos()
                         where v.videoCategory == category
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

            pageNumber = pageNumber < 0 ? 1 : pageNumber;

            return View(videos.ToPagedList(pageNumber, pageSize));
        }

        public ViewResult SearchEngine(string searchString, string currentFilter, int? page, string sortOrder)
        {

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var searchVideos = (from a in videoTestRepo.GetVideos()
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
	}
}