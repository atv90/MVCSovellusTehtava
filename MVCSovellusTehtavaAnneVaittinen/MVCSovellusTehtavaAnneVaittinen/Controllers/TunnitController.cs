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
        //modaali-ikkunan luonti
        public JsonResult GetSingleTunti(int id)
        {
            HarjoitustietokantaEntities entities = new HarjoitustietokantaEntities();
            var model = (from t in entities.Tunnit
                         where t.TuntiId == id
                         select new
                         {
                             TuntiId = t.TuntiId,
                             ProjektiId = t.ProjektiId,
                             HenkiloId = t.HenkiloId,
                             Pvm = t.Pvm,
                             Tunti = t.Tunnit1
                         }).FirstOrDefault();

            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        //modaali-ikkunan luonti
        public ActionResult Update(Tunnit tun)
        {
            HarjoitustietokantaEntities entities = new HarjoitustietokantaEntities();
            int id = tun.TuntiId;

            bool OK = false;

            Tunnit dbItem = (from t in entities.Tunnit
                             where t.TuntiId == id
                             select t).FirstOrDefault();
            if (dbItem != null)
            {
                dbItem.ProjektiId = tun.ProjektiId;
                dbItem.HenkiloId = tun.HenkiloId;
                dbItem.Pvm = tun.Pvm;
                dbItem.Tunnit1 = tun.Tunnit1;

                entities.SaveChanges();
                OK = true;
            }
            entities.Dispose();
            return Json(OK);
        }
    }
}