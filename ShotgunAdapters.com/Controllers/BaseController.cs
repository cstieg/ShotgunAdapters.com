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

        public BaseController()
        {
            ViewBag.GunCalibers = db.Calibers.ToList();
        }

        public async Task AddCalibersToViewBagAsync()
        {
            ViewBag.GunCalibers = await db.Calibers.ToListAsync();
        }
    }
}