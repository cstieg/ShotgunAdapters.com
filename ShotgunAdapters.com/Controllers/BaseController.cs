using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Cstieg.WebFiles;
using ShotgunAdapters.Models;

namespace ShotgunAdapters.Controllers
{
    public class BaseController : Controller
    {
        protected ApplicationDbContext db = new ApplicationDbContext();
        public static string contentFolder = "/content";

        protected IFileService storageService; 
        protected ImageManager imageManager; 

        public BaseController()
        {
            // Set storage service for product images
            storageService = new FileSystemService(contentFolder);
            imageManager = new ImageManager("images/products", storageService);

            // Pass list of calibers to view to display in menu
            ViewBag.GunCalibers = db.Calibers.Where(c => c.DisplayInMenu).OrderByDescending(c => c.Diameter).ToList();
            foreach (Caliber gunCaliber in ViewBag.GunCalibers)
            {
                gunCaliber.ProductsAmmunition = db.Products
                                                .Where(p => p.GunCaliberId == gunCaliber.Id)
                                                .OrderByDescending(p => p.AmmunitionCaliber.Diameter)
                                                .ToList();
            }
        }

        protected void DetachProductsInNavbar()
        {
            // Detach product in ViewBag before saving edit
            foreach (Caliber caliber in ViewBag.GunCalibers)
            {
                foreach (Product product in caliber.ProductsAmmunition)
                {
                    db.Entry(product).State = EntityState.Detached;
                }

            }
        }

        protected void DetachCalibersInNavbar()
        {
            // Detach product in ViewBag before saving edit
            foreach (Caliber caliber in ViewBag.GunCalibers)
            {
                db.Entry(caliber).State = EntityState.Detached;
            }
        }


        /// <summary>
        /// Add dependency to cache so it is refreshed when updating the dependency
        /// </summary>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            filterContext.HttpContext.Response.AddCacheItemDependency("Pages");
        }
    }
}