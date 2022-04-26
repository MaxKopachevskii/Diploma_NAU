using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_TicketStore.Models
{
    public class TicketCategory
    {
        public int Id { get; set; }

        [Display(Name = "Назва категорії")]
        public string Name { get; set; }

        [Display(Name = "Опис категорії")]
        public string ShortDesc { get; set; }

        public virtual IEnumerable<Ticket> Tickets { get; set; }

        public TicketCategory()
        {
            Tickets = new List<Ticket>();
        }
    }
}