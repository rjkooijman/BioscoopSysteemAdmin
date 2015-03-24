using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BioscoopSysteemWebsite.Domain.Entities;
using BioscoopSysteemWebsite.Domain.Interfaces;

namespace BioscoopSysteemAdmin.WebUI.Controllers {
    public class HomeController : Controller {
        private IRepository repo;

        public HomeController(IRepository repo) {
            this.repo = repo;
        }
        [HttpGet]
        public ViewResult Index() {
            return View();
        }

        [HttpPost]
        public ViewResult Index(User user) {
            if (ModelState.IsValid) {
                //Get the information of the user
                User actualUser = repo.GetUserById(user.UserId);
                if (actualUser == null) {
                    ViewBag.IncorrectUser = "De logincode die u probeert te gebruiken bestaat niet.";
                    return View();
                }
                //check the login information
                if (user.Password != actualUser.Password) {
                    ViewBag.IncorrectPassword = "Gebruikersnaam en Wachtwoord combinatie is niet correct.";
                    return View();
                } else {
                    //check user type and sent to correct view
                    List<Show> showList = repo.GetAllShows().Where(s => s.StartTime > DateTime.Now).ToList();
                    ViewBag.showList = showList;
                    return View(actualUser.Role.Role, actualUser);
                }
            } else {
                //there is a validation error
                return View();
            }
        }
    }
}