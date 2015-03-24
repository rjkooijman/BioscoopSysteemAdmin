using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace BioscoopSysteemWebsite.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Movie
    {
        //Gemaakt door: Frank Molengraaf
        [Key]
        public int MovieId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string EnglishDescription { get; set; }
        public string ImageData { get; set; }
        public string BannerImage { get; set; }
        public string Language { get; set; }
        public string Subtitle { get; set; }
        public int Duration { get; set; }
        public bool Type { get; set; }
        public string Genre { get; set; }
        public string TrailerUrl { get; set; }
        public string IMDBUrl { get; set; }
        public string FilmWebsite { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public virtual Pegi Pegi { get; set; }
        public int PegiId { get; set; }
        public virtual List<Actor> Actor { get; set; }
        public virtual Director Director { get; set; }
        public int DirectorID { get; set; }
    }
}
