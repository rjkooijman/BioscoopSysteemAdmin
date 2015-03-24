using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;
using BioscoopSysteemWebsite.Domain.Interfaces;
using BioscoopSysteemWebsite.WebUI.Controllers;
using BioscoopSysteemWebsite.Domain.Entities;
using System.Collections.Generic;

namespace BioscoopSysteemWebsite.Testing {
    [TestClass]
    public class HomeControllerTest {

        //Geschreven door Frank Molengraaf
        //Getest door Robert-Jan Kooijman
        [TestMethod]
        public void OverviewTestWithoutList() {
            Mock<IRepository> mock = new Mock<IRepository>();
            var controller = new HomeController(mock.Object);

            //Act
            var result = controller.Overview(4);

            //Assert
            Assert.IsNotNull(result);
        }

        //Geschreven door Frank Molengraaf
        //Getest door Ricardo Jobse
        [TestMethod]
        public void GetOverviewWithList() {
            //Arrange
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllShows()).Returns(new List<Show> {
                    new Show {ShowID = 1, Movie = 
                        new Movie { MovieId = 1, Name = "Lord of the Rings", Description = "Een hele leuke fantasyfilm", ImageData = null, Language = "Engels", Subtitle = "Nederlands", Duration= 200, Type = true, Genre = "Fantasy", StartDate = DateTime.Parse("13-12-2015"), EndDate = DateTime.Parse("2-2-2016"), Pegi = 
                        new Pegi() { Horror = true  }}, Room = 
                        new Room() { RoomId = 1, RoomNumber = 1, Accessibility = true, Rows = 
                        new List<RoomRow>(), Type = true}, AssignedSeats = 
                        new List<Seat>(), StartTime = DateTime.Parse("14:00:00")},
                    new Show {ShowID = 1, Movie = 
                        new Movie { MovieId = 2, Name = "American History X", Description = "Racism to the max", ImageData = null, Language = "Engels", Subtitle = "Nederlands", Duration= 150, Type = false, Genre = "Aktie", StartDate = DateTime.Parse("11-11-2015"), EndDate = DateTime.Parse("5-4-2016"), Pegi = 
                        new Pegi() { Racism = true  }}, Room = 
                        new Room() { RoomId = 2, RoomNumber = 2, Accessibility = false, Rows = 
                        new List<RoomRow>(), Type = false}, AssignedSeats = 
                        new List<Seat>(), StartTime = DateTime.Parse("21:00:00")}
           });
            var controller = new HomeController(mock.Object);

            //Act 
            var result = controller.Overview(4);

            //Assert
            Assert.IsNotNull(result);
        }

        //Geschreven door Ricardo Jobse
        //Getest door Robert-Jan Kooijman
        [TestMethod]
        public void SearchTitelTest() {
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllShows()).Returns(new List<Show> {
                    new Show {ShowID = 1, Movie = 
                        new Movie { MovieId = 1, Name = "Lord of the Rings", Description = "Een hele leuke fantasyfilm", ImageData = null, Language = "Engels", Subtitle = "Nederlands", Duration= 200, Type = true, Genre = "Fantasy", StartDate = DateTime.Parse("13-12-2015"), EndDate = DateTime.Parse("2-2-2016"), Pegi = 
                        new Pegi() { Horror = true  }}, Room = 
                        new Room() { RoomId = 1, RoomNumber = 1, Accessibility = true, Rows = 
                        new List<RoomRow>(), Type = true}, AssignedSeats = 
                        new List<Seat>(), StartTime = DateTime.Parse("14:00:00")},
                    new Show {ShowID = 1, Movie = 
                        new Movie { MovieId = 2, Name = "American History X", Description = "Racism to the max", ImageData = null, Language = "Engels", Subtitle = "Nederlands", Duration= 150, Type = false, Genre = "Aktie", StartDate = DateTime.Parse("11-11-2015"), EndDate = DateTime.Parse("5-4-2016"), Pegi = 
                        new Pegi() { Racism = true  }}, Room = 
                        new Room() { RoomId = 2, RoomNumber = 2, Accessibility = false, Rows = 
                        new List<RoomRow>(), Type = false}, AssignedSeats = 
                        new List<Seat>(), StartTime = DateTime.Parse("21:00:00")}
           });
            var controller = new HomeController(mock.Object);

            //Act
            var result = controller.Search("Titel", "Lord");

            //Arrange
            Assert.IsNotNull(result);
        }

        //Geschreven door Ricardo Jobse
        //Getest door Robert-Jan Kooijman
        [TestMethod]
        public void SearchTimeTest() {
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllShows()).Returns(new List<Show> {
                    new Show {ShowID = 1, Movie = 
                        new Movie { MovieId = 1, Name = "Lord of the Rings", Description = "Een hele leuke fantasyfilm", ImageData = null, Language = "Engels", Subtitle = "Nederlands", Duration= 200, Type = true, Genre = "Fantasy", StartDate = DateTime.Parse("13-12-2015"), EndDate = DateTime.Parse("2-2-2016"), Pegi = 
                        new Pegi() { Horror = true  }}, Room = 
                        new Room() { RoomId = 1, RoomNumber = 1, Accessibility = true, Rows = 
                        new List<RoomRow>(), Type = true}, AssignedSeats = 
                        new List<Seat>(), StartTime = DateTime.Parse("14:00:00")},
                    new Show {ShowID = 1, Movie = 
                        new Movie { MovieId = 2, Name = "American History X", Description = "Racism to the max", ImageData = null, Language = "Engels", Subtitle = "Nederlands", Duration= 150, Type = false, Genre = "Aktie", StartDate = DateTime.Parse("11-11-2015"), EndDate = DateTime.Parse("5-4-2016"), Pegi = 
                        new Pegi() { Racism = true  }}, Room = 
                        new Room() { RoomId = 2, RoomNumber = 2, Accessibility = false, Rows = 
                        new List<RoomRow>(), Type = false}, AssignedSeats = 
                        new List<Seat>(), StartTime = DateTime.Parse("21:00:00")}
           });
            var controller = new HomeController(mock.Object);

            //Act
            var result = controller.Search("Tijd", "14:00");

            //Arrange
            Assert.IsNotNull(result);
        }
    }
}
