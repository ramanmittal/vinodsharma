using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vinodsharma.Models
{
    public class CreateMemberViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string InlinerID { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int MaximumAmount { get; set; }
    }
}