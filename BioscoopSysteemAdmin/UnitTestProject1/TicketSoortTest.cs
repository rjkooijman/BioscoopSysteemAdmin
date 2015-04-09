using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BioscoopSysteemWebsite.Domain.Interfaces;
using BioscoopSysteemWebsite.Domain.Entities;
using System.Collections.Generic;

namespace BioscoopSysteemAdmin.Test {
    [TestClass]
    public class TicketSoortTest {
        [TestMethod]
        public void AddTicketSoort() {
            //Arrange
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetTicketSoorten()).Returns(new List<TicketSoort> {
                new TicketSoort { TicketSoortID = 1, Naam = "Normaal", Price = Decimal.Parse("8,50")},
                new TicketSoort { TicketSoortID = 2, Naam = "Student", Price = Decimal.Parse("8,00")}
            });
            TicketSoort tstest = new TicketSoort() { Naam = "VIP", Price = Decimal.Parse("13,50"), TicketSoortID = 1 };
            //Act
            mock.Object.GetTicketSoorten().Add(tstest);
            //Assert
            int count = mock.Object.GetTicketSoorten().Count;
            Assert.AreEqual("3", count.ToString());
        }
    }
}
