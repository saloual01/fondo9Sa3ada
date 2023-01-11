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
    public class ReservationsController : Controller
    {
        private HotelDBEntities3 db = new HotelDBEntities3();

        // GET: Reservations
        public async Task<ActionResult> Index()
        {
            var reservation = db.Reservation.Include(r => r.Room);
            return View(await reservation.ToListAsync());
        }

        // GET: Reservations/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = await db.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservations/Create
        public ActionResult Create()
        {
            ViewBag.roomID = new SelectList(db.Room, "room_ID", "room_number");
            return View();
        }

        // POST: Reservations/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "reservation_ID,roomID,DateDeReservation,Check_In,Check_Out,NumberOfGuests,isPaid,customerName,customerEmail,customerPhone,customerAdress")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservation.Add(reservation);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.roomID = new SelectList(db.Room, "room_ID", "room_number", reservation.roomID);
            return View(reservation);
        }

        // GET: Reservations/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = await db.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.roomID = new SelectList(db.Room, "room_ID", "description", reservation.roomID);
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "reservation_ID,roomID,DateDeReservation,Check_In,Check_Out,NumberOfGuests,isPaid,customerName,customerEmail,customerPhone,customerAdress")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.roomID = new SelectList(db.Room, "room_ID", "description", reservation.roomID);
            return View(reservation);
        }

        // GET: Reservations/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = await db.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Reservation reservation = await db.Reservation.FindAsync(id);
            db.Reservation.Remove(reservation);
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
