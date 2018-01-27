using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using vinodsharma.Models;

namespace vinodsharma.Entities
{
    public class Member
    {
        [Key]
        [ForeignKey("User")]
        public string MemberID { get; set; }
        public string Address { get; set; }
        public DateTime? CoDistributerDOB { get; set; }
        public string CoDistributerName { get; set; }
        public string CoDistributerRelation { get; set; }
        public DateTime? Dob { get; set; }
        public string DistributerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Points { get; set; }
        public string UplineId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Member Upliner { get; set; }
        public virtual ICollection<Member> Members { get; set; }
    }
}