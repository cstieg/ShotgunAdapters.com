using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using ShotgunAdapters.Models;

namespace ShotgunAdapters.Controllers
{
    public class CalibersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Calibers
        public async Task<ActionResult> Index()
        {
            return View(await db.Calibers.ToListAsync());
        }

        // GET: Calibers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caliber caliber = await db.Calibers.FindAsync(id);
            if (caliber == null)
            {
                return HttpNotFound();
            }
            return View(caliber);
        }

        // GET: Calibers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Calibers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Diameter")] Caliber caliber)
        {
            if (ModelState.IsValid)
            {
                db.Calibers.Add(caliber);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(caliber);
        }

        // GET: Calibers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caliber caliber = await db.Calibers.FindAsync(id);
            if (caliber == null)
            {
                return HttpNotFound();
            }
            return View(caliber);
        }

        // POST: Calibers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Diameter")] Caliber caliber)
        {
            if (ModelState.IsValid)
            {
                db.Entry(caliber).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(caliber);
        }

        // GET: Calibers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Caliber caliber = await db.Calibers.FindAsync(id);
            if (caliber == null)
            {
                return HttpNotFound();
            }
            return View(caliber);
        }

        // POST: Calibers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Caliber caliber = await db.Calibers.FindAsync(id);
            db.Calibers.Remove(caliber);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
