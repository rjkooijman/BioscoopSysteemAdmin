using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BioscoopSysteemWebsite.Domain.Entities;
using System.Collections.Generic;

namespace BioscoopSysteemWebsite.Testing {
    //Geschreven door Robert-Jan Kooijman
    //Getest door Frank Molengraaf
    [TestClass]
    public class OrderTest {
        [TestMethod]
        public void AddTicketTest() {
            //Arrange
            Order order = new Order(
                new Show {
                    ShowID = 1,
                    PopcornArrangement = false,
                    Movie = new Movie {
                        MovieId = 1,
                        Name = "Lord of the Rings",
                        Description = "Een hele leuke fantasyfilm",
                        ImageData = null,
                        BannerImage = null,
                        Language = "Engels",
                        Subtitle = "Nederlands",
                        Duration = 200,
                        Type = true,
                        Genre = "Fantasy",
                        StartDate = DateTime.Parse("13-12-2015"),
                        EndDate = DateTime.Parse("2-2-2016"),
                        Pegi =
                            new Pegi() { Horror = true },
                        Director = new Director() { DirectorID = 1, Name = "Steven Spielberg" }
                    },
                    Room =
                        new Room() {
                            RoomId = 1,
                            RoomNumber = 1,
                            Accessibility = true,
                            Rows =
                                new List<RoomRow>() {
                            new RoomRow() {RoomRowID = 1, RowNumber = 1, SeatAmount = 25}},
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
                };

            //Act
            order.AddTicket(new TicketSoort() { TicketSoortID = 1, Naam = "Standaard", Price = Decimal.Parse("8,50") }, 4);

            //Assert
            int count = order.Tickets.Count;
            Assert.IsTrue(count == 5);
        }

        [TestMethod]
        public void CalculateTotalPriceNormalTicked2dShortMovie() {
            //Calculate total price : normaal - 2d - korte film
            //Arrange
            Order order = new Order(
                new Show {
                    ShowID = 1,
                    PopcornArrangement = false,
                    Movie = new Movie {
                        MovieId = 1,
                        Name = "Lord of the Rings",
                        Description = "Een hele leuke fantasyfilm",
                        ImageData = null,
                        BannerImage = null,
                        Language = "Engels",
                        Subtitle = "Nederlands",
                        Duration = 100,
                        Type = false,
                        Genre = "Fantasy",
                        StartDate = DateTime.Parse("13-12-2015"),
                        EndDate = DateTime.Parse("2-2-2016"),
                        Pegi =
                            new Pegi() { Horror = true },
                        Director = new Director() { DirectorID = 1, Name = "Steven Spielberg" }
                    },
                    Room =
                        new Room() {
                            RoomId = 1,
                            RoomNumber = 1,
                            Accessibility = true,
                            Rows =
                                new List<RoomRow>() {
                            new RoomRow() {RoomRowID = 1, RowNumber = 1, SeatAmount = 25}},
                            Type = true
                        },
                    AssignedSeats =
                        new List<Seat>(),
                    StartTime = DateTime.Parse("14:00:00")
                }, false)
                {
                    OrderId = 1,
                    PickedUp = false,
                    Paid = false,
                    Tickets =
                        new List<Ticket>()
            {new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}}}
                };

            //Act
            order.GetTotalPrice();

            //Assert
            Assert.IsTrue(order.GetTotalPrice() == Decimal.Parse("8,50"));
        }

        [TestMethod]
        public void GetTotalPriceNormalTicket3dShortMovie() {
            //Calculate total price: 	normaal	- 3d - korte film
            //Arrange
            Order order = new Order(
                new Show {
                    ShowID = 1,
                    PopcornArrangement = false,
                    Movie = new Movie {
                        MovieId = 1,
                        Name = "Lord of the Rings",
                        Description = "Een hele leuke fantasyfilm",
                        ImageData = null,
                        BannerImage = null,
                        Language = "Engels",
                        Subtitle = "Nederlands",
                        Duration = 100,
                        Type = true,
                        Genre = "Fantasy",
                        StartDate = DateTime.Parse("13-12-2015"),
                        EndDate = DateTime.Parse("2-2-2016"),
                        Pegi =
                            new Pegi() { Horror = true },
                        Director = new Director() { DirectorID = 1, Name = "Steven Spielberg" }
                    },
                    Room =
                        new Room() {
                            RoomId = 1,
                            RoomNumber = 1,
                            Accessibility = true,
                            Rows =
                                new List<RoomRow>() {
                            new RoomRow() {RoomRowID = 1, RowNumber = 1, SeatAmount = 25}},
                            Type = true
                        },
                    AssignedSeats =
                        new List<Seat>(),
                    StartTime = DateTime.Parse("14:00:00")
                }, false)
                {
                    OrderId = 1,
                    PickedUp = false,
                    Paid = false,
                    Tickets =
                        new List<Ticket>()
            {new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}}}
                };

