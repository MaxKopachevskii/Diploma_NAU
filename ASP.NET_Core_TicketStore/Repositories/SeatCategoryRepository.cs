using System.Linq;
using ASP.NET_Core_TicketStore.Interfaces;
using ASP.NET_Core_TicketStore.Models;

namespace ASP.NET_Core_TicketStore.Repositories
{
    public class SeatCategoryRepository : IRepository<SeatCategory>
    {
        TicketStoreDbContext db;

        public SeatCategoryRepository(TicketStoreDbContext context)
        {
            db = context;
        }
        public IQueryable<SeatCategory> GetAll()
        {
            return db.SeatCategories;
        }

        public SeatCategory Get(int? id)
        {
            return db.SeatCategories.Find(id);
        }

        public void Create(SeatCategory item)
        {
            db.SeatCategories.Add(item);
        }

        public void Update(SeatCategory item)
        {
            db.SeatCategories.Update(item);
        }

        public void Delete(int id)
        {
            var category = db.SeatCategories.Find(id);
            if (category != null)
            {
                db.SeatCategories.Remove(category);
            }
        }
    }
}