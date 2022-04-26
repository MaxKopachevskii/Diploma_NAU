using System;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_Core_TicketStore.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Display(Name = "Назва маршруту")]
        public string Name { get; set; }

        [Display(Name = "Опис маршруту")]
        public string Desc { get; set; }
        
        [Display(Name = "Звідки")]
        public string From { get; set; }

        [Display(Name = "Куди")]
        public string To { get; set; }

        [Display(Name = "Доступна кількість місць")]
        public int AvailableNumberOfSeats { get; set; }
        
        [Display(Name = "Ціна за людину")]
        public int Price { get; set; }

        [Display(Name = "Шлях до зображення")]
        public string Img { get; set; }

        [Display(Name = "Вибране")]
        public bool IsFavorite { get; set; }

        [Display(Name = "В корзині")]
        public bool InBasket { get; set; }

        [Display(Name = "Категорiя")]
        public int? TicketCategoryId { get; set; }
        public TicketCategory TicketCategory { get; set; }
        
        [Display(Name = "Авіакомпанія")]
        public int? AirlineId { get; set; }
        public Airline Airline { get; set; }

        public Ticket()
        {
            IsFavorite = false;
            InBasket = false;
        }
    }
}