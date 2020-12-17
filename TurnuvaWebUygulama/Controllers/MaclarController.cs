using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VeritabaniKatmani;
using TurnuvaWebUygulama.Helper;
using VeritabaniKatmani.SqlQuery;

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

            return View(GetMaclar());
        }












        public List<Maclar> GetMaclar()
        {
            var maclarResult = MvcDbHelper.Repository.GetAll<Maclar>(Queries.Maclar.GetAll).ToList();
            return maclarResult;
        }
    }
}