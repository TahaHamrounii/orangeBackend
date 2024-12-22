using Microsoft.AspNetCore.Mvc;
using Reservation.Model;
using Reservation.Data;

namespace Reservation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ReservationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllReservations()
        {
            var reservations = _context.Reservations.ToList();
            return Ok(reservations);
        }

        [HttpGet("{id}")]
        public IActionResult GetReservationById(int id)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation == null) return NotFound();
            return Ok(reservation);
        }


        [HttpPost]
        public IActionResult AddReservation(ReservationH reservation)
        {
            _context.Reservations.Add(reservation);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetReservationById), new { id = reservation.ReservationId }, reservation);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateReservation(int id, ReservationH updatedReservation)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation == null) return NotFound();

            reservation.From = updatedReservation.From;
            reservation.To = updatedReservation.To;
            reservation.UserId = updatedReservation.UserId;
            reservation.RoomId = updatedReservation.RoomId;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            var reservation = _context.Reservations.Find(id);
            if (reservation == null) return NotFound();

            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
