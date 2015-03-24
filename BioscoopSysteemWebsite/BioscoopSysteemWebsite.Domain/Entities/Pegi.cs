﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace BioscoopSysteemWebsite.Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class Pegi
    {
        //Gemaakt door: Frank Molengraaf
        [Key]
        public int PegiId { get; set; }
        public bool All { get; set; }
        public bool SixPlus { get; set; }
        public bool TwelvePlus { get; set; }
        public bool SixteenPlus { get; set; }
        public bool Violence { get; set; }
        public bool Horror { get; set; }
        public bool Sex { get; set; }
        public bool Language { get; set; }
        public bool Drugs { get; set; }
        public bool Racism { get; set; }
    }
}
