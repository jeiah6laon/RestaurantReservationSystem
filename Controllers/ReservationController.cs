using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantReservationSystem.Data;
using RestaurantReservationSystem.Models;
using static RestaurantReservationSystem.Models.ReservationModel;

namespace RestaurantReservationSystem.Controllers
{
    public class ReservationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Reservation
        public async Task<IActionResult> Index()
        {
              return _context.Reservations != null ? 
                          View(await _context.Reservations.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Reservations'  is null.");
        }

        // GET: Reservation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservationModel = await _context.Reservations
                .FirstOrDefaultAsync(m => m.id == id);
            if (reservationModel == null)
            {
                return NotFound();
            }

            return View(reservationModel);
        }

        // GET: Reservation/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reservation/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DateTime reservedDateTime,TimeEnum timeSlot,[Bind("id,name,contactNo,pax,reservedDateTime,timeSlot")] ReservationModel reservationModel)
        {

            /*
            if (reservationModel.endTime < reservationModel.startTime)
            {
                ModelState.AddModelError("endTime", "End time should be later than start time.");
                return View(reservationModel);
            }
            */
            var regDateTime = _context.Reservations.Where(dt => dt.reservedDateTime == reservedDateTime).FirstOrDefault();
            var regTimeSlot = _context.Reservations.Where(ts => ts.timeSlot == timeSlot).FirstOrDefault();
            
            if (regTimeSlot != null)
            {
                if(regDateTime != null)
                {
                    ModelState.AddModelError("timeSlot", "Time slot is already taken.");
                    return View(reservationModel);
                }                  
            }

            if (ModelState.IsValid)
            {
                _context.Add(reservationModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservationModel);
        }

        // GET: Reservation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservationModel = await _context.Reservations.FindAsync(id);
            if (reservationModel == null)
            {
                return NotFound();
            }
            return View(reservationModel);
        }

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DateTime reservedDateTime, TimeEnum timeSlot, [Bind("id,name,contactNo,pax,reservedDate,timeSlot")] ReservationModel reservationModel)
        {
           

            if (id != reservationModel.id)
            {
                return NotFound();
            }

            var regDateTime = _context.Reservations.Where(dt => dt.reservedDateTime == reservedDateTime).FirstOrDefault();
            var regTimeSlot = _context.Reservations.Where(ts => ts.timeSlot == timeSlot).FirstOrDefault();

            if (regTimeSlot != null)
            {
                if (regDateTime != null)
                {
                    ModelState.AddModelError("timeSlot", "Time slot is already taken.");
                    return View(reservationModel);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservationModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationModelExists(reservationModel.id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(reservationModel);



        }

        // GET: Reservation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reservations == null)
            {
                return NotFound();
            }

            var reservationModel = await _context.Reservations
                .FirstOrDefaultAsync(m => m.id == id);
            if (reservationModel == null)
            {
                return NotFound();
            }

            return View(reservationModel);
        }

        // POST: Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Reservations == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Reservations'  is null.");
            }
            var reservationModel = await _context.Reservations.FindAsync(id);
            if (reservationModel != null)
            {
                _context.Reservations.Remove(reservationModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationModelExists(int id)
        {
          return (_context.Reservations?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
