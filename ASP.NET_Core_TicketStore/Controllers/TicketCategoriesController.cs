using ASP.NET_Core_TicketStore.Interfaces;
using ASP.NET_Core_TicketStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_TicketStore.Controllers
{
    public class TicketCategoriesController : Controller
    {
        ITicketsUnitOfWork unitOfWork;
        public TicketCategoriesController(ITicketsUnitOfWork context)
        {
            unitOfWork = context;
        }

        public IActionResult AllCategories()
        {
            return View(unitOfWork.TicketCategories.GetAll());
        }

        [HttpGet]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateCategory(TicketCategory category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.TicketCategories.Create(category);
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
            var category = unitOfWork.TicketCategories.Get(id);
            if (category != null)
            {
                return View(category);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult EditCategory(TicketCategory category)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.TicketCategories.Update(category);
                unitOfWork.Save();
                return RedirectToAction("AllCategories");
            }
            return View();
        }

        public IActionResult DeleteCategory(int id)
        {
            unitOfWork.TicketCategories.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("AllCategories");
        }
    }
}