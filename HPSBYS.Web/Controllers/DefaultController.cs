using HPSBYS.Web.Fiilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPSBYS.Web.Controllers
{
    
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult NoDirectAccess()
        {
            return View();
        }
    }
}