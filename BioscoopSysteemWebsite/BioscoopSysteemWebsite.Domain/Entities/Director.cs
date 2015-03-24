using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BioscoopSysteemWebsite.Domain.Entities {
    [ExcludeFromCodeCoverage]
    public class Director {

        //Gemaakt door: Frank Molengraaf
        [Key]
        public int DirectorID { get; set; }

        public string Name { get; set; }
        public virtual List<Movie> Movies { get; set; }
    }
}
