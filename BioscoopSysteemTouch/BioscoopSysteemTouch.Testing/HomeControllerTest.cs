using BioscoopSysteemWebsite.Domain.Entities;
using BioscoopSysteemWebsite.Domain.Interfaces;
using BioscoopSysteemTouch.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace BioscoopSysteemTouch.UnitTests {
    [TestClass]
    public class HomeControllerTest {
        /*[TestMethod]
        public void GetIndex() {
            Mock<IRepository> mock = new Mock<IRepository>();
            var controller = new HomeController(mock.Object);
            var result = controller.Index();

            Assert.IsNotNull(result);
        }*/
        [TestMethod]
        public void GetReservation() {
            Mock<IRepository> mock = new Mock<IRepository>();

            var controller = new HomeController(mock.Object);
            var result = controller.Reservation();

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void GetOverViewWithList() {
            //Arrange
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllShows()).Returns(new List<Show> {
                    new Show {ShowID = 1, Movie = new Movie { MovieId = 1, Name = "Lord of the Rings", Description = "Een hele leuke fantasyfilm", ImageData = null, Language = "Engels", Subtitle = "Nederlands", Duration= 200, Type = true, Genre = "Fantasy", StartDate = DateTime.Parse("13-12-2015"), EndDate = DateTime.Parse("2-2-2016"), Pegi = new Pegi() { Horror = true  }}, Room = new Room() { RoomId = 1, RoomNumber = 1, Accessibility = true, Rows = new List<RoomRow>(), Type = true}, AssignedSeats = new List<Seat>(), StartTime = DateTime.Parse("14:00:00")},
                    new Show {ShowID = 1, Movie = new Movie { MovieId = 2, Name = "American History X", Description = "Racism to the max", ImageData = null, Language = "Engels", Subtitle = "Nederlands", Duration= 150, Type = false, Genre = "Aktie", StartDate = DateTime.Parse("11-11-2015"), EndDate = DateTime.Parse("5-4-2016"), Pegi = new Pegi() { Racism = true  }}, Room = new Room() { RoomId = 2, RoomNumber = 2, Accessibility = false, Rows = new List<RoomRow>(), Type = false}, AssignedSeats = new List<Seat>(), StartTime = DateTime.Parse("21:00:00")}
           });
            var controller = new HomeController(mock.Object);

            //Act 
            var result = controller.Overview();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetOverViewWithoutList() {
            //Arrange
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllShows()).Returns(new List<Show> { });
            var controller = new HomeController(mock.Object);

            //Act
            var result = controller.Overview();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetReservationWithGet() {
            //Arrange
            Mock<IRepository> mock = new Mock<IRepository>();

            var controller = new HomeController(mock.Object);

            //Act
            var result = controller.Reservation();

            //Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetPay() {
            //Arrange
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetOrderByID(1)).Returns(new Order(
                new Show {
                    ShowID = 1,
                    Movie = new Movie {
                        MovieId = 1,
                        Name = "Lord of the Rings",
                        Description = "Een hele leuke fantasyfilm",
                        ImageData = null,
                        Language = "Engels",
                        Subtitle = "Nederlands",
                        Duration = 200,
                        Type = true,
                        Genre = "Fantasy",
                        StartDate = DateTime.Parse("13-12-2015"),
                        EndDate = DateTime.Parse("2-2-2016"),
                        Pegi =
                            new Pegi() { Horror = true }
                    },
                    Room =
                        new Room() {
                            RoomId = 1,
                            RoomNumber = 1,
                            Accessibility = true,
                            Rows =
                                new List<RoomRow>(),
                            Type = true
                        },
                    AssignedSeats =
                        new List<Seat>(),
                    StartTime = DateTime.Parse("14:00:00")
                }, false) {
                    OrderId = 1,
                    PickedUp = false,
                    Paid = false,
                    Tickets =
                        new List<Ticket>()
            {new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}}}
                });

            var controller = new HomeController(mock.Object);

            //Act
            var result = controller.Pay(mock.Object.GetOrderByID(1));

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
