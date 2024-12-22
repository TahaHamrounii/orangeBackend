using System.ComponentModel.DataAnnotations;

namespace Reservation.Model
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required]
        public int Cin { get; set; }
    }
}
