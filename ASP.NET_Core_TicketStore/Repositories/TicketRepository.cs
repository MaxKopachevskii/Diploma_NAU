using System.Collections.Generic;
using System.Linq;
using ASP.NET_Core_TicketStore.Interfaces;
using ASP.NET_Core_TicketStore.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_TicketStore.Repositories
{
    public class TicketRepository : IRepository<Ticket>
    {
        TicketStoreDbContext db;

        public TicketRepository(TicketStoreDbContext context)
        {
            db = context;
        }

        public IQueryable<Ticket> GetAll()
        {
            return db.Tickets
                .Include(m => m.TicketCategory)
                .Include(m => m.Airline);
        }

        public IQueryable<Ticket> GetAllTicketsByCategory(int categoryId)
        {
            return db.Tickets
                .Include(m => m.TicketCategory)
                .Include(m => m.Airline)
                .Where(item => item.TicketCategoryId == categoryId);
        }

        public IEnumerable<Ticket> GetAllInBasket()
        {
            return db.Tickets
                .Include(m => m.TicketCategory)
                .Include(m => m.Airline)
                .Where(item => item.InBasket == true).ToList();
        }

        public IQueryable<Ticket> GetAllFavoriteTickets()
        {
            return db.Tickets
                .Include(m => m.TicketCategory)
                .Include(m => m.Airline)
                .Where(item => item.IsFavorite == true);
        }

        public Ticket Get(int? id)
        {
            return db.Tickets
                .Include(m => m.TicketCategory)
                .Include(m => m.Airline)
                .FirstOrDefault(item => item.Id == id);
        }

        public void Create(Ticket item)
        {
            db.Tickets.Add(item);
        }

        public void Update(Ticket item)
        {
            db.Tickets.Update(item);
        }

        public void Delete(int id)
        {
            var book = db.Tickets.Find(id);
            if (book != null)
            {
                db.Tickets.Remove(book);
            }
        }
    }
}