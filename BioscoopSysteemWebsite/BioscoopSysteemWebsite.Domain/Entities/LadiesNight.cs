using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BioscoopSysteemWebsite.Domain.Entities {
    public class LadiesNight {
        [Key]
        public int LadiesNightid { get; set; }
        public DateTime LadiesNightDay { get; set; }
    }
}
