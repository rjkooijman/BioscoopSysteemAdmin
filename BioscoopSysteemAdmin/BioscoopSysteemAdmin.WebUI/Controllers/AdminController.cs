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
        private IRepository irepo;

        public AdminController(IRepository repo) {
            irepo = repo;
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
    }
}