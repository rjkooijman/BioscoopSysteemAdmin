using BioscoopSysteemWebsite.Domain.Entities;
using BioscoopSysteemWebsite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BioscoopSysteemAdmin.WebUI.Controllers {
    public class PlannerController : Controller {

        private IRepository repo;

        public PlannerController(IRepository repo) {
            this.repo = repo;
        }

        public ActionResult Index() {
            List<Show> showList = repo.GetAllShows().Where(s => s.StartTime > DateTime.Now).ToList();
            return View(showList);
        }
    }
}