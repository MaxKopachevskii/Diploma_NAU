using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ASP.NET_Core_TicketStore.Interfaces;
using ASP.NET_Core_TicketStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Core_TicketStore.Controllers
{
    public class TicketsController : Controller
    {
        ITicketsUnitOfWork unitOfWork;

        enum TicketCategories
        {
            WarmRoutes = 1,
            ColdRoutes = 2,
            NatureRoutes = 3,
            CityRoutes = 4
        }

        public TicketsController(ITicketsUnitOfWork context)
        {
            unitOfWork = context;
        }

        public IActionResult Index()
        {
            return RedirectToAction("AllFavoriteTickets");
        }

        public async Task<IActionResult> AllFavoriteTickets(int page = 1)
        {
            int pageSize = 6;   // количество элементов на странице

            IQueryable<Ticket> source = unitOfWork.Tickets.GetAllFavoriteTickets();
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            TicketIndexViewModel viewModel = new TicketIndexViewModel
            {
                PageViewModel = pageViewModel,
                Tickets = items
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AllTickets(string search, int page = 1)
        {
            int pageSize = 6;   // количество элементов на странице

            IQueryable<Ticket> source = unitOfWork.Tickets.GetAll().Where(item => item.Name.Contains(search));
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            TicketIndexViewModel viewModel = new TicketIndexViewModel
            {
                PageViewModel = pageViewModel,
                Tickets = items
            };
            return View(viewModel);
        }

        public async Task<IActionResult> AllTickets(int page = 1)
        {
            int pageSize = 12;   // количество элементов на странице

            IQueryable<Ticket> source = unitOfWork.Tickets.GetAll();
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            TicketIndexViewModel viewModel = new TicketIndexViewModel
            {
                PageViewModel = pageViewModel,
                Tickets = items
            };
            return View(viewModel);
        }

        public async Task<IActionResult> AllWarmRoutes(int page = 1)
        {
            int pageSize = 12;   // количество элементов на странице

            IQueryable<Ticket> source = unitOfWork.Tickets.GetAllTicketsByCategory((int)TicketCategories.WarmRoutes);
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            TicketIndexViewModel viewModel = new TicketIndexViewModel
            {
                PageViewModel = pageViewModel,
                Tickets = items
            };
            return View(viewModel);
        }
        
        public async Task<IActionResult> AllColdRoutes(int page = 1)
        {
            int pageSize = 12;   // количество элементов на странице

            IQueryable<Ticket> source = unitOfWork.Tickets.GetAllTicketsByCategory((int)TicketCategories.ColdRoutes);
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            TicketIndexViewModel viewModel = new TicketIndexViewModel
            {
                PageViewModel = pageViewModel,
                Tickets = items
            };
            return View(viewModel);
        }
        
        public async Task<IActionResult> AllNatureRoutes(int page = 1)
        {
            int pageSize = 12;   // количество элементов на странице

            IQueryable<Ticket> source = unitOfWork.Tickets.GetAllTicketsByCategory((int)TicketCategories.NatureRoutes);
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            TicketIndexViewModel viewModel = new TicketIndexViewModel
            {
                PageViewModel = pageViewModel,
                Tickets = items
            };
            return View(viewModel);
        }
        
        public async Task<IActionResult> AllCityRoutes(int page = 1)
        {
            int pageSize = 12;   // количество элементов на странице

            IQueryable<Ticket> source = unitOfWork.Tickets.GetAllTicketsByCategory((int)TicketCategories.CityRoutes);
            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            TicketIndexViewModel viewModel = new TicketIndexViewModel
            {
                PageViewModel = pageViewModel,
                Tickets = items
            };
            return View(viewModel);
        }
        

        public IActionResult Basket()
        {
            var tickets = unitOfWork.Tickets.GetAllInBasket();
            return View(tickets);
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult AdminPanel()
        {
            var tickets = unitOfWork.Tickets.GetAll();
            return View(tickets);
        }

        public IActionResult TicketInBasket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ticket = unitOfWork.Tickets.Get(id);
            if (ticket != null)
            {
                ticket.InBasket = true;
                unitOfWork.Tickets.Update(ticket);
                unitOfWork.Save();
                return RedirectToAction("DetailsTicket", new { id = id});
            }
            return NotFound();
        }

        public IActionResult TicketOutBasket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ticket = unitOfWork.Tickets.Get(id);
            if (ticket != null)
            {
                ticket.InBasket = false;
                unitOfWork.Tickets.Update(ticket);
                unitOfWork.Save();
                return RedirectToAction("Basket");
            }
            return NotFound();
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpGet]
        public IActionResult CreateTicket()
        {
            return View();
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPost]
        public IActionResult CreateTicket(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Tickets.Create(ticket);
                unitOfWork.Save();
                return RedirectToAction("AdminPanel");
            }
            return View();
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpGet]
        public IActionResult EditTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ticket = unitOfWork.Tickets.Get(id);
            if (ticket != null)
            {
                return View(ticket);
            }
            return NotFound();
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        [HttpPost]
        public IActionResult EditTicket(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.Tickets.Update(ticket);
                unitOfWork.Save();
                return RedirectToAction("AdminPanel");
            }
            return View();
        }

        public IActionResult DetailsTicket(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var ticket = unitOfWork.Tickets.Get(id);
            if (ticket != null)
            {
                return View(ticket);
            }
            return NotFound();
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult DeleteTicket(int id)
        {
            unitOfWork.Tickets.Delete(id);
            unitOfWork.Save();
            return RedirectToAction("AdminPanel");
        }

        public void CleanBasket()
        {
            var tickets = unitOfWork.Tickets.GetAll();
            foreach (var item in tickets)
            {
                if (item.InBasket)
                {
                    item.InBasket = false;
                    unitOfWork.Tickets.Update(item);
                    unitOfWork.Save();
                }
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Delivery()
        {
            return View();
        }
    }
}