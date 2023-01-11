using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using fondo9Sa3ada.context;

namespace fondo9Sa3ada.Controllers
{
    public class reviewsController : Controller
    {
        private HotelDBEntities3 db = new HotelDBEntities3();

        // GET: reviews
        public async Task<ActionResult> Index()
        {
            var review = db.review.Include(r => r.Reservation);
            return View(await review.ToListAsync());
        }

        // GET: reviews/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            review review = await db.review.FindAsync(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: reviews/Create
        public ActionResult Create()
        {
            ViewBag.reservation_ID = new SelectList(db.Reservation, "reservation_ID", "customerName");
            return View();
        }

        // POST: reviews/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "review_ID,reservation_ID,roomId,review1")] review review)
        {
            if (ModelState.IsValid)
            {
                db.review.Add(review);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.reservation_ID = new SelectList(db.Reservation, "reservation_ID", "customerName", review.reservation_ID);
            return View(review);
        }

        // GET: reviews/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            review review = await db.review.FindAsync(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.reservation_ID = new SelectList(db.Reservation, "reservation_ID", "customerName", review.reservation_ID);
            return View(review);
        }

        // POST: reviews/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "review_ID,reservation_ID,roomId,review1")] review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.reservation_ID = new SelectList(db.Reservation, "reservation_ID", "customerName", review.reservation_ID);
            return View(review);
        }

        // GET: reviews/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            review review = await db.review.FindAsync(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            review review = await db.review.FindAsync(id);
            db.review.Remove(review);
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
