using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeritabaniKatmani;
using TurnuvaWebUygulama.Helper;
using VeritabaniKatmani.SqlQuery;
using OperasyonKatmani.GrupOperasyon;

namespace TurnuvaWebUygulama.Controllers
{
    public class MaclarController : Controller
    {
        // GET: Maclar
        public ActionResult Index()
        {

           // turnuva id basılacak
            var model = MvcDbHelper.Repository.GetById<Maclar>(Queries.Maclar.GetbyMax, new { TurnuvaId = 1 }).FirstOrDefault();
 
            ViewBag.MaxHafta = model.MaxHafta;


           // int model1 = GrupOperasyon.hesapla(1, 2);


            return View(GetMaclar());
        }

        public ActionResult Gruplar()
        {
            var m = MvcDbHelper.Repository.GetById<Kullanicilar>(Queries.Kullanicilar.GetbyName, new { KullaniciAdi = User.Identity.Name }).FirstOrDefault();
            
            GrupListele model = new GrupListele();
            
            model.GrupAdlari = MvcDbHelper.Repository.GetById<GrupAdlari>(Queries.GrupAdlari.GetbyId, new { TurnuvaId = m.SeciliTurnuva }).ToList();
            model.Gruplar = MvcDbHelper.Repository.GetById<Gruplar>(Queries.Gruplar.GetAll, new { Id = m.SeciliTurnuva }).ToList();
       

            return View(model);
        }












        public List<Maclar> GetMaclar()
        {
            var maclarResult = MvcDbHelper.Repository.GetAll<Maclar>(Queries.Maclar.GetAll).ToList();
            return maclarResult;
        }

    

    }
}