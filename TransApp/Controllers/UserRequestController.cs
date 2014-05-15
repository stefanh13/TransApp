using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransApp.DAL;
using TransApp.Models;
using TransApp.Repositories;
using Microsoft.AspNet.Identity;
using PagedList;


namespace TransApp.Controllers
{
    public class UserRequestController : Controller
    {
        const int PAGESIZE = 10;
        
        UserRequestRepository userReqRepo = new UserRequestRepository();

        public ActionResult GetRequests(int? page, string sortOrder)
        {

            ViewBag.CurrentSort = sortOrder;
            
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.LanguageSortParm = sortOrder == "Language" ? "lang_desc" : "Language";
            ViewBag.LikeSortParm = sortOrder == "Like" ? "like_desc" : "Like";
            
            var requests = (from req in userReqRepo.GetAllUserRequests()
                            select req);

            if (Math.Ceiling(Convert.ToDouble(requests.Count()) / PAGESIZE) < page)
            {
                return View("NotFound");
            }
            
            switch (sortOrder)
            {
                case "name_desc":
                    requests = requests.OrderByDescending(r => r.requestName);
                    break;
                case "lang_desc":
                    requests = requests.OrderByDescending(r => r.requestLanguage);
                    break;
                case "like_desc":
                    requests = requests.OrderByDescending(r => r.likes);
                    break;
                case "Date":
                    requests = requests.OrderBy(r => r.requestTime);
                    break;
                case "Name":
                    requests = requests.OrderBy(r => r.requestName);
                    break;
                case "Language":
                    requests = requests.OrderBy(r => r.requestLanguage);
                    break;
                case "Like":
                    requests = requests.OrderBy(r => r.likes);
                    break;
                default:
                    requests = requests.OrderByDescending(r => r.requestTime);
                    break;
            }

            int pageSize = PAGESIZE;
            int pageNumber = (page ?? 1);

            pageNumber = pageNumber < 0 ? 1 : pageNumber;

            return View(requests.ToPagedList(pageNumber, pageSize));
            
            /*var model = (from r in UserReqRepo.GetAllUserRequests()
                         orderby r.requestTime descending
                         select r).Take(10);
            return View(model);*/
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateUserRequest()
        {
            List<SelectListItem> languageList = new List<SelectListItem>();

            languageList.Add(new SelectListItem { Text = "", Value = "" });
            languageList.Add(new SelectListItem { Text = "Enska", Value = "Enska" });
            languageList.Add(new SelectListItem { Text = "Franska", Value = "Franska" });
            languageList.Add(new SelectListItem { Text = "Íslenska", Value = "Íslenska" });
            languageList.Add(new SelectListItem { Text = "Þýska", Value = "Þýska" });
            ViewData["requestLanguage"] = languageList;
            userReqRepo.Save();
            return View(new UserRequest());
        }

        [HttpPost]
        public ActionResult CreateUserRequest(UserRequest u)
        {
            List<SelectListItem> languageList = new List<SelectListItem>();

            languageList.Add(new SelectListItem { Text = "", Value = "" });
            languageList.Add(new SelectListItem { Text = "Enska", Value = "Enska" });
            languageList.Add(new SelectListItem { Text = "Franska", Value = "Franska" });
            languageList.Add(new SelectListItem { Text = "Íslenska", Value = "Íslenska" });
            languageList.Add(new SelectListItem { Text = "Þýska", Value = "Þýska" });
            ViewData["requestLanguage"] = languageList;

            if (ModelState.IsValid)
            {
                u.userName = User.Identity.Name;
                userReqRepo.AddUserRequests(u);
                return RedirectToAction("GetRequests");
            }
            return View(u);
        }

        [HttpPost]    
        public ActionResult Like(int? id)
        {

            if (id == null || !userReqRepo.IsIdValid(id))
            {
                return View("NotFound");
            }
            
            userReqRepo.UpdateLike(id);

            string returnUrl = "/GetUserRequestById/" + id.Value.ToString();

            return RedirectToAction(returnUrl);
        }
        public ActionResult GetUserRequestById(int? id)
        {
            if(id == null || !userReqRepo.IsIdValid(id))
            {
                return View("NotFound");
            }
            
            var model = (from l in userReqRepo.GetAllUserRequests()
                         where l.ID == id
                         select l).FirstOrDefault();
  

            return View(model);
        }
	}
}