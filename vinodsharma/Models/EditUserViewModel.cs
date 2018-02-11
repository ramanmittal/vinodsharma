using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vinodsharma.Models
{
    public class EditUserViewModel
    {
        public int MemeberID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int MaxValue { get; set; }
        public bool IsActive { get; set; }
        public string MembershipID { get; set; }
        public string Email { get; set; }
    }
}