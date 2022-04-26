using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_TicketStore.Models
{
    public class TicketOrder
    {
        [Display(Name = "Номер замовлення")]
        public int Id { get; set; }

        [Display(Name = "Ім'я та прізвище")]
        public string FullName { get; set; }

        [Display(Name = "Номер телефону")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Комментарії до замовлення")]
        public string Comment { get; set; }

        [Display(Name = "Дата оформлення замовлення")]
        public DateTime Date { get; set; }
        
        [Display(Name = "Кількість людей")]
        public int NumberOfPersons { get; set; }
        
        [Display(Name = "Дата вильоту")]
        public DateTime DepartureDate { get; set; }
        
        [Display(Name = "Класс місць")]
        public int? SeatCategoryId { get; set; }
        public SeatCategory SeatCategory { get; set; }

        public IEnumerable<Ticket> OrderTickets { get; set; }
        public TicketOrder()
        {
            OrderTickets = new List<Ticket>();
            Date = DateTime.Now;
        }
    }
}