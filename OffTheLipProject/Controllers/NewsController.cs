﻿using OffTheLipProject.Models;
using OffTheLipProject.Models.ModelOTL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace OffTheLipProject.Controllers
{
    public class NewsController : Controller
    {
        readonly ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(string searchString, int page = 0)
        {
            List<News> news = db.News.ToList();

            var newsDB = db.News;

            if (!String.IsNullOrEmpty(searchString))
            {
                news = newsDB.Where(s => s.Name.Contains(searchString) || s.Description.Contains(searchString)).ToList();
            }
            else
            {
                news = newsDB.OrderBy(o => o.Id).Skip(page * 6).Take(6).ToList();
            }

            TempData["searchString"] = searchString;
            return View(news);
        }

        public ActionResult CreateNews()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetNews(NewsSurferViewModel model)
        {
            if (ModelState.IsValid)
            {
                var notice = new News { Name = model.Name, Description = model.Description, Location = model.Location, Image = model.Image};
                db.News.Add(notice);

                Surfer surfer = db.Surfers.Where(a => a.Name == model.SurferName).FirstOrDefault();
                notice.Surfers.Add(surfer);

                var result = db.SaveChanges();

                if (result > 0)
                {
                    ViewBag.Message = string.Format("News was created successfully");
                    var modelList = db.News.ToList();
                    return View("Index", modelList);
                }
            }
            return View(model);
        }

        public List<News> SearchNew(string nameNew)
        {
            var notice = db.News.Where(a => a.Name == nameNew);

            if (notice != null){
                return notice.ToList();
            }
            else
            {
                return db.News.ToList();
            }
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult EditNews(NewsSurferViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    if (newsId != null)
        //    {

        //    }
        //}

        public ActionResult DeleteNews()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteNews(string noticeName)
        {
            var notice = db.News.Where(a => a.Name == noticeName).FirstOrDefault();

            if (notice != null)
            {
                db.News.Remove(notice);
                db.SaveChanges();
                TempData["Message"] = string.Format("Notice was deleted successfully");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Message"] = string.Format("ERROR, notice wasnt deleted");
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult DisplayNews(int? DocId)
        {
            if (!DocId.HasValue)
            {
                Response.Redirect("/");
                return View();
            }
            else
            {
                var item = db.News.Find(DocId.Value);

                var comments = db.CommentsNews
                    .Where(a => a.News.Id == item.Id)
                    .Select(a => new NewsCommentViewModel { Text = a.Text, Author = a.Author })
                    .ToList();
                ViewBag.Comments = comments;

                var nComments = comments.Count();

                var surfer = item.Surfers.FirstOrDefault();

                if (surfer != null)
                {
                    var obj1 = new NewsCommentViewModel
                    {
                        DocId = DocId.Value,
                        Name = item.Name,
                        Image = item.Image,
                        Description = item.Description,
                        Location = item.Location,
                        SurferName = surfer.Name,
                        NumComments = nComments
                    };
                    return View(obj1);
                }

                var obj2 = new NewsCommentViewModel
                {
                    DocId = DocId.Value,
                    Name = item.Name,
                    Image = item.Image,
                    Description = item.Description,
                    Location = item.Location,
                    NumComments = nComments
                };

                return View(obj2);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SetComment(NewsCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                News Item = db.News.Find(model.DocId);

                var comment = new CommentNews{ Author = HttpContext.User.Identity.Name, Text = model.Text, News = Item };
                db.CommentsNews.Add(comment);
                var result = db.SaveChanges();

                if (result > 0)
                {
                    return RedirectToAction("DisplayNews", "News", new { DocId = Item.Id });
                }
            }
            return View(model);
        }
    }
}