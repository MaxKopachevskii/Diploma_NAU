using System;
using ASP.NET_Core_TicketStore.Repositories;

namespace ASP.NET_Core_TicketStore.Interfaces
{
    public interface ITicketsUnitOfWork : IDisposable
    {
        TicketRepository Tickets { get; }
        TicketCategoryRepository TicketCategories { get; }
        AirlineRepository Airlines { get; }
        SeatCategoryRepository SeatCategories { get; }
        TicketOrderRepository TicketOrders { get; }
        void Save();
    }
}