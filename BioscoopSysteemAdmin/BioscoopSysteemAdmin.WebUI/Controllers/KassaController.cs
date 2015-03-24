using BioscoopSysteemWebsite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BioscoopSysteemAdmin.WebUI.Controllers
{
    public class KassaController : Controller
    {
        private IRepository irepo;

        public KassaController(IRepository repo) {
            irepo = repo;
        }
        // GET: Kassa
        public ActionResult Index()
        {
            return View();
        }
    }
}