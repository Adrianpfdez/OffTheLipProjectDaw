using Microsoft.AspNet.Identity;
using OffTheLipProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OffTheLipProject.Controllers
{
    public class PostController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetPost(Post model)
        {
            if (ModelState.IsValid)
            {
                var documentary = new Documentary { Name = model.Name, Description = model.Description, Location = model.Location, Url = model.Url, UrlRedirect = model.UrlRedirect };
                db.Documentaries.Add(documentary);
                var result = db.SaveChanges();

                if (result == 1)
                {
                    return RedirectToAction("Documentary", "Home");
                }
            }
            return View(model);
        }

        public ActionResult GetPost()
        {
            // Contamos el número de ids que hay en la tabla Documentary
            var countId = db.Documentaries.Select(a => a.Id).Count();
            // Le indicamos que nos busque el último id de la tabla (que se corresponde con el n de ids existentes)
            var lastID = db.Documentaries.Find(countId);

            return View(lastID);
        }

        public ActionResult Display(int? DocId)
        {
            if (!DocId.HasValue)
            {
                Response.Redirect("/");
                return View();
            }
            else
            {
                var item = db.Documentaries.Find(DocId.Value);
                return View(item);
            }
        }

    }
}