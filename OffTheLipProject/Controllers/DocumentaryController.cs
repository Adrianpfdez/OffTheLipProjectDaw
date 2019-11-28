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