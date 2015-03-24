using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BioscoopSysteemWebsite.Domain.Entities {
    public class User {
        [Key]
        [Required(ErrorMessage = "Gelieve uw inlog code in te vullen")]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required(ErrorMessage = "Gelieve uw wachtwoord in te vullen")]
        public string Password { get; set; }
        public virtual Roles Role { get; set; }
        public int RoleID { get; set; }

    }
}
