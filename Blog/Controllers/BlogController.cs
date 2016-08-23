using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        protected static BlogContext Context;

        public BlogController()
        {
            Context = new BlogContext();
        }
    }
}