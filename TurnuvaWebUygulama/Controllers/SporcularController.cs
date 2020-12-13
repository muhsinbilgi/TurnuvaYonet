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
    public class SporcularController : Controller
    {
        // GET: Sporcu
        public ActionResult Index()
        {
            return View(GetSporcular());
        }

        public ActionResult Ekle()
        {
            List<SelectListItem> degerler = (from i in MvcDbHelper.Repository.GetAll<Statu>(Queries.Statu.GetAll).ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Adi,
                                                 Value = i.Id.ToString()
                                             }).ToList();

            ViewBag.dgr = degerler;
            return View();
        }


        [HttpPost]
        public ActionResult Ekle(Sporcular model, HttpPostedFileBase file)
        {
            List<SelectListItem> degerler = (from i in MvcDbHelper.Repository.GetAll<Statu>(Queries.Statu.GetAll).ToList()
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
                    model.Resim = file.FileName;

                }


            }
            model.KullaniciId = 1;
            model.TakimId = 1;
            model.TurnuvaId = 1;
            MvcDbHelper.Repository.Insert(Queries.Sporcular.Insert, model);
            ViewBag.Basari = 1;
            ViewBag.dgr = degerler;
            return View();

        }


        public ActionResult Detay(int Id)
        {

            var model = MvcDbHelper.Repository.GetById<Sporcular>(Queries.Sporcular.GetbyId, new { Id = Id }).FirstOrDefault();
            return View(model);
        }


        public ActionResult Duzenle(int Id)
        {
            List<SelectListItem> degerler = (from i in MvcDbHelper.Repository.GetAll<Statu>(Queries.Statu.GetAll).ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Adi,
                                                 Value = i.Id.ToString()
                                             }).ToList();

            ViewBag.dgr = degerler;

            var model = MvcDbHelper.Repository.GetById<Sporcular>(Queries.Sporcular.GetbyId, new { Id = Id }).FirstOrDefault();

            return View(model);
        }



        [HttpPost]
        public ActionResult Duzenle(Sporcular model, HttpPostedFileBase file)
        {


            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    file.SaveAs(HttpContext.Server.MapPath("~/Image/")
                                                          + file.FileName);
                    model.Resim = file.FileName;

                }


            }

            model.KullaniciId = 1;
            model.TakimId = 1;
            model.TurnuvaId = 1;
            ViewBag.Basari = 1;


            MvcDbHelper.Repository.Update(Queries.Sporcular.Update, model);
            return RedirectToAction("Index");
        }




        public ActionResult Sil(int Id)
        {
            Sporcular model = new Sporcular() { Id = Id };
            MvcDbHelper.Repository.Delete<Sporcular>(Queries.Sporcular.Delete, model);
            return RedirectToAction("Index");
        }







        public List<Sporcular> GetSporcular()
        {
            var SporcularResult = MvcDbHelper.Repository.GetAll<Sporcular>(Queries.Sporcular.GetAll).ToList();
            return SporcularResult;
        }
    }
}