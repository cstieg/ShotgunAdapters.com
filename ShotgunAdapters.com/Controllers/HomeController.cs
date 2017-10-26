using ShotgunAdapters.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ShotgunAdapters.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            var products = await db.Products.Where(p => p.DisplayOnFrontPage).ToListAsync();
            return View(products);
        }

        public async Task<ActionResult> Products(int? id = null)
        {
            List<Product> products;
            if (id == null)
            {
                products = await db.Products.ToListAsync();
            }
            else
            {
                products = await db.Products.Where(p => p.GunCaliberId == id).ToListAsync();
            }
            return View(products);
        }

        public async Task<ActionResult> Product(int id)
        {
            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }
            return View(product);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Reviews()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Faq()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }



    }
}