using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vinodsharma.Entities
{
    public class UserActivationHistory
    {
        public int HistoryID { get; set; }
        public int MemberID { get; set; }
        public DateTime ChangeTime { get; set; }
        public bool OldActivationStatus { get; set; }
        public bool NewActivationStatus { get; set; }
    }
}