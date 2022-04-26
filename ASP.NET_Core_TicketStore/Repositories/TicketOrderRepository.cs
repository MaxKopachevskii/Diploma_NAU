using System.Linq;
using ASP.NET_Core_TicketStore.Interfaces;
using ASP.NET_Core_TicketStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_TicketStore.Repositories
{
    public class TicketOrderRepository : IRepository<TicketOrder>
    {
        TicketStoreDbContext db;

        public TicketOrderRepository(TicketStoreDbContext context)
        {
            db = context;
        }
        public IQueryable<TicketOrder> GetAll()
        {
            return db.TicketOrders
                .Include(m => m.OrderTickets)
                .Include(m => m.SeatCategory);
        }

        public TicketOrder Get(int? id)
        {
            return db.TicketOrders
                .Include(m => m.OrderTickets)
                .Include(m => m.SeatCategory)
                .FirstOrDefault(item => item.Id == id);
        }

        public void Create(TicketOrder item)
        {
            db.TicketOrders.Add(item);
        }

        public void Update(TicketOrder item)
        {
            db.TicketOrders.Update(item);
        }

        public void Delete(int id)
        {
            var order = db.TicketOrders.Find(id);
            if (order != null)
            {
                db.TicketOrders.Remove(order);
            }
        }
    }
}