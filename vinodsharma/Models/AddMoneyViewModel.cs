using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace vinodsharma.Models
{
    public class AddMoneyViewModel
    {
        public int MemberID { get; set; }
        public string MemberShipID { get; set; }
        public int Points { get; set; }
        //[Required]
        //public DateTime Date { get; set; }
       
        public decimal AmountGiven { get; set; }
        [Required]
        [Range(1, Int32.MaxValue,ErrorMessage = "The field Amount must be greater than 1.")]
        public decimal Amount { get; set; }

    }
}