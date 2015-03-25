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
            List<Movie> moviesList = repo.GetAllMovies().Where(m => m.StartDate < DateTime.Now && m.EndDate > DateTime.Now).ToList();
            List<Room> roomList = repo.GetAllRooms().ToList();
            List<LadiesNight> ladiesNightList = repo.GetAllLadiesNights().Where(l => l.LadiesNightDay.DayOfYear > DateTime.Now.DayOfYear).ToList();
            List<Show> showList = repo.GetAllShows().Where(s => s.StartTime > DateTime.Now).ToList();
            List<SelectListItem> roomListSelect = new List<SelectListItem>();
            List<SelectListItem> movieListSelect = new List<SelectListItem>();
        }

        [HttpGet]
        public ActionResult VoorstellingToevoegen() {
            List<LadiesNight> ladiesNightList = repo.GetAllLadiesNights().Where(l => l.LadiesNightDay.DayOfYear > DateTime.Now.DayOfYear).ToList();

            ViewBag.ladiesNightSelect = ladiesNightList;
            ViewBag.RoomSelect = RoomSelect();
            ViewBag.MovieSelect = MovieSelect();
            return View();
        }

        [HttpPost]
        public ActionResult VoorstellingToevoegen(Show show) {
            DateTime date = DateTime.Parse(Request["datum"]);
            DateTime newDate = date.Add(TimeSpan.Parse(Request["starttijd"]));
            show.Movie = repo.GetMovieById(show.MovieId);
            List<Room> roomList = repo.GetAllRooms().ToList();
            List<Show> showList = repo.GetAllShows().Where(s => s.Room == repo.GetRoomById(show.RoomId)).ToList();
            show.Room = repo.GetRoomById(show.RoomId);
            if (repo.GetLadiesNightByDate(newDate) == null) {
                show.LadiesNight = null;
            } else {
                show.LadiesNight = repo.GetLadiesNightByDate(newDate);
                show.LadiesNightid = show.LadiesNight.LadiesNightid;
            }
            
            if(newDate.Date < show.Movie.EndDate.Date) {
                foreach (Show x in showList) {
                    Double doubleDuration = Double.Parse(show.Movie.Duration.ToString());
                    TimeSpan span = TimeSpan.FromMinutes(doubleDuration);
                   
                    if (newDate.TimeOfDay > x.StartTime.TimeOfDay && newDate < (x.StartTime.AddMinutes(doubleDuration))) {
                        List<LadiesNight> ladiesNightList = repo.GetAllLadiesNights().Where(l => l.LadiesNightDay.DayOfYear > DateTime.Now.DayOfYear).ToList();
                        ViewBag.ladiesNightSelect = ladiesNightList;
                        ViewBag.RoomSelect = RoomSelect();
                        ViewBag.MovieSelect = MovieSelect();
                        ViewBag.TimeError = "Deze zaal is bezet op de gegeven starttijd.";
                        return View();
                    }
                }
            } else {
                List<LadiesNight> ladiesNightList = repo.GetAllLadiesNights().Where(l => l.LadiesNightDay.DayOfYear > DateTime.Now.DayOfYear).ToList();
                ViewBag.ladiesNightSelect = ladiesNightList;
                ViewBag.RoomSelect = RoomSelect();
                ViewBag.MovieSelect = MovieSelect();
                ViewBag.DateError = "De film is op deze dag niet meer beschikbaar.";
            }

            repo.AddShow(show);

            return View("Index", "Home");
        }

        private List<SelectListItem> MovieSelect() {
            List<SelectListItem> movieSelect = new List<SelectListItem>();
            List<Movie> availableMovies = repo.GetAllMovies().Where(m => m.StartDate < DateTime.Now && m.EndDate > DateTime.Now).ToList();
            SelectListItem x = null;
            foreach (Movie movie in availableMovies) {
                for (int i = 0; i < availableMovies.Count(); i++) {
                    x = new SelectListItem() { Text = movie.Name, Value = movie.MovieId.ToString() };
                }
                movieSelect.Add(x);
            }
            return movieSelect;
        }

        private List<SelectListItem> RoomSelect() {
            List<SelectListItem> roomSelect = new List<SelectListItem>();
            List<Room> allRooms =  repo.GetAllRooms().ToList();
             SelectListItem x = null;
            foreach (Room room in allRooms) {
                for (int i = 0; i < allRooms.Count(); i++) {
                    x = new SelectListItem() { Text = room.RoomNumber.ToString(), Value = room.RoomNumber.ToString() };
                }
                roomSelect.Add(x);
            }
            return roomSelect;
        }
    }
}