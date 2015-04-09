using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioscoopSysteemWebsite.Domain.Entities {
    public class Subscriber {
        [Key]
        public int SubscriberId { get; set; }

        [Required(ErrorMessage = "Vul een voornaam in")]
        public string Voornaam { get; set; }
        [Required(ErrorMessage = "Vul een achternaam in")]
        public string Achternaam { get; set; }
        [Required(ErrorMessage = "Vul een straat in")]
        public string Straat { get; set; }
        [Required(ErrorMessage = "Vul een huisnummer in")]
        public int Huisnummer { get; set; }
        [Required(ErrorMessage = "Vul een postcode in")]
        public string Postcode { get; set; }
        [Required(ErrorMessage = "Vul een woonplaats in")]
        public string Woonplaats { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

    }
}
