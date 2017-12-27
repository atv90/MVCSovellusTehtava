using MVCSovellusTehtavaAnneVaittinen.Models;
using Newtonsoft.Json;
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
        public ActionResult P()
        {
            return View();
        }

        public JsonResult GetList()

        {
            HarjoitustietokantaEntities entities = new HarjoitustietokantaEntities();
            var model = (from p in entities.Projektit
                         select new
                         {
                             ProjektiId = p.ProjektiId,
                             Nimi = p.Nimi
                         }).ToList();
            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();

            return Json(json,JsonRequestBehavior.AllowGet);
        }
    }
}