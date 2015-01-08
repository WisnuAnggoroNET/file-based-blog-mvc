using BlogEngine.Web.Models;
using System.Configuration;
using System.Web.Mvc;

namespace BlogEngine.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var manager = new BlogFileSystemManager(Server.MapPath(ConfigurationManager.AppSettings["BlogPostsDirectory"]));
            var model = manager.GetBlogListings(5);
            return View(model);
        }

        public ActionResult ViewBlogPost(string postName)
        {
            var manager = new BlogFileSystemManager(Server.MapPath(ConfigurationManager.AppSettings["BlogPostsDirectory"]));
            var model = manager.GetBlogPostByTitleForUrl(postName);
            if (model == null)
            {
                return RedirectToRoute("Error");
            }
            return View(model);
        }

        public ActionResult Error()
        {
            return View();
        }
	}
}