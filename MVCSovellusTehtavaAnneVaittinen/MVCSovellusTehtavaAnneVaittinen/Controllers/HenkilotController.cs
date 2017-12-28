using MVCSovellusTehtavaAnneVaittinen.Models;
using Newtonsoft.Json;
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
            return View();
        }
        public JsonResult GetList()

        {
            HarjoitustietokantaEntities entities = new HarjoitustietokantaEntities();
            var model = (from p in entities.Henkilot
                         select new
                         {
                             HenkiloId = p.HenkiloId,
                             Etunimi = p.Etunimi,
                             Sukunimi = p.Sukunimi,
                             Osoite = p.Osoite,
                             Esimies = p.Esimies
                         }).ToList();
            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();

            return Json(json, JsonRequestBehavior.AllowGet);
        }
    }
}