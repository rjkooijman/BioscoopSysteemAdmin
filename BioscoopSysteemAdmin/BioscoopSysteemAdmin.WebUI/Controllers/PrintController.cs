using BioscoopSysteemWebsite.Domain.Entities;
using BioscoopSysteemWebsite.Domain.Interfaces;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Web.Mvc;

//Gemaakt door: Ricardo Jobse

namespace BioscoopSysteemAdmin.WebUI.Controllers {
    public class PrintController : Controller {
        private IRepository repository;



        public PrintController(IRepository repo) {
            repository = repo;
        }
        public ViewResult CreateObject(Order order) {
            //loop through the tickets inside the order
            foreach (Ticket t in order.Tickets) {
                //Give filename
                string fileName = @"C:\AvansCinemaTickets\ticket" + "_" + order.Show.Movie.MovieId + "_" + t.TicketId + ".pdf";
                Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                using (FileStream fs = new FileStream(fileName, FileMode.Create)) {

                    //Create ITextSharp document
                    var doc = new Document(PageSize.A4, 36, 72, 108, 180);

                    //Create MemoryStream
                    var output = new MemoryStream();
                    //Create Filestream object

                    //Create pdf filewriter to write the object to a pdf file
                    var writer = PdfWriter.GetInstance(doc, fs);

                    //Open the documentDocument
                    doc.Open();

                    //Setting fonts
                    var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
                    var subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
                    var boldTableFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
                    var endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
                    var bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);

                    //Add a paragraph to the document
                    doc.Add(new Paragraph("Avans Cinema Ticket", titleFont));

                    //Create table
                    var orderInfoTable = new PdfPTable(2);
                    orderInfoTable.HorizontalAlignment = 0;
                    orderInfoTable.SpacingBefore = 10;
                    orderInfoTable.SpacingAfter = 10;
                    orderInfoTable.DefaultCell.Border = 0;
                    orderInfoTable.SetWidths(new int[] { 1, 4 });

                    orderInfoTable.AddCell(new Phrase("Film:", subTitleFont));
                    orderInfoTable.AddCell(order.Show.Movie.Name);
                    orderInfoTable.AddCell(new Phrase("Zaal:", boldTableFont));
                    orderInfoTable.AddCell(order.Show.Room.RoomNumber.ToString());
                    orderInfoTable.AddCell(new Phrase("Rij nummer:", boldTableFont));
                    orderInfoTable.AddCell(t.Seat.Row.ToString());
                    orderInfoTable.AddCell(new Phrase("Stoel nummer:", boldTableFont));
                    orderInfoTable.AddCell(t.Seat.Number.ToString());
                    orderInfoTable.AddCell(new Phrase("Datum:", boldTableFont));
                    orderInfoTable.AddCell(order.Show.StartTime.Day
                         + "-" + order.Show.StartTime.Month + "-" + order.Show.StartTime.Year);
                    orderInfoTable.AddCell(new Phrase("Tijd:", boldTableFont));
                    orderInfoTable.AddCell(order.Show.StartTime.TimeOfDay.ToString());
                    orderInfoTable.AddCell(new Phrase("U bent geholpen door:", boldTableFont));
                    orderInfoTable.AddCell(order.User.FirstName + " " + order.User.LastName);



                    // Setting paragraph's text alignment using iTextSharp.text.Element class
                    orderInfoTable.HorizontalAlignment = Element.ALIGN_MIDDLE;

                    // Adding Table to the Document object
                    doc.Add(orderInfoTable);

                    //Close the document
                    doc.Close();

                    //Close the filestream and release the output
                    fs.Close();
                }
            }


            return View("Succes");
        }

        public ViewResult SubscriberPrint(Subscriber subscriber) {
            string filename = @"C:\AvansCinemaTickets\abonnementen" + "_" + subscriber.SubscriberId + "_" + subscriber.Postcode + subscriber.Huisnummer + ".pdf";
            Directory.CreateDirectory(Path.GetDirectoryName(filename));
            using (FileStream fs = new FileStream(filename, FileMode.Create)) {

                //Create ITextSharp document
                var doc = new Document(PageSize.A4, 36, 72, 108, 180);

                //Create MemoryStream
                var output = new MemoryStream();
                //Create Filestream object

                //Create pdf filewriter to write the object to a pdf file
                var writer = PdfWriter.GetInstance(doc, fs);

                //Open the documentDocument
                doc.Open();

                //Setting fonts
                var titleFont = FontFactory.GetFont("Arial", 18, Font.BOLD);
                var subTitleFont = FontFactory.GetFont("Arial", 14, Font.BOLD);
                var boldTableFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
                var endingMessageFont = FontFactory.GetFont("Arial", 10, Font.ITALIC);
                var bodyFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);

                //Add a paragraph to the document
                doc.Add(new Paragraph("Avans Cinema Ticket", titleFont));

                //Create table
                var orderInfoTable = new PdfPTable(2);
                orderInfoTable.HorizontalAlignment = 0;
                orderInfoTable.SpacingBefore = 10;
                orderInfoTable.SpacingAfter = 10;
                orderInfoTable.DefaultCell.Border = 0;
                orderInfoTable.SetWidths(new int[] { 1, 2 });


                orderInfoTable.AddCell(new Phrase("Naam", subTitleFont));
                orderInfoTable.AddCell(subscriber.Voornaam + " " + subscriber.Achternaam);
                orderInfoTable.AddCell(new Phrase("Adres:", boldTableFont));
                orderInfoTable.AddCell(subscriber.Straat + " " + subscriber.Huisnummer.ToString());
                orderInfoTable.AddCell(new Phrase("Postcode + woonplaats", boldTableFont));
                orderInfoTable.AddCell(subscriber.Postcode + "  " + subscriber.Woonplaats);
                orderInfoTable.AddCell(new Phrase("Stoel nummer:", boldTableFont));

                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(subscriber.ImageData);
                if (image.Height > image.Width) {
                    float percentage = 0.0f;
                    percentage = 300 / image.Height;
                    image.ScalePercent(percentage * 100);
                } else {
                    float percentage = 0.0f;
                    percentage = 120 / image.Width;
                    image.ScalePercent(percentage * 100);
                }

                image.Border = iTextSharp.text.Rectangle.BOX;
                image.BorderColor = iTextSharp.text.BaseColor.BLACK;
                image.BorderWidth = 3f;

                // Setting paragraph's text alignment using iTextSharp.text.Element class
                orderInfoTable.HorizontalAlignment = Element.ALIGN_MIDDLE;

                // Adding Table to the Document object
                doc.Add(image);
                doc.Add(orderInfoTable);


                //Close the document
                doc.Close();

                //Close the filestream and release the output
                fs.Close();
            }

            return View();
        }
    }
}

