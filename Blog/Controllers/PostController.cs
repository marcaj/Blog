using Blog.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class PostController : BlogController
    {
        public ActionResult List()
        {
            IQueryable<Post> posts = Context.Posts.OrderByDescending(x => x.PublishDate);
            return View(posts);
        }

        [HttpGet]
        public ActionResult GetPosts(int offset)
        {
            IQueryable<Post> posts = Context.Posts.OrderByDescending(x => x.PublishDate).ThenByDescending(x => x.Id).Skip(offset).Take(1);
            return Json(posts, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Details(int id)
        {
            Post post = Context.Posts.Include().First(p => p.Id == id);
            return View(post);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ActionCreate(string title, string content, string tags)
        {
            ICollection<Tag> tagCollection = Tag.GetListByString(tags, Context);
            Post post = new Post
            {
                Title = title,
                PublishDate = DateTime.Now,
                Content = content,
                Tags = tagCollection
            };

            HttpPostedFileBase featureImageFile = HttpContext.Request.Files[0];
            if (featureImageFile != null && featureImageFile.ContentLength != 0)
            {
                string path = WebConfigurationManager.AppSettings["Directory"];
                Guid guid = Guid.NewGuid();
                Directory.CreateDirectory(path + Path.DirectorySeparatorChar + guid);
                featureImageFile.SaveAs(path + Path.DirectorySeparatorChar + guid + Path.DirectorySeparatorChar + featureImageFile.FileName);
                Picture featureImage = new Picture
                {
                    FileName = featureImageFile.FileName,
                    Post = post,
                    Guid = guid
                };
            }

            Context.Posts.Add(post);
            try
            {
                Context.SaveChanges();
            }
            catch (Exception exception)
            {
                throw exception;
            }
            

            Response.Redirect("/Post/List", true);
            return null;
        }

        [HttpPost]
        public ActionResult ActionUpdate(string title, string content, string tags, string id)
        {
            //            ICollection<Tag> tagCollection = Tag.GetListByString(tags, Context);
            //            Post post = new Post
            //            {
            //                Title = title,
            //                PublishDate = DateTime.Now,
            //                Content = content,
            //                Tags = tagCollection
            //            };

            //            Context.Posts.Add(post);
            //            Context.SaveChanges();

            Response.Redirect("/Post/Details/" + id, true);
            return null;
        }

        public ActionResult Update(int id)
        {
            Post post = Context.Posts.Include().First(p => p.Id == id);
            return View(post);
        }

    }
}