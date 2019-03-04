using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMSEntity
{
    public class Employee:Entity
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }
        [Required, MaxLength(50)]
        public String LastName { get; set; }

        public DateTime WorkingSince { get; set; }
        [Required]
        public String Email { get; set; }

        public double Salary { get; set; }
        [Required]
        public String Address { get; set; }
        [Required]
        public string MobileNo { get; set; }
        [Required]
        public DateTime DOB { get; set; }


        public string UserId { get; set; }
        [Required]
        public string UserType { get; set; }
        [Required]
        public string NID { get; set; }
        [Required, MaxLength(50), MinLength(2)]
        public string Password { get; set; }

        public byte[] Picture { get; set; }

    }
}
