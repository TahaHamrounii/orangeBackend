using Microsoft.EntityFrameworkCore;
using Reservation.Model;
using Reservation.Data;

namespace Reservation.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ReservationH> Reservations { get; set; }
        public DbSet<Room> Rooms { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=hotel.db");
        }
    }
}


