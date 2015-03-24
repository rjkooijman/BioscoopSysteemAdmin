using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BioscoopSysteemTouch.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageData { get; set; }
        public string Language { get; set; }
        public string Subtitle { get; set; }
        public int Duration { get; set; }
        public bool Type { get; set;}
        public string Genre { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PegiId { get; set; }
        public virtual Pegi Pegi { get; set; }
    }
}
