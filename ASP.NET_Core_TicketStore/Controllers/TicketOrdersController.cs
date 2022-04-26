using ASP.NET_Core_TicketStore.Interfaces;
using ASP.NET_Core_TicketStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_TicketStore.Controllers
{
    public class TicketOrdersController : Controller
    {
        ITicketsUnitOfWork unitOfWork;
        public TicketOrdersController(ITicketsUnitOfWork context)
        {
            unitOfWork = context;
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult GetAllOrders()
        {
            var orders = unitOfWork.TicketOrders.GetAll();
            return View(orders);
        }

        [HttpGet]
        public IActionResult CreateOrderForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateOrderForm(TicketOrder order)
        {
            if (ModelState.IsValid)
            {
                order.OrderTickets = unitOfWork.Tickets.GetAllInBasket();
                unitOfWork.TicketOrders.Create(order);
                var books = unitOfWork.Tickets.GetAll();
                foreach (var item in books)
                {
                    if (item.InBasket)
                    {
                        item.AvailableNumberOfSeats--;
                        item.InBasket = false;
                        unitOfWork.Tickets.Update(item);
                    }
                }
                unitOfWork.Save();
                return RedirectToAction("ShowNumberOfOrder", new { orderNumber = order.Id});
            }
            return View();
        }

        public IActionResult ShowNumberOfOrder(int orderNumber)
        {
            ViewBag.OrderNumber = orderNumber;
            return View();
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult DetailsOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = unitOfWork.TicketOrders.Get(id);
            if (order != null)
            {
                return View(order);
            }
            return NotFound();
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult DeleteOrder(int id)
        {
            unitOfWork.TicketOrders.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("AdminPanel","Tickets");
        }

        public void CleanBasket()
        {
            var books = unitOfWork.Tickets.GetAll();
            foreach (var item in books)
            {
                if (item.InBasket)
                {
                    item.InBasket = false;
                    unitOfWork.Tickets.Update(item);
                    unitOfWork.Save();
                }
            }
        }

        public void Sales()
        {
            var books = unitOfWork.Tickets.GetAll();
            foreach (var item in books)
            {
                if (item.InBasket)
                {
                    item.AvailableNumberOfSeats--;
                    unitOfWork.Tickets.Update(item);
                    unitOfWork.Save();
                }
            }
        }
    }
}