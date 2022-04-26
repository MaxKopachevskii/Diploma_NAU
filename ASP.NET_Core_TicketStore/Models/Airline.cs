using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_TicketStore.Models
{
    public class Airline
    {
        public int Id { get; set; }

        [Display(Name = "Назва авіакомпанії")]
        public string Name { get; set; }

        [Display(Name = "Опис")]
        public string ShortDesc { get; set; }

        public virtual IEnumerable<Ticket> Tickets { get; set; }

        public Airline()
        {
            Tickets = new List<Ticket>();
        }
    }
}