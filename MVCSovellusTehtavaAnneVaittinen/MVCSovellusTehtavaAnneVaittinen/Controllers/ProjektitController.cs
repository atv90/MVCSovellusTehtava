using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSovellusTehtavaAnneVaittinen.Controllers
{
    public class ProjektitController : Controller
    {
        // GET: Projektit
        public ActionResult Projektit()
        {
            return View();
        }
    }
}