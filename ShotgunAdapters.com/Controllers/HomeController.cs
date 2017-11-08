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
    [OutputCache(CacheProfile = "CacheForADay")]
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
                products = await db.Products
                                    .OrderByDescending(p => p.GunCaliber.Diameter)
                                    .OrderByDescending(p => p.AmmunitionCaliber.Diameter)
                                    .ToListAsync();
            }
            else
            {
                products = await db.Products
                                    .Where(p => p.GunCaliberId == id)
                                    .OrderByDescending(p => p.AmmunitionCaliber.Diameter)
                                    .ToListAsync();
            }
            return View(products);
        }

        public async Task<ActionResult> ProductsByCaliber(string caliberName)
        {
            var products = await db.Products
                                    .Where(p => p.GunCaliber.Name == caliberName)
                                    .OrderByDescending(p => p.AmmunitionCaliber.Diameter)
                                    .ToListAsync();
            return View("Products", products);
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
            return View(await db.Reviews.OrderByDescending(r => r.Date).ToListAsync());
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