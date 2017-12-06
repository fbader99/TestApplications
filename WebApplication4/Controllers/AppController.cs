using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.ViewModels;
using WebApplication4.Services;
using WebApplication4.Data;

namespace WebApplication4.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IDutchRepository _repository;
        public AppController(IMailService mailService, IDutchRepository repository)
        {
            _mailService = mailService;
            _repository = repository;
        }
        public IActionResult Index()
        {
            //throw new InvalidOperationException("Custom exception raised");
            return View();
        }
        [HttpGet("/contact")]
        public IActionResult Contact()
        {
            //throw new InvalidOperationException("Conact page has error");
            ViewBag.Title = "Contact us";
            return View();
        }
        [HttpPost("/contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if(ModelState.IsValid)
            {
                _mailService.SendMessage("fshabbir99@gmail.com", model.Subject,$"From: {model.Name} - {model.Email}, Message: {model.Message}");
                ViewBag.UserMessage = "Mail Sent";
                ModelState.Clear();
            }
            else
            {


            }
            return View();
        }
        public IActionResult About()
        {
            ViewBag.Title = "About us";
            return View();
        }

        public IActionResult Shop()
        {
            //var results = _context.Products
            //    .OrderBy(p => p.Category)
            //    .ToList();

            //return View(results.ToList());

            var _results = _repository.GetAllProducts();
            return View(_results.ToList());
        }
    }
}
