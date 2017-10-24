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
            storageService = new FileSystemService(contentFolder);
            imageManager = new ImageManager("images/products", storageService);

            ViewBag.GunCalibers = db.Calibers.ToList();
        }

        public async Task AddCalibersToViewBagAsync()
        {
            ViewBag.GunCalibers = await db.Calibers.ToListAsync();
        }
    }
}