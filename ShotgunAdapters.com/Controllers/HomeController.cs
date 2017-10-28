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
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public async Task<ActionResult> Reviews()
        {
            return View(await db.Reviews.OrderBy(r => r.Date).ToListAsync());
        }

        public ActionResult Faq()
        {
            return View();
        }



    }
}