using DutchTreat.Data;
using DutchTreat.Services;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly DutchContext _context;

        public AppController(IMailService mailService, DutchContext context)
        {
            _mailService = mailService;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("contact")]
        public IActionResult Contact()
        {
            ViewBag.Title = "Contact us!";
            return View();
        }

        [HttpPost("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendMessage("valenciacristhian47@gmail.com", model.Asunto, $"De: {model.Nombre} - {model.Email}, Mensaje: {model.Mensaje}");
                ViewBag.UserMessage = "Email enviado";
                ModelState.Clear();
                return View();
            }
            return View(model);
        }

        public IActionResult About()
        {
            ViewBag.Title = "About us";
            return View();
        }

        public IActionResult Shop()
        {
            var results = _context.Products
                .OrderBy(p => p.Category)
                .ToList();
            return View(results);
        }
    }
}
