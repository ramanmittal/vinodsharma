using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vinodsharma.Models
{
    public class UrerProfileModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [DisplayName("Date Of Birth")]
        public DateTime? DOB { get; set; }
        public string MembershipID { get; set; }
        public string Email { get; set; }
        [Required]
        public string CoDistributerFirstName { get; set; }
        [Required]
        public string CoDistributerLasttName { get; set; }
        [DisplayName("Co Distributer DOB")]
        public DateTime? CoDob { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string PinCode { get; set; }
    }
}