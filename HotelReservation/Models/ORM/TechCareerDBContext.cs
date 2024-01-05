using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Models.ORM
{
    public class TechCareerDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //connection string
            optionsBuilder.UseSqlServer("Server=KURSAD; Database=HotelReservation; trusted_connection=true");
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
    }
}
