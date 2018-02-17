using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace vinodsharma.Models
{
    public class AddMoneyModel
    {
        [Required]
        public int MemberID { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}