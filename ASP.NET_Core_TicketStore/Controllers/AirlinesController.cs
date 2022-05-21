using ASP.NET_Core_TicketStore.Interfaces;
using ASP.NET_Core_TicketStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_TicketStore.Controllers
{
    public class AirlinesController : Controller
    {
        ITicketsUnitOfWork unitOfWork;

        public AirlinesController(ITicketsUnitOfWork context)
        {
            unitOfWork = context;
        }

        public IActionResult AllAirlines()
        {
            return View(unitOfWork.Airlines.GetAll());
        }

        [HttpGet]
        public IActionResult CreateAirline()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAirline(Airline category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Airlines.Create(category);
                unitOfWork.Save();
                return RedirectToAction("AllAirlines");
            }

            return View();
        }

        [HttpGet]
        public IActionResult EditAirline(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airline = unitOfWork.Airlines.Get(id);
            if (airline != null)
            {
                return View(airline);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult EditAirline(Airline category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Airlines.Update(category);
                unitOfWork.Save();
                return RedirectToAction("AllAirlines");
            }

            return View();
        }

        public IActionResult DeleteAirline(int id)
        {
            unitOfWork.Airlines.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("AllAirlines");
        }
    }
}