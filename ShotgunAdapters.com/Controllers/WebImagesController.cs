using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Cstieg.Image;
using ShotgunAdapters.Models;
using Cstieg.ControllerHelper;
using Cstieg.WebFiles.Controllers;
using Cstieg.WebFiles;

namespace ShotgunAdapters.Controllers
{
    public class WebImagesController : BaseController
    {
        // GET: WebImages
        public async Task<ActionResult> Index()
        {
            return View(await db.WebImages.ToListAsync());
        }

        // GET: WebImages/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebImage webImage = await db.WebImages.FindAsync(id);
            if (webImage == null)
            {
                return HttpNotFound();
            }
            return View(webImage);
        }

        // GET: WebImages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WebImages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ImageUrl,ImageSrcSet,Caption")] WebImage webImage)
        {
            if (ModelState.IsValid)
            {
                db.WebImages.Add(webImage);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(webImage);
        }

        // GET: WebImages/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebImage webImage = await db.WebImages.FindAsync(id);
            if (webImage == null)
            {
                return HttpNotFound();
            }
            return View(webImage);
        }

        // POST: WebImages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ImageUrl,ImageSrcSet,Caption")] WebImage webImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(webImage).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(webImage);
        }

        // GET: WebImages/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WebImage webImage = await db.WebImages.FindAsync(id);
            if (webImage == null)
            {
                return HttpNotFound();
            }
            return View(webImage);
        }

        // POST: WebImages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WebImage webImage = await db.WebImages.FindAsync(id);
            db.WebImages.Remove(webImage);
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
