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

        public JsonResult GetSingleHenkilo(int id)
        {
            HarjoitustietokantaEntities entities = new HarjoitustietokantaEntities();
            var model = (from h in entities.Henkilot
                         where h.HenkiloId == id
                         select new
                         {
                             HenkiloId = h.HenkiloId,
                             Etunimi = h.Etunimi,
                             Sukunimi = h.Sukunimi,
                             Osoite = h.Osoite,
                             Esimies = h.Esimies
                         }).FirstOrDefault();

            string json = JsonConvert.SerializeObject(model);
            entities.Dispose();
            return Json(json, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Update(Henkilot henk)
        {
            HarjoitustietokantaEntities entities = new HarjoitustietokantaEntities();
            //haetaan tietokannan rivi id:n perusteella
            int id = henk.HenkiloId;

            bool OK = false;

            //onko kyseessä uusi lisäys vai vanhan muokkaus
            if (id.ToString() == "(lisätään automaattisesti)")
            {
                //uuden lisääminen eli kopioidaan kentät
                Henkilot dbItem = new Henkilot()
                {
                    //otetaan CompanyNamesta Substring funktiolla 5 ensimmäistä merkkiä, jos loppuu
                    //välilyöntiin tehdään Trim()
                    HenkiloId = int.Parse(henk.Etunimi.Substring(0, 3).Trim().ToUpper()),
                    Etunimi = henk.Etunimi,
                    Sukunimi = henk.Sukunimi,
                    Osoite = henk.Osoite,
                    Esimies = henk.Esimies
            };
                //tallennetaan uusi lisäys tietokantaan
                entities.Henkilot.Add(dbItem);
                entities.SaveChanges();
                OK = true;
            }
            else
            {
                Henkilot dbItem = (from h in entities.Henkilot
                                   where h.HenkiloId == id
                                   select h).FirstOrDefault();
                //kopioidaan selaimelta saadut tiedot tietokantaan, jos kentän arvo ei ole nolla
                if (dbItem != null)
                {
                    dbItem.HenkiloId = henk.HenkiloId;
                    dbItem.Etunimi = henk.Etunimi;
                    dbItem.Sukunimi = henk.Sukunimi;
                    dbItem.Sukunimi = henk.Sukunimi;
                    dbItem.Osoite = henk.Osoite;
                    dbItem.Esimies = henk.Esimies;

                    entities.SaveChanges();
                    //jos tietojen tallennus onnistuu asetetaan OK = true
                    OK = true;
                }
            }
            entities.Dispose();
            return Json(OK);
        }
    }
}