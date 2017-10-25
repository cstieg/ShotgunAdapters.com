using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using ShotgunAdapters.Models;
using Cstieg.WebFiles;
using System.Collections.Generic;
using System.Web;
using Cstieg.WebFiles.Controllers;
using Cstieg.ControllerHelper;

namespace ShotgunAdapters.Controllers
{
    public class ProductsController : BaseController
    {
        // GET: Products
        public async Task<ActionResult> Index()
        {
            var products = db.Products.Include(p => p.AmmunitionCaliber).Include(p => p.GunCaliber);
            return View(await products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.AmmunitionCaliberId = new SelectList(db.Calibers, "Id", "Name");
            ViewBag.GunCaliberId = new SelectList(db.Calibers, "Id", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Description,Price,Shipping,ImageUrl,DisplayOnFrontPage,DoNotDisplay,GunCaliberId,AmmunitionCaliberId,ProductInfo")] Product product)
        {
            // Check file is exists and is valid image
            HttpPostedFileBase imageFile = _ModelControllersHelper.GetImageFile(ModelState, Request, "");

            if (ModelState.IsValid)
            {
                // Save image to disk and store filepath in model
                try
                {
                    string timeStamp = FileManager.GetTimeStamp();
                    product.ImageUrl = await imageManager.SaveFile(imageFile, 200, true, timeStamp);
                    product.ImageSrcSet = await imageManager.SaveImageMultipleSizes(imageFile, new List<int>() { 800, 400, 200, 100 }, true, timeStamp);
                }
                catch
                {
                    ModelState.AddModelError("ImageUrl", "Failure saving image. Please try again.");
                    return View(product);
                }

                // add new model
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AmmunitionCaliberId = new SelectList(db.Calibers, "Id", "Name", product.AmmunitionCaliberId);
            ViewBag.GunCaliberId = new SelectList(db.Calibers, "Id", "Name", product.GunCaliberId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.AmmunitionCaliberId = new SelectList(db.Calibers, "Id", "Name", product.AmmunitionCaliberId);
            ViewBag.GunCaliberId = new SelectList(db.Calibers, "Id", "Name", product.GunCaliberId);
            product.WebImages = product.WebImages ?? new List<WebImage>();
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Description,Price,Shipping,ImageUrl,DisplayOnFrontPage,DoNotDisplay,GunCaliberId,AmmunitionCaliberId,ProductInfo")] Product product)
        {
            // Check file is exists and is valid image
            HttpPostedFileBase imageFile = _ModelControllersHelper.GetImageFile(ModelState, Request);

            if (ModelState.IsValid)
            {
                // Save image to disk and store filepath in model
                try
                {
                    string timeStamp = FileManager.GetTimeStamp();
                    product.ImageUrl = await imageManager.SaveFile(imageFile, 200, true, timeStamp);
                    product.ImageSrcSet = await imageManager.SaveImageMultipleSizes(imageFile, new List<int>() { 800, 400, 200, 100 }, true, timeStamp);
                }
                catch
                {
                    ModelState.AddModelError("ImageUrl", "Failure saving image. Please try again.");
                    return View(product);
                }

                // add new model
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AmmunitionCaliberId = new SelectList(db.Calibers, "Id", "Name", product.AmmunitionCaliberId);
            ViewBag.GunCaliberId = new SelectList(db.Calibers, "Id", "Name", product.GunCaliberId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await db.Products.FindAsync(id);

            // remove image files used by product
            imageManager.DeleteImageWithMultipleSizes(product.ImageUrl);

            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<JsonResult> AddImage(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return this.JError(404, "Can't find product " + id.ToString());
            }

            // Check file is exists and is valid image
            HttpPostedFileBase imageFile = _ModelControllersHelper.GetImageFile(ModelState, Request, "", "file");

            if (ModelState.IsValid)
            {
                // Save image to disk and store filepath in model
                try
                {
                    string timeStamp = FileManager.GetTimeStamp();
                    WebImage image = new WebImage
                    {
                        ProductId = product.Id,
                        ImageUrl = await imageManager.SaveFile(imageFile, 200, true, timeStamp),
                        ImageSrcSet = await imageManager.SaveImageMultipleSizes(imageFile, new List<int>() { 800, 400, 200, 100 }, true, timeStamp)
                    };
                    db.WebImages.Add(image);
                    await db.SaveChangesAsync();
                }
                catch
                {
                    return this.JError(400, "Error saving image");
                }
            }
            return new JsonResult { Data = new { success = "True" } };
        }

        public async Task<JsonResult> DeleteImage(int id)
        {
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return this.JError(404, "Can't find product " + id.ToString());
            }

            int imageId = int.Parse(Request.Params.Get("imageId"));
            WebImage image = await db.WebImages.FindAsync(imageId);
            if (image == null)
            {
                return this.JError(404, "Can't find image " + imageId.ToString());
            }

            // remove image files used by product
            imageManager.DeleteImageWithMultipleSizes(image.ImageUrl);

            db.WebImages.Remove(image);
            await db.SaveChangesAsync();
            return this.JOk();
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
