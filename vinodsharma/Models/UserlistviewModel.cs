using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vinodsharma.Models
{
    public class UserlistviewModel
    {
        public int MemberID { get; set; }
        public string FullName { get; set; }
        public string MembershipID { get; set; }
        public string InlinerName { get; set; }
        public string IntlinerID { get; set; }
        public int Points { get; set; }
        public bool IsActive { get; set; }
        public bool IsAlways { get; internal set; }
    }
}