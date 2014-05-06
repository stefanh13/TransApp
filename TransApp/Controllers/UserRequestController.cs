using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TransApp.Controllers
{
    public class UserRequestController : Controller
    {
        //TODO:
        // 1 - Install package Entity framework
        // 2 - Model class
        // 3 - Context class
        // 4 - Initializer class
        // 5 - Reference initalizer to web config
        // 6 - Add connection to web config
        // 7 - Create scaffolding from model and data context

        // GET: /UserRequest/
        public ActionResult Index()
        {
            return View();
        }
	}
}