using Microsoft.AspNetCore.Mvc;
using Reservation.Model;
using Reservation.Data;
using Microsoft.EntityFrameworkCore;

namespace Reservation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RoomController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllRooms()
        {
            var rooms = _context.Rooms.ToList();
            return Ok(rooms);
        }
        [HttpGet("empty")]
        public IActionResult GetEmptyRooms()
        {
            var rooms = _context.Rooms.Where(r => r.Status == "free").ToList();
            return Ok(rooms);
        }

        [HttpGet("{id}")]
        public IActionResult GetRoomById(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room == null) return NotFound();
            return Ok(room);
        }

        [HttpPost]
        public IActionResult AddRoom(Room room)
        {

            _context.Rooms.Add(room);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetRoomById), new { id = room.RoomId }, room);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoom(int id, Room updatedRoom)
        {
            var room = _context.Rooms.Find(id);
            if (room == null) return NotFound();

            room.RoomNumber = updatedRoom.RoomNumber;
            room.Status = updatedRoom.Status;
            room.Price = updatedRoom.Price;
            room.Type = updatedRoom.Type;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoom(int id)
        {
            var room = _context.Rooms.Find(id);
            if (room == null) return NotFound();

            _context.Rooms.Remove(room);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpGet("room/{price}/{type}")]
        public IActionResult ReturnRooms(int price = 9999, string type = " ")
        {
            var rooms = _context.Rooms
                .Where(r => r.Status == "free" && r.Price <= price && (string.IsNullOrWhiteSpace(type) || r.Type == type))
                .ToList();

            if (rooms.Any())
            {
                return Ok(rooms);
            }

            var closestRoom = (from room in _context.Rooms
                               join reservation in _context.Reservations
                               on room.RoomId equals reservation.RoomId
                               where room.Status == "free" || room.Status == "reserved"
                               orderby reservation.To
                               select new
                               {
                                   Room = room,
                                   ClosestReservation = reservation
                               }).FirstOrDefault();

            if (closestRoom != null)
            {
                return Ok(closestRoom.Room);
            }

            return NotFound("Aucune chambre disponible ne correspond aux critères.");

        }
    }
}
