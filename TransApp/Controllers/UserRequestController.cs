using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
            // Depends on what column user clicks in the list which if statement will be runned.
            ViewBag.CurrentSort = sortOrder;
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "Date" : "";
            ViewBag.NameSortParm = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.LanguageSortParm = sortOrder == "Language" ? "lang_desc" : "Language";
            ViewBag.LikeSortParm = sortOrder == "Like" ? "like_desc" : "Like";
            
            var requests = (from req in userReqRepo.GetAllUserRequests()
                            select req);

            // Prevents the user from accessing pages that aren't in the list.
            if (Math.Ceiling(Convert.ToDouble(requests.Count()) / PAGESIZE) < page)
            {
                return View("NotFound");
            }
            
            // Depends on what sortOrder is, which order the list will be in.
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

            // If the user tries to access a page that is less than 0.
            pageNumber = pageNumber < 0 ? 1 : pageNumber;

            return View(requests.ToPagedList(pageNumber, pageSize));    
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreateUserRequest()
        {
            // Declare a list that contains all the languages that will be displayed in the drop down list.
            List<SelectListItem> languageList = new List<SelectListItem>();

            languageList.Add(new SelectListItem { Text = "Veldu tungumál", Value = string.Empty });
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
            // Same method as above for the drop down list.
            List<SelectListItem> languageList = new List<SelectListItem>();

            languageList.Add(new SelectListItem { Text = "Veldu tungumál", Value = string.Empty });
            languageList.Add(new SelectListItem { Text = "Enska", Value = "Enska" });
            languageList.Add(new SelectListItem { Text = "Franska", Value = "Franska" });
            languageList.Add(new SelectListItem { Text = "Íslenska", Value = "Íslenska" });
            languageList.Add(new SelectListItem { Text = "Þýska", Value = "Þýska" });
            ViewData["requestLanguage"] = languageList;
            
            // If the the model is valid then create new request.
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
            // Prevents the user from crashing the site.
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
            // Prevents the user from crashing the site.
            if(id == null || !userReqRepo.IsIdValid(id))
            {
                return View("NotFound");
            }
            
            var model = (from item in userReqRepo.GetAllUserRequests()
                         where item.ID == id
                         select item).FirstOrDefault();
  
            return View(model);
        }
	}
}