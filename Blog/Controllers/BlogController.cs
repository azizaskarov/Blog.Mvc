using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        static List<BlogEntry> Posts = new List<BlogEntry>();
        public IActionResult Index()
        {

            return View("Index", Posts);
        }

        public IActionResult CreatorPage(Guid id)
        {
            if (id != Guid.Empty)
            {
                BlogEntry existingEntry = Posts.FirstOrDefault(b => b.Id == id);

                return View(model: existingEntry);
            }

            return View();
        }

        [HttpPost]
        public IActionResult CreatorPage(BlogEntry entry)
        {
            if (entry.Id == Guid.Empty)
            {
                var newEntry = new BlogEntry()
                {
                    Id = Guid.NewGuid(),
                    Content = entry.Content,
                };

                Posts.Add(newEntry);
            }
            else
            {
                var existingEntry = Posts.FirstOrDefault(p => p.Id == entry.Id);
                existingEntry.Content = entry.Content;
            }


            return RedirectToAction("Index");
        }
    }
}
