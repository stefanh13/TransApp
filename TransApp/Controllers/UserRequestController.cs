using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransApp.DAL;
using TransApp.Models;
using TransApp.Repositories;


namespace TransApp.Controllers
{
    public class UserRequestController : Controller
    {
        UserRequestRepository UserReqRepo = new UserRequestRepository();

        public ActionResult GetRequests()
        {
            var model = UserReqRepo.GetAllUserRequests();
            return View(model);
        }

        [HttpGet]
        public ActionResult CreateUserRequest()
        {
            List<SelectListItem> languageList = new List<SelectListItem>();

            languageList.Add(new SelectListItem { Text = "", Value = "" });
            languageList.Add(new SelectListItem { Text = "Enska", Value = "2" });
            languageList.Add(new SelectListItem { Text = "Franska", Value = "3" });
            languageList.Add(new SelectListItem { Text = "Íslenska", Value = "4" });
            languageList.Add(new SelectListItem { Text = "Þýska", Value = "5" });
            ViewData["requestLanguage"] = languageList;
            UserReqRepo.Save();
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
                UpdateModel(u);
                UserReqRepo.AddUserRequests(u);
                UserReqRepo.Save();
                return RedirectToAction("GetRequests");
            }
            return View(u);
        }

        public ActionResult OrderRequestsByName()
        {
            var model = (from r in UserReqRepo.GetAllUserRequests()
                         orderby r.requestName ascending
                         select r).Take(10);

            return View(model);
        }
        public ActionResult OrderRequestsByLanguage()
        {
            var model = (from l in UserReqRepo.GetAllUserRequests()
                         orderby l.requestLanguage ascending
                         select l).Take(10);

            return View(model);
        }
        public ActionResult OrderRequestsByDate()
        {
            var model = (from t in UserReqRepo.GetAllUserRequests()
                         orderby t.requestTime ascending
                         select t).Take(10);

            return View(model);
        }
        public ActionResult OrderRequestsByLikes()
        {
            var model = (from l in UserReqRepo.GetAllUserRequests()
                         orderby l.likes descending
                         select l).Take(10);

            return View(model);
        }
	}
}