using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransApp.DAL;
using TransApp.Models;

namespace TransApp.Controllers
{
    public class UserRequestController : Controller
    {
        UserRequestContext context = new UserRequestContext();

        public ActionResult GetRequests()
        {
            IEnumerable<UserRequest> requests = context.userRequest.ToList();

            return View(requests);
        }
	}
}