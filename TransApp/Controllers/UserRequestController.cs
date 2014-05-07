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
	}
}