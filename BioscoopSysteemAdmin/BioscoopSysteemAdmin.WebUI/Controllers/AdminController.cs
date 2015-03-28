using BioscoopSysteemWebsite.Domain.Entities;
using BioscoopSysteemWebsite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BioscoopSysteemAdmin.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private IRepository repo;

        public AdminController(IRepository repo) {
            this.repo = repo;
        }
        // GET: Admin
        public ActionResult Overview(int userid) {
            if (repo.GetUserById(userid).Role.Role == "Admin") {
                List<TicketSoort> ticketSoort = repo.GetTicketSoorten().ToList();
                ViewBag.Userid = userid;
                return View(ticketSoort);
            } else {
                return View("~/Views/Home/Unauthorized.cshtml");
            }
        }

        [HttpGet]
        public ActionResult TicketToevoegen(int userid) {
            if (repo.GetUserById(userid).Role.Role == "Admin") {
                ViewBag.Userid = userid;
                return View();
            } else {
                return View("~/Views/Home/Unauthorized.cshtml");
            }
        }

        [HttpPost]
        public ActionResult TicketToevoegen(TicketSoort ticket) {
            int userid = int.Parse(Request["userId"]);
            if (repo.GetUserById(userid).Role.Role == "Admin") {
                foreach (TicketSoort t in repo.GetTicketSoorten()) {
                    if (ticket.Naam != t.Naam) {
                        ViewBag.Userid = userid;
                        ticket.Price = Decimal.Parse(ticket.Price.ToString());
                    } else {
                        ViewBag.TicketError = "Een ticket met deze naam bestaat al";
                        return View();
                    }
                }
                repo.AddTicketSoort(ticket);
                return View("TicketSucces", ticket);
            } else {
                return View("~/Views/Home/Unauthorized.cshtml");
            }
        }

        public ActionResult TicketSucces(Ticket ticket) {
            int userid = (int)Session["UserID"];
            if (repo.GetUserById(userid).Role.Role == "Admin") {
                ViewBag.Userid = userid;
                return View();
            } else {
                return View("~/Views/Home/Unauthorized.cshtml");
            }
        }
    }
}