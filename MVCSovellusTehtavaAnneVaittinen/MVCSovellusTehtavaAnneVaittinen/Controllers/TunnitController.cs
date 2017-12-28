using MVCSovellusTehtavaAnneVaittinen.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCSovellusTehtavaAnneVaittinen.Controllers
{
    public class TunnitController : Controller
    {
        // GET: Tunnit
        public ActionResult T()
        {
            return View();
        }

        public JsonResult GetList()
        {
            HarjoitustietokantaEntities entities = new HarjoitustietokantaEntities();
            var model = (from t in entities.Tunnit
                         select new
                         {
                             TuntiId = t.TuntiId,
                             ProjektiId = t.ProjektiId,
                             HenkiloId = t.HenkiloId,
                             Pvm = t.Pvm,
                             Tunti = t.Tunnit1
                         }).ToList();
            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Tu()
        {
            HarjoitustietokantaEntities entities = new HarjoitustietokantaEntities();
            List<Tunnit> model = entities.Tunnit.ToList();
            entities.Dispose();
            return View(model);
        }
    }
}