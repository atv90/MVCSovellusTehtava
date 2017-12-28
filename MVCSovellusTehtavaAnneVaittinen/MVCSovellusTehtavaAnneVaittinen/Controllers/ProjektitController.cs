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
        public JsonResult GetSingleProjekti(int id)
        {
            HarjoitustietokantaEntities entities = new HarjoitustietokantaEntities();
            var model = (from p in entities.Projektit
                         where p.ProjektiId == id
                         select new
                         {
                             ProjektiId = p.ProjektiId,
                             Nimi = p.Nimi
                         }).FirstOrDefault();

            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Update(Projektit proj)
        {
            HarjoitustietokantaEntities entities = new HarjoitustietokantaEntities();
            //haetaan tietokannan rivi id:n perusteella
            int id = proj.ProjektiId;

            bool OK = false;
            Projektit dbItem = (from p in entities.Projektit
                                 where p.ProjektiId == id
                                 select p).FirstOrDefault();
            //kopioidaan selaimelta saadut tiedot tietokantaan, jos kentän arvo ei ole nolla
            if (dbItem != null)
            {
                dbItem.ProjektiId = proj.ProjektiId;
                dbItem.Nimi = proj.Nimi;

                entities.SaveChanges();
                //jos tietojen tallennus onnistuu asetetaan OK = true
                OK = true;
            }
            entities.Dispose();
            return Json(OK, JsonRequestBehavior.AllowGet);
        }
    }
}