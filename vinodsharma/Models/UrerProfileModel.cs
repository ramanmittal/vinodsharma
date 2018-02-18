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
        public int MemberID { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        
        [DisplayName("Date Of Birth")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? DOB { get; set; }
        public string MembershipID { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        
        public string CoDistributerFirstName { get; set; }
        
        public string CoDistributerLasttName { get; set; }
        [DisplayName("Co Distributer DOB")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? CoDob { get; set; }
        
        public string Phone { get; set; }
        
        public string Address { get; set; }
        
        public string City { get; set; }
        
        public string State { get; set; }
        
        public string PinCode { get; set; }
    }
}