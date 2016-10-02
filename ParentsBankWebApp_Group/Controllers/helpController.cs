using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ParentsBankWebApp_Group.Controllers
{
    public class helpController : Controller
    {
        // GET: help
        public ActionResult Index()
        {
            return View();
        }

        // Custom Page request
        //[Route("help/{pagename}")]
        public ActionResult Page()
        {
            string requestPage = Request["pagename"];
            if (requestPage != null)
                return View(requestPage);
            return View();
        }

        //[Route("~help/financial-resources")]
        public ActionResult showPage(string pagename)
        {
            //string requestPage = Request["pagename"];
            if (pagename != null)
                return View(pagename);
            return View();
        }
    }
}