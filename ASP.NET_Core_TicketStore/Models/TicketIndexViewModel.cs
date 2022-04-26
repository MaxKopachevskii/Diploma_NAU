using System.Collections.Generic;

namespace ASP.NET_Core_TicketStore.Models
{
    public class TicketIndexViewModel
    {
        public IEnumerable<Ticket> Tickets { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public string Search { get; set; }
    }
}