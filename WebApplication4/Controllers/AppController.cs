using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication4.ViewModels;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        public AppController(IMailService mailService)
        {
            _mailService = mailService;
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
    }
}
