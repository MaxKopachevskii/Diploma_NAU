using System.Linq;
using ASP.NET_Core_TicketStore.Interfaces;
using ASP.NET_Core_TicketStore.Models;

namespace ASP.NET_Core_TicketStore.Repositories
{
    public class AirlineRepository : IRepository<Airline>
    {
        TicketStoreDbContext db;

        public AirlineRepository(TicketStoreDbContext context)
        {
            db = context;
        }
        public IQueryable<Airline> GetAll()
        {
            return db.Airlines;
        }

        public Airline Get(int? id)
        {
            return db.Airlines.Find(id);
        }

        public void Create(Airline item)
        {
            db.Airlines.Add(item);
        }

        public void Update(Airline item)
        {
            db.Airlines.Update(item);
        }

        public void Delete(int id)
        {
            var category = db.Airlines.Find(id);
            if (category != null)
            {
                db.Airlines.Remove(category);
            }
        }
    }
}