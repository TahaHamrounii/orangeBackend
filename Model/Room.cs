using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Reservation.Model
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        [Required]
        public string RoomNumber { get; set; }
        public string Status { get; set; }
        public float Price { get; set; }
        [Required]
        public string Type { get; set; }

    }
}