            //Act
            order.GetTotalPrice();

            //Assert
            Assert.IsTrue(order.GetTotalPrice() == Decimal.Parse("11,00"));
        }

        [TestMethod]
        public void GetTotalPriceNormalTicket2dLongMovie() {
            //Calculate total price: 	normaal - 2d - lange film
            //Arrange
            Order order = new Order(
                new Show {
                    ShowID = 1,
                    PopcornArrangement = true,
                    Movie = new Movie {
                        MovieId = 1,
                        Name = "Lord of the Rings",
                        Description = "Een hele leuke fantasyfilm",
                        ImageData = null,
                        BannerImage = null,
                        Language = "Engels",
                        Subtitle = "Nederlands",
                        Duration = 150,
                        Type = false,
                        Genre = "Fantasy",
                        StartDate = DateTime.Parse("13-12-2015"),
                        EndDate = DateTime.Parse("2-2-2016"),
                        Pegi =
                            new Pegi() { Horror = true },
                        Director = new Director() { DirectorID = 1, Name = "Steven Spielberg" }
                    },
                    Room =
                        new Room() {
                            RoomId = 1,
                            RoomNumber = 1,
                            Accessibility = true,
                            Rows =
                                new List<RoomRow>() {
                            new RoomRow() {RoomRowID = 1, RowNumber = 1, SeatAmount = 25}},
                            Type = true
                        },
                    AssignedSeats =
                        new List<Seat>(),
                    StartTime = DateTime.Parse("14:00:00")
                }, false)
                {
                    OrderId = 1,
                    PickedUp = false,
                    Paid = false,
                    Tickets =
                        new List<Ticket>()
            {new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}}}
                };

            //Act
            order.GetTotalPrice();

            //Assert
            Assert.IsTrue(order.GetTotalPrice() == Decimal.Parse("9,00"));
        }

        [TestMethod]
        public void GetTotalPriceNormalTicked3dLongMovie() {
            //Calculate total price: normaal - 3d - lange film
            //Arrange
            Order order = new Order(
                new Show {
                    ShowID = 1,
                    PopcornArrangement = true,
                    Movie = new Movie {
                        MovieId = 1,
                        Name = "Lord of the Rings",
                        Description = "Een hele leuke fantasyfilm",
                        ImageData = null,
                        BannerImage = null,
                        Language = "Engels",
                        Subtitle = "Nederlands",
                        Duration = 150,
                        Type = true,
                        Genre = "Fantasy",
                        StartDate = DateTime.Parse("13-12-2015"),
                        EndDate = DateTime.Parse("2-2-2016"),
                        Pegi =
                            new Pegi() { Horror = true },
                        Director = new Director() { DirectorID = 1, Name = "Steven Spielberg" }
                    },
                    Room =
                        new Room() {
                            RoomId = 1,
                            RoomNumber = 1,
                            Accessibility = true,
                            Rows =
                                new List<RoomRow>() {
                            new RoomRow() {RoomRowID = 1, RowNumber = 1, SeatAmount = 25}},
                            Type = true
                        },
                    AssignedSeats =
                        new List<Seat>(),
                    StartTime = DateTime.Parse("14:00:00")
                }, false)
                {
                    OrderId = 1,
                    PickedUp = false,
                    Paid = false,
                    Tickets =
                        new List<Ticket>()
            {new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}}}
                };

            //Act
            order.GetTotalPrice();

            //Assert
            Assert.IsTrue(order.GetTotalPrice() == Decimal.Parse("11,50"));
        }

        [TestMethod]
        public void GetTotalPrice2TicketsNormalPopcorn2dShortMovie() {
            //Calculate total price: normaal&popcorn 2d - korte film
            //Arrange
            Order order = new Order(
                new Show {
                    ShowID = 1,
                    PopcornArrangement = true,
                    Movie = new Movie {
                        MovieId = 1,
                        Name = "Lord of the Rings",
                        Description = "Een hele leuke fantasyfilm",
                        ImageData = null,
                        BannerImage = null,
                        Language = "Engels",
                        Subtitle = "Nederlands",
                        Duration = 90,
                        Type = false,
                        Genre = "Fantasy",
                        StartDate = DateTime.Parse("13-12-2015"),
                        EndDate = DateTime.Parse("2-2-2016"),
                        Pegi =
                            new Pegi() { Horror = true },
                        Director = new Director() { DirectorID = 1, Name = "Steven Spielberg" }
                    },
                    Room =
                        new Room() {
                            RoomId = 1,
                            RoomNumber = 1,
                            Accessibility = true,
                            Rows =
                                new List<RoomRow>() {
                            new RoomRow() {RoomRowID = 1, RowNumber = 1, SeatAmount = 25}},
                            Type = true
                        },
                    AssignedSeats =
                        new List<Seat>(),
                    StartTime = DateTime.Parse("14:00:00")
                }, false)
                {
                    OrderId = 1,
                    PickedUp = false,
                    Paid = false,
                    Tickets =
                        new List<Ticket>()
            {new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
            new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("12,00"), Naam = "Popcorn"}}}
                };

            //Act
            order.GetTotalPrice();

            //Assert
            Assert.IsTrue(order.GetTotalPrice() == Decimal.Parse("20,50"));
        }

        [TestMethod]
        public void GetTotalPrice2TicketsNormalPopcorn2dLongMovie() {
            //Calculate total price: normaal&popcorn 2d - lange film
            //Arrange
            Order order = new Order(
                new Show {
                    ShowID = 1,
                    PopcornArrangement = true,
                    Movie = new Movie {
                        MovieId = 1,
                        Name = "Lord of the Rings",
                        Description = "Een hele leuke fantasyfilm",
                        ImageData = null,
                        BannerImage = null,
                        Language = "Engels",
                        Subtitle = "Nederlands",
                        Duration = 150,
                        Type = false,
                        Genre = "Fantasy",
                        StartDate = DateTime.Parse("13-12-2015"),
                        EndDate = DateTime.Parse("2-2-2016"),
                        Pegi =
                            new Pegi() { Horror = true },
                        Director = new Director() { DirectorID = 1, Name = "Steven Spielberg" }
                    },
                    Room =
                        new Room() {
                            RoomId = 1,
                            RoomNumber = 1,
                            Accessibility = true,
                            Rows =
                                new List<RoomRow>() {
                            new RoomRow() {RoomRowID = 1, RowNumber = 1, SeatAmount = 25}},
                            Type = true
                        },
                    AssignedSeats =
                        new List<Seat>(),
                    StartTime = DateTime.Parse("14:00:00")
                }, false)
                {
                    OrderId = 1,
                    PickedUp = false,
                    Paid = false,
                    Tickets =
                        new List<Ticket>()
            {new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
            new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("12,00"), Naam = "Popcorn"}}}
                };

            //Act
            order.GetTotalPrice();

            //Assert
            Assert.IsTrue(order.GetTotalPrice() == Decimal.Parse("21,50"));
        }

        [TestMethod]
        public void GetTotalPrice2TicketsNormalPopcorn3dShortMovie() {
            //Calculate total price: normaal&popcorn 3d - korte film
            //Arrange
            Order order = new Order(
                new Show {
                    ShowID = 1,
                    PopcornArrangement = true,
                    Movie = new Movie {
                        MovieId = 1,
                        Name = "Lord of the Rings",
                        Description = "Een hele leuke fantasyfilm",
                        ImageData = null,
                        BannerImage = null,
                        Language = "Engels",
                        Subtitle = "Nederlands",
                        Duration = 90,
                        Type = true,
                        Genre = "Fantasy",
                        StartDate = DateTime.Parse("13-12-2015"),
                        EndDate = DateTime.Parse("2-2-2016"),
                        Pegi =
                            new Pegi() { Horror = true },
                        Director = new Director() { DirectorID = 1, Name = "Steven Spielberg" }
                    },
                    Room =
                        new Room() {
                            RoomId = 1,
                            RoomNumber = 1,
                            Accessibility = true,
                            Rows =
                                new List<RoomRow>() {
                            new RoomRow() {RoomRowID = 1, RowNumber = 1, SeatAmount = 25}},
                            Type = true
                        },
                    AssignedSeats =
                        new List<Seat>(),
                    StartTime = DateTime.Parse("14:00:00")
                }, false)
                {
                    OrderId = 1,
                    PickedUp = false,
                    Paid = false,
                    Tickets =
                        new List<Ticket>()
            {new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
            new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("12,00"), Naam = "Popcorn"}}}
                };

            //Act
            order.GetTotalPrice();

            //Assert
            Assert.IsTrue(order.GetTotalPrice() == Decimal.Parse("25,50"));
        }

        [TestMethod]
        public void GetTotalPrice2TicketsNormalPopcorn3dLongMovie() {
            //Calculate total price: normaal&popcorn 3d - lange film
            //Arrange
            Order order = new Order(
                new Show {
                    ShowID = 1,
                    PopcornArrangement = true,
                    Movie = new Movie {
                        MovieId = 1,
                        Name = "Lord of the Rings",
                        Description = "Een hele leuke fantasyfilm",
                        ImageData = null,
                        BannerImage = null,
                        Language = "Engels",
                        Subtitle = "Nederlands",
                        Duration = 150,
                        Type = true,
                        Genre = "Fantasy",
                        StartDate = DateTime.Parse("13-12-2015"),
                        EndDate = DateTime.Parse("2-2-2016"),
                        Pegi =
                            new Pegi() { Horror = true },
                        Director = new Director() { DirectorID = 1, Name = "Steven Spielberg" }
                    },
                    Room =
                        new Room() {
                            RoomId = 1,
                            RoomNumber = 1,
                            Accessibility = true,
                            Rows =
                                new List<RoomRow>() {
                            new RoomRow() {RoomRowID = 1, RowNumber = 1, SeatAmount = 25}},
                            Type = true
                        },
                    AssignedSeats =
                        new List<Seat>(),
                    StartTime = DateTime.Parse("14:00:00")
                }, false)
                {
                    OrderId = 1,
                    PickedUp = false,
                    Paid = false,
                    Tickets =
                        new List<Ticket>()
            {new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
            new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("12,00"), Naam = "Popcorn"}}}
                };

            //Act
            order.GetTotalPrice();

            //Assert
            Assert.IsTrue(order.GetTotalPrice() == Decimal.Parse("26,50"));
        }

        [TestMethod]
        public void GetTotalPrice10Tickets2dShortMovie() {
            //Calculate total price: normaal&korting 2d - korte film
            //Arrange
            Order order = new Order(
                new Show {
                    ShowID = 1,
                    PopcornArrangement = true,
                    Movie = new Movie {
                        MovieId = 1,
                        Name = "Lord of the Rings",
                        Description = "Een hele leuke fantasyfilm",
                        ImageData = null,
                        BannerImage = null,
                        Language = "Engels",
                        Subtitle = "Nederlands",
                        Duration = 90,
                        Type = false,
                        Genre = "Fantasy",
                        StartDate = DateTime.Parse("13-12-2015"),
                        EndDate = DateTime.Parse("2-2-2016"),
                        Pegi =
                            new Pegi() { Horror = true },
                        Director = new Director() { DirectorID = 1, Name = "Steven Spielberg" }
                    },
                    Room =
                        new Room() {
                            RoomId = 1,
                            RoomNumber = 1,
                            Accessibility = true,
                            Rows =
                                new List<RoomRow>() {
                            new RoomRow() {RoomRowID = 1, RowNumber = 1, SeatAmount = 25}},
                            Type = true
                        },
                    AssignedSeats =
                        new List<Seat>(),
                    StartTime = DateTime.Parse("14:00:00")
                }, false)
                {
                    OrderId = 1,
                    PickedUp = false,
                    Paid = false,
                    Tickets =
                        new List<Ticket>()
            {new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
            new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}}}
                };

            //Act
            order.GetTotalPrice();

            //Assert
            Assert.IsTrue(order.GetTotalPrice() == Decimal.Parse("77,50"));
        }

        [TestMethod]
        public void GetTotalPrice10Tickets2dLongMovie() {
            //Calculate total price: normaal&korting 2d - lange film
            //Arrange
            Order order = new Order(
                new Show {
                    ShowID = 1,
                    PopcornArrangement = true,
                    Movie = new Movie {
                        MovieId = 1,
                        Name = "Lord of the Rings",
                        Description = "Een hele leuke fantasyfilm",
                        ImageData = null,
                        BannerImage = null,
                        Language = "Engels",
                        Subtitle = "Nederlands",
                        Duration = 180,
                        Type = false,
                        Genre = "Fantasy",
                        StartDate = DateTime.Parse("13-12-2015"),
                        EndDate = DateTime.Parse("2-2-2016"),
                        Pegi =
                            new Pegi() { Horror = true },
                        Director = new Director() { DirectorID = 1, Name = "Steven Spielberg" }
                    },
                    Room =
                        new Room() {
                            RoomId = 1,
                            RoomNumber = 1,
                            Accessibility = true,
                            Rows =
                                new List<RoomRow>() {
                            new RoomRow() {RoomRowID = 1, RowNumber = 1, SeatAmount = 25}},
                            Type = true
                        },
                    AssignedSeats =
                        new List<Seat>(),
                    StartTime = DateTime.Parse("14:00:00")
                }, false)
                {
                    OrderId = 1,
                    PickedUp = false,
                    Paid = false,
                    Tickets =
                        new List<Ticket>()
            {new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
            new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}}}
                };

            //Act
            order.GetTotalPrice();

            //Assert
            Assert.IsTrue(order.GetTotalPrice() == Decimal.Parse("82,50"));
        }

        [TestMethod]
        public void GetTotalPrice10Tickets3dShortMovie() {
            //Calculate total price: normaal&korting 3d - korte film
            //Arrange
            Order order = new Order(
                new Show {
                    ShowID = 1,
                    PopcornArrangement = true,
                    Movie = new Movie {
                        MovieId = 1,
                        Name = "Lord of the Rings",
                        Description = "Een hele leuke fantasyfilm",
                        ImageData = null,
                        BannerImage = null,
                        Language = "Engels",
                        Subtitle = "Nederlands",
                        Duration = 90,
                        Type = true,
                        Genre = "Fantasy",
                        StartDate = DateTime.Parse("13-12-2015"),
                        EndDate = DateTime.Parse("2-2-2016"),
                        Pegi =
                            new Pegi() { Horror = true },
                        Director = new Director() { DirectorID = 1, Name = "Steven Spielberg" }
                    },
                    Room =
                        new Room() {
                            RoomId = 1,
                            RoomNumber = 1,
                            Accessibility = true,
                            Rows =
                                new List<RoomRow>() {
                            new RoomRow() {RoomRowID = 1, RowNumber = 1, SeatAmount = 25}},
                            Type = true
                        },
                    AssignedSeats =
                        new List<Seat>(),
                    StartTime = DateTime.Parse("14:00:00")
                }, false)
                {
                    OrderId = 1,
                    PickedUp = false,
                    Paid = false,
                    Tickets =
                        new List<Ticket>()
            {new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
            new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}}}
                };

            //Act
            order.GetTotalPrice();

            //Assert
            Assert.IsTrue(order.GetTotalPrice() == Decimal.Parse("102,50"));
        }

        [TestMethod]
        public void GetTotalPrice10Tickets3dLongMovie() {
            //Calculate total price: normaal&korting 3d - lange film
            //Arrange
            Order order = new Order(
                new Show {
                    ShowID = 1,
                    PopcornArrangement = true,
                    Movie = new Movie {
                        MovieId = 1,
                        Name = "Lord of the Rings",
                        Description = "Een hele leuke fantasyfilm",
                        ImageData = null,
                        BannerImage = null,
                        Language = "Engels",
                        Subtitle = "Nederlands",
                        Duration = 180,
                        Type = true,
                        Genre = "Fantasy",
                        StartDate = DateTime.Parse("13-12-2015"),
                        EndDate = DateTime.Parse("2-2-2016"),
                        Pegi =
                            new Pegi() { Horror = true },
                        Director = new Director() { DirectorID = 1, Name = "Steven Spielberg" }
                    },
                    Room =
                        new Room() {
                            RoomId = 1,
                            RoomNumber = 1,
                            Accessibility = true,
                            Rows =
                                new List<RoomRow>() {
                            new RoomRow() {RoomRowID = 1, RowNumber = 1, SeatAmount = 25}},
                            Type = true
                        },
                    AssignedSeats =
                        new List<Seat>(),
                    StartTime = DateTime.Parse("14:00:00")
                }, false)
                {
                    OrderId = 1,
                    PickedUp = false,
                    Paid = false,
                    Tickets =
                        new List<Ticket>()
            {new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
            new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
                new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}}}
                };

            //Act
            order.GetTotalPrice();

            //Assert
            Assert.IsTrue(order.GetTotalPrice() == Decimal.Parse("107,50"));
        }

        [TestMethod]
        public void GetTotalPrice3TicketTypes2dShortMovie() {
            //Calculate total price: normaal-korting-popcorn 2d - korte film
            //Arrange
            Order order = new Order(
                new Show {
                    ShowID = 1,
                    PopcornArrangement = true,
                    Movie = new Movie {
                        MovieId = 1,
                        Name = "Lord of the Rings",
                        Description = "Een hele leuke fantasyfilm",
                        ImageData = null,
                        BannerImage = null,
                        Language = "Engels",
                        Subtitle = "Nederlands",
                        Duration = 90,
                        Type = false,
                        Genre = "Fantasy",
                        StartDate = DateTime.Parse("13-12-2015"),
                        EndDate = DateTime.Parse("2-2-2016"),
                        Pegi =
                            new Pegi() { Horror = true },
                        Director = new Director() { DirectorID = 1, Name = "Steven Spielberg" }
                    },
                    Room =
                        new Room() {
                            RoomId = 1,
                            RoomNumber = 1,
                            Accessibility = true,
                            Rows =
                                new List<RoomRow>() {
                            new RoomRow() {RoomRowID = 1, RowNumber = 1, SeatAmount = 25}},
                            Type = true
                        },
                    AssignedSeats =
                        new List<Seat>(),
                    StartTime = DateTime.Parse("14:00:00")
                }, false)
                {
                    OrderId = 1,
                    PickedUp = false,
                    Paid = false,
                    Tickets =
                        new List<Ticket>()
            {new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
            new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
            new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("12,00"), Naam = "Popcorn"}}}
                };

            //Act
            order.GetTotalPrice();

            //Assert
            Assert.IsTrue(order.GetTotalPrice() == Decimal.Parse("27,50"));
        }

        [TestMethod]
        public void GetTotalPrice3TicketTypes2dLongMovie() {
            //Calculate total price: normaal-korting-popcorn 2d - lange film
            //Arrange
            Order order = new Order(
                new Show {
                    ShowID = 1,
                    PopcornArrangement = true,
                    Movie = new Movie {
                        MovieId = 1,
                        Name = "Lord of the Rings",
                        Description = "Een hele leuke fantasyfilm",
                        ImageData = null,
                        BannerImage = null,
                        Language = "Engels",
                        Subtitle = "Nederlands",
                        Duration = 190,
                        Type = false,
                        Genre = "Fantasy",
                        StartDate = DateTime.Parse("13-12-2015"),
                        EndDate = DateTime.Parse("2-2-2016"),
                        Pegi =
                            new Pegi() { Horror = true },
                        Director = new Director() { DirectorID = 1, Name = "Steven Spielberg" }
                    },
                    Room =
                        new Room() {
                            RoomId = 1,
                            RoomNumber = 1,
                            Accessibility = true,
                            Rows =
                                new List<RoomRow>() {
                            new RoomRow() {RoomRowID = 1, RowNumber = 1, SeatAmount = 25}},
                            Type = true
                        },
                    AssignedSeats =
                        new List<Seat>(),
                    StartTime = DateTime.Parse("14:00:00")
                }, false)
                {
                    OrderId = 1,
                    PickedUp = false,
                    Paid = false,
                    Tickets =
                        new List<Ticket>()
            {new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
            new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
            new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("12,00"), Naam = "Popcorn"}}}
                };

            //Act
            order.GetTotalPrice();

            //Assert
            Assert.IsTrue(order.GetTotalPrice() == Decimal.Parse("29,00"));
        }

        [TestMethod]
        public void GetTotalPrice3TicketTypes3dShortMovie() {
            //Calculate total price: normaal-korting-popcorn 3d - korte film
            //Arrange
            Order order = new Order(
                new Show {
                    ShowID = 1,
                    PopcornArrangement = true,
                    Movie = new Movie {
                        MovieId = 1,
                        Name = "Lord of the Rings",
                        Description = "Een hele leuke fantasyfilm",
                        ImageData = null,
                        BannerImage = null,
                        Language = "Engels",
                        Subtitle = "Nederlands",
                        Duration = 90,
                        Type = true,
                        Genre = "Fantasy",
                        StartDate = DateTime.Parse("13-12-2015"),
                        EndDate = DateTime.Parse("2-2-2016"),
                        Pegi =
                            new Pegi() { Horror = true },
                        Director = new Director() { DirectorID = 1, Name = "Steven Spielberg" }
                    },
                    Room =
                        new Room() {
                            RoomId = 1,
                            RoomNumber = 1,
                            Accessibility = true,
                            Rows =
                                new List<RoomRow>() {
                            new RoomRow() {RoomRowID = 1, RowNumber = 1, SeatAmount = 25}},
                            Type = true
                        },
                    AssignedSeats =
                        new List<Seat>(),
                    StartTime = DateTime.Parse("14:00:00")
                }, false)
                {
                    OrderId = 1,
                    PickedUp = false,
                    Paid = false,
                    Tickets =
                        new List<Ticket>()
            {new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
            new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
            new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("12,00"), Naam = "Popcorn"}}}
                };

            //Act
            order.GetTotalPrice();

            //Assert
            Assert.IsTrue(order.GetTotalPrice() == Decimal.Parse("35,00"));
        }

        [TestMethod]
        public void GetTotalPrice3TicketTypes3dLongMovie() {
            //Calculate total price: normaal-korting-popcorn 3d - lange film
            //Arrange
            Order order = new Order(
                new Show {
                    ShowID = 1,
                    PopcornArrangement = true,
                    Movie = new Movie {
                        MovieId = 1,
                        Name = "Lord of the Rings",
                        Description = "Een hele leuke fantasyfilm",
                        ImageData = null,
                        BannerImage = null,
                        Language = "Engels",
                        Subtitle = "Nederlands",
                        Duration = 190,
                        Type = true,
                        Genre = "Fantasy",
                        StartDate = DateTime.Parse("13-12-2015"),
                        EndDate = DateTime.Parse("2-2-2016"),
                        Pegi =
                            new Pegi() { Horror = true },
                        Director = new Director() { DirectorID = 1, Name = "Steven Spielberg" }
                    },
                    Room =
                        new Room() {
                            RoomId = 1,
                            RoomNumber = 1,
                            Accessibility = true,
                            Rows =
                                new List<RoomRow>() {
                            new RoomRow() {RoomRowID = 1, RowNumber = 1, SeatAmount = 25}},
                            Type = true
                        },
                    AssignedSeats =
                        new List<Seat>(),
                    StartTime = DateTime.Parse("14:00:00")
                }, false)
                {
                    OrderId = 1,
                    PickedUp = false,
                    Paid = false,
                    Tickets =
                        new List<Ticket>()
            {new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("8,50"), Naam = "Standaard"}},
            new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("7,00"), Naam = "Korting"}},
            new Ticket() { Seat = 
                new Seat() { SeatId = 1, Number = 1, Row = 1}, TicketId = 1, TicketSoort = 
                new TicketSoort() {TicketSoortID = 1, Price = Decimal.Parse("12,00"), Naam = "Popcorn"}}}
                };

            //Act
            order.GetTotalPrice();

            //Assert
            Assert.IsTrue(order.GetTotalPrice() == Decimal.Parse("36,50"));
        }
    }
}