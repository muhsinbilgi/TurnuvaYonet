using VeritabaniKatmani;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TurnuvaWebUygulama.Helper;
using VeritabaniKatmani.SqlQuery;


namespace TurnuvaWebUygulama.Controllers
{
    public class TakimlarController : Controller
    {
        // GET: Takimlar
        public ActionResult Index()
        {
         
            return View(GetTakimlar());
        }

        
        public ActionResult Ekle()
        {
            List<SelectListItem> degerler = (from i in MvcDbHelper.Repository.GetAll<Kategori>(Queries.Kategori.GetAll).ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Adi,
                                                 Value = i.Id.ToString()
                                             }).ToList();

            ViewBag.dgr = degerler;
            return View();
        }


     
        [HttpPost]
        public ActionResult Ekle(Takimlar model, HttpPostedFileBase file)
        {
            List<SelectListItem> degerler = (from i in MvcDbHelper.Repository.GetAll<Kategori>(Queries.Kategori.GetAll).ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Adi,
                                                 Value = i.Id.ToString()
                                             }).ToList();

           

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Image/")
                                                          + file.FileName);
                    model.Logo = file.FileName;

                }


            }

            model.TurnuvaId = 1;
            MvcDbHelper.Repository.Insert(Queries.Takimlar.Insert, model);
            ViewBag.Basari = 1;
            ViewBag.dgr = degerler;
            return View();
        }


        public ActionResult Detay(int Id)
        {
       

            var model = MvcDbHelper.Repository.GetById<Takimlar>(Queries.Takimlar.GetbyId, new { Id = Id }).FirstOrDefault();
            return View(model);
        }


        public ActionResult Duzenle(int Id)
        {
            List<SelectListItem> degerler = (from i in MvcDbHelper.Repository.GetAll<Kategori>(Queries.Kategori.GetAll).ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Adi,
                                                 Value = i.Id.ToString()
                                             }).ToList();

            ViewBag.dgr = degerler;

            var model = MvcDbHelper.Repository.GetById<Takimlar>(Queries.Takimlar.GetbyId, new { Id = Id }).FirstOrDefault();

            return View(model);
        }



        [HttpPost]
        public ActionResult Duzenle(Takimlar model, HttpPostedFileBase file)
        {
     

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Image/")
                                                          + file.FileName);
                    model.Logo = file.FileName;

                }


            }

            model.TurnuvaId = 1;
          
            ViewBag.Basari = 1;


            MvcDbHelper.Repository.Update(Queries.Takimlar.Update, model);
            return RedirectToAction("Index");
        }

       

  

        public ActionResult Sil(int Id)
        {
            Takimlar model = new Takimlar() { Id = Id };
            MvcDbHelper.Repository.Delete<Takimlar>(Queries.Takimlar.Delete, model);
            return RedirectToAction("Index");
        }


        public List<Takimlar> GetTakimlar()
        {
            var m = MvcDbHelper.Repository.GetById<Kullanicilar>(Queries.Kullanicilar.GetbyName, new { KullaniciAdi = User.Identity.Name }).FirstOrDefault();

            if(m.Rol == "Y")
            {
                var takimlarResultY = MvcDbHelper.Repository.GetById<Takimlar>(Queries.Takimlar.GetbyY, new { TurnuvaId = m.TurnuvaId }).ToList();
                return takimlarResultY;
            }
            else if(m.Rol == "T")
            {
                var takimlarResultT = MvcDbHelper.Repository.GetById<Takimlar>(Queries.Takimlar.GetbyT, new { Id = m.TakimId }).ToList();
                return takimlarResultT;
            }
            else
            {
                var takimlarResult = MvcDbHelper.Repository.GetAll<Takimlar>(Queries.Takimlar.GetAll).ToList();
                return takimlarResult;
            }

           
           
        }


    }
}