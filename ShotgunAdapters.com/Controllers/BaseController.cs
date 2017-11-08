using Cstieg.WebFiles;
using ShotgunAdapters.Models;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

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