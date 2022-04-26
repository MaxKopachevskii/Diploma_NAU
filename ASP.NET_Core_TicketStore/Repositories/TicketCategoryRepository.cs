using System.Linq;
using ASP.NET_Core_TicketStore.Interfaces;
using ASP.NET_Core_TicketStore.Models;

namespace ASP.NET_Core_TicketStore.Repositories
{
    public class TicketCategoryRepository : IRepository<TicketCategory>
    {
        TicketStoreDbContext db;

        public TicketCategoryRepository(TicketStoreDbContext context)
        {
            db = context;
        }
        public IQueryable<TicketCategory> GetAll()
        {
            return db.TicketCategories;
        }

        public TicketCategory Get(int? id)
        {
            return db.TicketCategories.Find(id);
        }

        public void Create(TicketCategory item)
        {
            db.TicketCategories.Add(item);
        }

        public void Update(TicketCategory item)
        {
            db.TicketCategories.Update(item);
        }

        public void Delete(int id)
        {
            var category = db.TicketCategories.Find(id);
            if (category != null)
            {
                db.TicketCategories.Remove(category);
            }
        }
    }
}