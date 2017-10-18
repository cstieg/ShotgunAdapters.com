using ShotgunAdapters.Models;
using System.Linq;
using System.Web.Mvc;

namespace ShotgunAdapters.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Products()
        {
            var products = db.Products.ToList();
            return View(products);
        }
    }
}