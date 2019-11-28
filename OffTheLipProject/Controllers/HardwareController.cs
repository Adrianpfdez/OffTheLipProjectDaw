﻿using OffTheLipProject.Models;
using OffTheLipProject.Models.ModelOTL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OffTheLipProject.Controllers
{
    public class HardwareController : Controller
    {
        readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string searchString, int page = 0)
        {
            List<Hardware> harware = db.Hardwares.ToList();

            var hardwareDB = db.Hardwares;

            if (!String.IsNullOrEmpty(searchString))
            {
                harware = hardwareDB.Where(s => s.Name.Contains(searchString) || s.Description.Contains(searchString)).ToList();
            }
            else
            {
                harware = hardwareDB.OrderBy(o => o.Id).Skip(page * 4).Take(4).ToList();
            }

            TempData["searchString"] = searchString;
            return View(harware);
        }

        public ActionResult CreateHardware()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetHardware(HardwareSurferViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hardware = new Hardware { Name = model.Name, Description = model.Description, ReleaseDate = model.ReleaseDate, Price = model.Price, Image = model.Image};
                db.Hardwares.Add(hardware);

                Surfer surfer = db.Surfers.Where(a => a.Name == model.SurferName).FirstOrDefault();
                hardware.Surfers.Add(surfer);

                var result = db.SaveChanges();

                if (result > 0)
                {
                    ViewBag.Message = string.Format("Item was created successfully");
                    var modelList = db.Hardwares.ToList();
                    return View("Index", modelList);
                }
            }
            return View();
        }

        public ActionResult DisplayHardware(int? DocId)
        {
            if (!DocId.HasValue)
            {
                Response.Redirect("/");
                return View();
            }
            else
            {
                var item = db.Hardwares.Find(DocId.Value);

                var comments = db.CommentsHardwares
                    .Where(a => a.Hardware.Id == item.Id)
                    .Select(a => new HardwareCommentViewModel { Text = a.Text, Author = a.Author })
                    .ToList();
                ViewBag.Comments = comments;

                int nComments = comments.Count();

                Surfer surfer = item.Surfers.FirstOrDefault();

                if (surfer != null)
                {
                    var obj1 = new HardwareCommentViewModel
                    {
                        DocId = DocId.Value,
                        Name = item.Name,
                        Image = item.Image,
                        Description = item.Description,
                        Price = item.Price,
                        ReleaseDate = item.ReleaseDate,
                        SurferName = surfer.Name,
                        NumComments = nComments
                    };

                    return View(obj1);
                }

                var obj2 = new HardwareCommentViewModel
                {
                    DocId = DocId.Value,
                    Name = item.Name,
                    Image = item.Image,
                    Description = item.Description,
                    Price = item.Price,
                    ReleaseDate = item.ReleaseDate,
                    NumComments = nComments
                };

                return View(obj2);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetComment(HardwareCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Hardware Item = db.Hardwares.Find(model.DocId);

                var comment = new CommentHardware { Author = HttpContext.User.Identity.Name, Text = model.Text, Hardware = Item };
                db.CommentsHardwares.Add(comment);
                var result = db.SaveChanges();

                if (result > 0)
                {
                    return RedirectToAction("DisplayHardware", "Hardware", new { DocId = Item.Id });
                }
            }
            return View(model);
        }
    }
}