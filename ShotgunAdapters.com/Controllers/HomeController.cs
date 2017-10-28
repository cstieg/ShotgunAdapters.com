using ShotgunAdapters.Models;
using System.Collections.Generic;
using System.Configuration;
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

        public async Task<ActionResult> ProductsByCaliber(string caliberName)
        {
            return View("Products", await db.Products.Where(p => p.GunCaliber.Name == caliberName).ToListAsync());
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

        /// <summary>
        /// Displays list of links to model edit pages
        /// </summary>
        public ActionResult Edit()
        {
            string modelControllers = ConfigurationManager.AppSettings["modelControllers"];
            char[] delimiters = { ',' };
            string[] controllersArray = modelControllers.Split(delimiters);
            List<string> controllers = new List<string>(controllersArray);
            return View(controllers);
        }

    }
}