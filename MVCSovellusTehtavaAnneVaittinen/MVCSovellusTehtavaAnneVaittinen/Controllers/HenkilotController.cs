using MVCSovellusTehtavaAnneVaittinen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSovellusTehtavaAnneVaittinen.Controllers
{
    public class HenkilotController : Controller
    {
        // GET: Henkilot
        public ActionResult H()
        {
            HarjoitustietokantaEntities entities = new HarjoitustietokantaEntities();
            List<Henkilot> model = entities.Henkilot.ToList();
            entities.Dispose();
            return View(model);
        }
    }
}