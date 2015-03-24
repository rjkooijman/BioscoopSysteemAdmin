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
    public class PrintControllerTest {

        [TestMethod]
        public void CreateObjectTest() {
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

            var controller = new PrintController(mock.Object);

            //Act
            var result = controller.CreateObject(mock.Object.GetOrderByID(1));

            //Assert
            Assert.IsNotNull(result);
        }
    }
}
