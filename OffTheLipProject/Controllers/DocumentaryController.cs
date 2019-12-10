using OffTheLipProject.Migrations;
using OffTheLipProject.Models;
using OffTheLipProject.Models.ModelOTL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Documentary = OffTheLipProject.Models.Documentary;

namespace OffTheLipProject.Controllers
{
    public class DocumentaryController : Controller
    {
        readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string searchString, int page = 0)
        {
            List<Documentary> documentaries = db.Documentaries.ToList();

            var documentariesDB = db.Documentaries;

            if (!String.IsNullOrEmpty(searchString) )
            {
                documentaries = documentariesDB.Where(s => s.Name.Contains(searchString) || s.Description.Contains(searchString)).ToList();
            }
            else
            {
                documentaries = documentariesDB.OrderBy(o => o.Id).Skip(page * 6).Take(6).ToList();
            }

            TempData["searchString"] = searchString;
            return View(documentaries);
        }

        public ActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetPost(DocumentarySurferViewModel model)
        {
            if (ModelState.IsValid)
            {
                var documentary = new Documentary { Name = model.Name, Description = model.Description, Location = model.Location, Url = model.Url, UrlRedirect = model.UrlRedirect };
                db.Documentaries.Add(documentary);

                Surfer surfer = db.Surfers.Where(a => a.Name == model.SurferName).FirstOrDefault();
                documentary.Surfers.Add(surfer);

                var result = db.SaveChanges();

                if (result > 0)
                {
                    ViewBag.Message = string.Format("Documentary was created successfully");
                    var modelList = db.Documentaries.ToList();
                    return View("Index", modelList);
                }
            }
            return View(model);
        }

        public ActionResult SearchDocumentary()
        {
            return View();
        }

        public ActionResult EditDocumentary(string documentaryName)
        {
            var documentary = db.Documentaries.Where(a => a.Name == documentaryName).FirstOrDefault();

            if (documentary != null)
            {
                var surfer = documentary.Surfers.FirstOrDefault();

                DocumentarySurferViewModel documentaryVM;

                if (surfer != null)
                {
                    documentaryVM = new DocumentarySurferViewModel
                    {
                        Id = documentary.Id,
                        Name = documentary.Name,
                        Description = documentary.Description,
                        Location = documentary.Location,
                        Url = documentary.Url,
                        UrlRedirect = documentary.UrlRedirect,
                        SurferName = surfer.Name
                    };
                }
                else
                {
                    documentaryVM = new DocumentarySurferViewModel
                    {
                        Id = documentary.Id,
                        Name = documentary.Name,
                        Description = documentary.Description,
                        Location = documentary.Location,
                        Url = documentary.Url,
                        UrlRedirect = documentary.UrlRedirect,
                    };
                }

                return View(documentaryVM);
            }

            TempData["Message"] = string.Format("ERROR, documentary does not exist");
            return RedirectToAction("Index", "Home");
            //return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        }

        public ActionResult SaveChanges(DocumentarySurferViewModel model, int id)
        {
            var documentarries = db.Documentaries.Where(a => a.Id == id).FirstOrDefault();

            if (documentarries == null)
            {
                TempData["Message"] = string.Format("ERROR, documentary was not edited ");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                documentarries.Name = model.Name;
                documentarries.Description = model.Description;
                documentarries.Location = model.Location;
                documentarries.Url = model.Url;
                documentarries.UrlRedirect = model.UrlRedirect;

                db.SaveChanges();

                TempData["Message"] = string.Format("Documentary was edited successfully");
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DeleteDocumentary()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDocumentary(string videoName)
        {
            var documentary = db.Documentaries.Where(a => a.Name == videoName).FirstOrDefault();

            if (documentary != null)
            {
                db.Documentaries.Remove(documentary);
                db.SaveChanges();
                TempData["Message"] = string.Format("Documentary was deleted successfully");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Message"] = string.Format("ERROR, documentary was not deleted");
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DisplayPost(int? DocId)
        {
            if (!DocId.HasValue)
            {
                Response.Redirect("/");
                return View();
            }
            else
            {
                var item = db.Documentaries.Find(DocId.Value);

                var comments = db.CommentsDocumentaries
                    .Where(a => a.Documentary.Id == item.Id)
                    .Select(a => new DocumentaryCommentViewModel { Text = a.Text, Author = a.Author })
                    .ToList();
                ViewBag.Comments = comments;

                int nComments = comments.Count();

                var surfer = item.Surfers.FirstOrDefault();

                if (surfer != null)
                {
                    var obj1 = new DocumentaryCommentViewModel
                    {
                        DocId = DocId.Value,
                        Name = item.Name,
                        Url = item.Url,
                        Description = item.Description,
                        Location = item.Location,
                        SurferName = surfer.Name,
                        NumComments = nComments
                    };

                    return View(obj1);
                }

                var obj2 = new DocumentaryCommentViewModel
                {
                    DocId = DocId.Value,
                    Name = item.Name,
                    Url = item.Url,
                    Description = item.Description,
                    Location = item.Location,
                    NumComments = nComments
                };

                return View(obj2);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetComment(DocumentaryCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Documentary Item = db.Documentaries.Find(model.DocId);

                var comment = new CommentDocumentary { Author = HttpContext.User.Identity.Name, Text = model.Text, Documentary = Item };
                db.CommentsDocumentaries.Add(comment);
                var result = db.SaveChanges();

                if (result > 0)
                {
                    return RedirectToAction("DisplayPost", "Documentary", new { DocId = Item.Id });
                }
            }
            return View(model);
        }
    }
}