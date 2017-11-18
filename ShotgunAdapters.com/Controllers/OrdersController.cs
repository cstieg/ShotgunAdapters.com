using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using Cstieg.Sales.Models;

namespace ShotgunAdapters.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class OrdersController : BaseController
    {
        // GET: Orders
        public async Task<ActionResult> Index()
        {
            var orders = db.Orders.Include(o => o.BillToAddress).Include(o => o.Customer).Include(o => o.ShipToAddress);
            return View(await orders.ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.BillToAddressId = new SelectList(db.Addresses, "Id", "Recipient");
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "CustomerName");
            ViewBag.ShipToAddressId = new SelectList(db.Addresses, "Id", "Recipient");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,CustomerId,DateOrdered,ShipToAddressId,BillToAddressId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BillToAddressId = new SelectList(db.Addresses, "Id", "Recipient", order.BillToAddressId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "CustomerName", order.CustomerId);
            ViewBag.ShipToAddressId = new SelectList(db.Addresses, "Id", "Recipient", order.ShipToAddressId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.BillToAddressId = new SelectList(db.Addresses, "Id", "Recipient", order.BillToAddressId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "CustomerName", order.CustomerId);
            ViewBag.ShipToAddressId = new SelectList(db.Addresses, "Id", "Recipient", order.ShipToAddressId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CustomerId,DateOrdered,ShipToAddressId,BillToAddressId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BillToAddressId = new SelectList(db.Addresses, "Id", "Recipient", order.BillToAddressId);
            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "CustomerName", order.CustomerId);
            ViewBag.ShipToAddressId = new SelectList(db.Addresses, "Id", "Recipient", order.ShipToAddressId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            db.Orders.Remove(order);
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
