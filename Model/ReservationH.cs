using System.ComponentModel.DataAnnotations;

namespace Reservation.Model
{
    public class ReservationH
    {
        [Key]
        public int ReservationId { get; set; }

        [Required]
        public DateTime From { get; set; }

        [Required]
        public DateTime To { get; set; }

        // Foreign Keys
        public int UserId { get; set; }
        public int RoomId { get; set; }
        
    }
}
