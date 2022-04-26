using ASP.NET_Core_TicketStore.Interfaces;
using ASP.NET_Core_TicketStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_TicketStore.Controllers
{
    public class SeatCategoriesController : Controller
    {
        ITicketsUnitOfWork unitOfWork;
        public SeatCategoriesController(ITicketsUnitOfWork context)
        {
            unitOfWork = context;
        }

        public IActionResult AllCategories()
        {
            return View(unitOfWork.SeatCategories.GetAll());
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(SeatCategory category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.SeatCategories.Create(category);
                unitOfWork.Save();
                return RedirectToAction("AllCategories");
            }
            return View();
        }

        [HttpGet]
        public IActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = unitOfWork.SeatCategories.Get(id);
            if (category != null)
            {
                return View(category);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditCategory(SeatCategory category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.SeatCategories.Update(category);
                unitOfWork.Save();
                return RedirectToAction("AllCategories");
            }
            return View();
        }

        public IActionResult DeleteCategory(int id)
        {
            unitOfWork.SeatCategories.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("AllCategories");
        }
    }
}