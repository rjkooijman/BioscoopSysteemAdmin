using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BioscoopSysteemWebsite.Domain.Entities;

namespace BioscoopSysteemWebsite.Testing {
    [TestClass]
    public class MailTest {

        //Geschreven door Robert-Jan Kooijman
        //Getest door Frank Molengraaf
        [TestMethod]
        public void checkMailAdresFalseTest() {

            //Arrange
            Mail mail = new Mail() { MailId = 1, MailAdres = "testtest@test.nl" };

            //Act
            mail.checkMailAdres("testtest@test.nl");

            //Arrange
            Assert.IsFalse(mail.checkMailAdres("testtest@test.nl"));
        }

        [TestMethod]
        public void checkMailAdresTrueTest() {
            //Arrange
            Mail mail = new Mail() { MailId = 1, MailAdres = "testtest@test.nl" };

            //Act
            mail.checkMailAdres("testtest1@test.nl");

            //Assert
            Assert.IsTrue(mail.checkMailAdres("testtest1@test.nl"));
        }

        [TestMethod]
        public void checkMailAdresEmptyTest() {
            
            //Arrange
            Mail mail = new Mail() { MailId = 1, MailAdres = "" };

            //Act
            mail.checkMailAdres("");

            //Assert
            Assert.IsTrue(mail.checkMailAdres(""));
        }
    }
}
