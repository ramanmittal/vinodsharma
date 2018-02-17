using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vinodsharma.Models
{
    public class EditUserModel
    {
        public int MemeberID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int MaxValue { get; set; }
        public bool IsActive { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}