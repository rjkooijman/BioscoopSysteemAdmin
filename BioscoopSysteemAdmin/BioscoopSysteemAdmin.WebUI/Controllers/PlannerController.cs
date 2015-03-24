using BioscoopSysteemWebsite.Domain.Entities;
using BioscoopSysteemWebsite.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BioscoopSysteemWebsite.Domain.Entities;

namespace BioscoopSysteemAdmin.WebUI.Controllers {
    public class PlannerController : Controller {

        private IRepository repo;

        public PlannerController(IRepository repo) {
            this.repo = repo;
        }

        [HttpGet]
        public ActionResult VoorstellingToevoegen() {
            List<Movie> moviesList = repo.GetAllMovies().Where(m => m.StartDate < DateTime.Now && m.EndDate > DateTime.Now).ToList();
            List<Room> roomList = repo.GetAllRooms().ToList();
            List<SelectListItem> roomListSelect = new List<SelectListItem>();
            List<SelectListItem> movieListSelect = new List<SelectListItem>();
            SelectListItem x = null;
            foreach(Movie movie in moviesList) {
                for (int i = 0; i < moviesList.Count(); i++) {
                    x = new SelectListItem() { Text = movie.Name, Value = movie.MovieId.ToString() };
                }
                movieListSelect.Add(x);
            }
            foreach (Room room in roomList) {
                for(int i = 0; i < roomList.Count(); i++) {
                    x = new SelectListItem() { Text = room.RoomNumber.ToString(), Value = room.RoomNumber.ToString()};
                }
                roomListSelect.Add(x);
            }
            ViewBag.RoomSelect = roomListSelect;
            ViewBag.MovieSelect = movieListSelect;
            return View();
        }
    }
}