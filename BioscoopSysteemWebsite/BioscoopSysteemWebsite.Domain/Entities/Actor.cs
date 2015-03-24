using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace BioscoopSysteemWebsite.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Actor
    {
        //Gemaakt door: Frank Molengraaf
        [Key]
        public int ActorId { get; set; }
        public string ActorName { get; set; }
        public virtual List<Movie> Movie { get; set; }
    }
}
