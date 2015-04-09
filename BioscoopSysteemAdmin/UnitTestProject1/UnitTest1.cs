using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using BioscoopSysteemWebsite.Domain.Interfaces;
using BioscoopSysteemWebsite.Domain.Entities;
using System.Collections.Generic;

namespace BioscoopSysteemAdmin.Test {
    [TestClass]
    public class SubscriberTest {
        [TestMethod]
        public void AddDuplicateSubscription() {
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllSubscribers()).Returns(new List<Subscriber>(){
            new Subscriber() { Voornaam = "RJ", Achternaam = "Test", Huisnummer = 76, Postcode = "4901VP", Straat = "Hoofseweg", Woonplaats = "Oosterhout", SubscriberId = 1 }});

            Subscriber sub = new Subscriber() { Voornaam = "RJ", Achternaam = "Test", Huisnummer = 76, Postcode = "4901VP", Straat = "Hoofseweg", Woonplaats = "Oosterhout", SubscriberId = 2 };
            var result = mock.Object.DuplicateSubscriber(sub);

            Assert.IsTrue(result);

        }

        [TestMethod]
        public void AddSubscriber() {
            Mock<IRepository> mock = new Mock<IRepository>();
            mock.Setup(m => m.GetAllSubscribers()).Returns(new List<Subscriber>(){
                new Subscriber() { Voornaam = "RJ", Achternaam = "Test", Huisnummer = 76, Postcode = "4901VP", Straat = "Hoofseweg", Woonplaats = "Oosterhout", SubscriberId = 1 }
            });

            Subscriber sub = new Subscriber() { Voornaam = "RJ", Achternaam = "Test", Huisnummer = 76, Postcode = "4901VP", Straat = "Hoofseweg", Woonplaats = "Oosterhout", SubscriberId = 2 };
            mock.Object.GetAllSubscribers().Add(sub);

            int count = mock.Object.GetAllSubscribers().Count;
            Assert.IsTrue(count == 2);

            
        }
    }
}
