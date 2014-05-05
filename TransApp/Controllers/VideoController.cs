using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TransApp.Repositories;

namespace TransApp.Controllers
{
    public class VideoController : Controller
    {

        private readonly IVideoRepository repo;

        public VideoController(IVideoRepository rep)
        {
            repo = rep;
        }
        
        
        //
        // GET: /Video/
        public ActionResult FrontPage()
        {
            return View();
        }
	}
}