using OffTheLipProject.Models;
using OffTheLipProject.Models.ModelOTL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OffTheLipProject.Controllers
{
    public class SurferController : Controller
    {
        readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string surferName)
        {
            Surfer surfer = db.Surfers.Where(a => a.Name == surferName).FirstOrDefault();

            List<News> newsSurfer = surfer.News.ToList();
            List<Documentary> documentarySurfer = surfer.Documentaries.ToList();
            List<Hardware> hardwareSurfer = surfer.Hardwares.ToList();

            var obj = new SurferViewModel
            {
                Surfer = surfer,
                Documentaries = documentarySurfer,
                Hardwares = hardwareSurfer,
                News = newsSurfer,
            };

            return View(obj);
        }

        public ActionResult CreateSurfer()
        {
            return View();
        }

        public ActionResult SetSurfer(Surfer model)
        {
            if (ModelState.IsValid)
            {
                var surfer = new Surfer { Name = model.Name, Stance = model.Stance, Image = model.Image, Natinality = model.Natinality, Competitor = model.Competitor };
                db.Surfers.Add(surfer);
                var result = db.SaveChanges();

                if (result > 0)
                {
                    TempData["Message"] = string.Format("Surfer was created successfully");
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData["Message"] = string.Format("ERROR, Surfer was not created");
            return RedirectToAction("Index", "Home"); 
        }

        public ActionResult SearchSurfer()
        {
            return View();
        }

        public ActionResult EditSurfer(string surferName)
        {
            var surfer = db.Surfers.Where(a => a.Name == surferName).FirstOrDefault();

            if (surfer != null)
            {
                return View(surfer);
            }

            TempData["Message"] = string.Format("ERROR, surfer does not exist");
            return RedirectToAction("Index", "Home");
            //return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        public ActionResult SaveChanges(Surfer model, int id)
        {
            var surfer = db.Surfers.Where(a => a.Id == id).FirstOrDefault();

            if (surfer == null)
            {
                TempData["Message"] = string.Format("ERROR, surfer was not edited ");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                surfer.Name = model.Name;
                surfer.Natinality = model.Natinality;
                surfer.Image = model.Image;
                surfer.Stance = model.Stance;
                surfer.Competitor = model.Competitor;

                db.SaveChanges();

                TempData["Message"] = string.Format("Surfer was edited successfully");
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DeleteSurfer()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSurfer(string surferName)
        {
            var surfer = db.Surfers.Where(a => a.Name == surferName).FirstOrDefault();

            if (surfer != null)
            {
                db.Surfers.Remove(surfer);
                db.SaveChanges();
                TempData["Message"] = string.Format("Surfer was deleted successfully");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Message"] = string.Format("ERROR, surfer was not deleted");
                return RedirectToAction("Index", "Home");
            }
        }
    }
}