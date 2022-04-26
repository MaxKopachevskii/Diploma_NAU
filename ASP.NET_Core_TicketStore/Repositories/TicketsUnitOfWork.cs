using System;
using ASP.NET_Core_TicketStore.Interfaces;
using ASP.NET_Core_TicketStore.Models;

namespace ASP.NET_Core_TicketStore.Repositories
{
    public class TicketsUnitOfWork : ITicketsUnitOfWork
    {
        private TicketStoreDbContext db;
        
        private TicketRepository ticketRepository;

        private TicketCategoryRepository ticketCategoryRepository;

        private AirlineRepository airlineRepository;
        
        private SeatCategoryRepository seatCategoryRepository;

        private TicketOrderRepository ticketOrderRepository;
        
        public TicketsUnitOfWork(TicketStoreDbContext context)
        {
            db = context;
        }

        public TicketRepository Tickets
        {
            get
            {
                if (ticketRepository == null)
                    ticketRepository = new TicketRepository(db);
                return ticketRepository;
            }
        }

        public TicketCategoryRepository TicketCategories
        {
            get
            {
                if (ticketCategoryRepository == null)
                    ticketCategoryRepository = new TicketCategoryRepository(db);
                return ticketCategoryRepository;
            }
        }

        public AirlineRepository Airlines
        {
            get
            {
                if (airlineRepository == null)
                    airlineRepository = new AirlineRepository(db);
                return airlineRepository;
            }
        }
        
        public TicketOrderRepository TicketOrders
        {
            get
            {
                if (ticketOrderRepository == null)
                    ticketOrderRepository = new TicketOrderRepository(db);
                return ticketOrderRepository;
            }
        }
        
        public SeatCategoryRepository SeatCategories
        {
            get
            {
                if (seatCategoryRepository == null)
                    seatCategoryRepository = new SeatCategoryRepository(db);
                return seatCategoryRepository;
            }
        }


        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}