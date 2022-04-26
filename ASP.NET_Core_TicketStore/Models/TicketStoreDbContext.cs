using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_TicketStore.Models
{
    public class TicketStoreDbContext : DbContext
    {
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketCategory> TicketCategories { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<SeatCategory> SeatCategories { get; set; }
        public DbSet<TicketOrder> TicketOrders { get; set; }

        public TicketStoreDbContext(DbContextOptions<TicketStoreDbContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
    }
}