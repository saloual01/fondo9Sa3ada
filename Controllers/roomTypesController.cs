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
    public class roomTypesController : Controller
    {
        private HotelDBEntities3 db = new HotelDBEntities3();

        // GET: roomTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.roomType.ToListAsync());
        }

        // GET: roomTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            roomType roomType = await db.roomType.FindAsync(id);
            if (roomType == null)
            {
                return HttpNotFound();
            }
            return View(roomType);
        }

        // GET: roomTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: roomTypes/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "roomTypeId,numberOfBeds,PricePerNight")] roomType roomType)
        {
            if (ModelState.IsValid)
            {
                db.roomType.Add(roomType);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(roomType);
        }

        // GET: roomTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            roomType roomType = await db.roomType.FindAsync(id);
            if (roomType == null)
            {
                return HttpNotFound();
            }
            return View(roomType);
        }

        // POST: roomTypes/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "roomTypeId,numberOfBeds,PricePerNight")] roomType roomType)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomType).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(roomType);
        }

        // GET: roomTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            roomType roomType = await db.roomType.FindAsync(id);
            if (roomType == null)
            {
                return HttpNotFound();
            }
            return View(roomType);
        }

        // POST: roomTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            roomType roomType = await db.roomType.FindAsync(id);
            db.roomType.Remove(roomType);
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
