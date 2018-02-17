using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace vinodsharma.Entities
{
    public class MemeberAmountHistory
    {
        public int MemeberAmountHistoryID { get; set; }
        public int MemeberID { get; set; }
        public decimal AmountGiven { get; set; }
        public DateTime Date { get; set; }
        public string Remarks { get; set; }
        public virtual Member Member { get; set; }
    }
}