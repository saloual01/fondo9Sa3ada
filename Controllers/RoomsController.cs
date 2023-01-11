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
    public class RoomsController : Controller
    {
        private HotelDBEntities3 db = new HotelDBEntities3();

        // GET: Rooms
        public async Task<ActionResult> Index()
        {
            var room = db.Room.Include(r => r.roomType);
            return View(await room.ToListAsync());
        }

        // GET: Rooms/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = await db.Room.FindAsync(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            ViewBag.room_type_ID = new SelectList(db.roomType, "roomTypeId", "roomTypeId");
            return View();
        }

        // POST: Rooms/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "room_ID,room_number,room_type_ID,pricePerNight,capacity,description,isOccupied,isClean,RoomImage")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Room.Add(room);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.room_type_ID = new SelectList(db.roomType, "roomTypeId", "roomTypeId", room.room_type_ID);
            return View(room);
        }

        // GET: Rooms/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = await db.Room.FindAsync(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.room_type_ID = new SelectList(db.roomType, "roomTypeId", "roomTypeId", room.room_type_ID);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "room_ID,room_number,room_type_ID,pricePerNight,capacity,description,isOccupied,isClean,RoomImage")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.room_type_ID = new SelectList(db.roomType, "roomTypeId", "roomTypeId", room.room_type_ID);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = await db.Room.FindAsync(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Room room = await db.Room.FindAsync(id);
            db.Room.Remove(room);
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
