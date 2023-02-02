using Microsoft.EntityFrameworkCore;
using RestaurantReservationSystem.Models;

namespace RestaurantReservationSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ReservationModel> Reservations { get; set; }

    }
}
