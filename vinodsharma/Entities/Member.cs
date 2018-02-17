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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MemberID { get; set; }
        public string Address { get; set; }
        public DateTime? CoDistributerDOB { get; set; }
        public string CoDistributerFirstName { get; set; }
        public string CoDistributerLastName { get; set; }
        public string CoDistributerRelation { get; set; }
        public DateTime? Dob { get; set; }
        public string DistributerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Points { get; set; }
        public int? MaxValue { get; set; }
        public bool? IsAlways { get; set; }
        public int? UplineId { get; set; }
        public decimal? AmountGiven { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Member Upliner { get; set; }
        public string UserId { get; set; }
        public bool HasDeleted { get; set; }
        public virtual ICollection<Member> Members { get; set; }
        public bool? IsActive { get; internal set; }
        public bool? HasCollected { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public virtual ICollection<MemeberAmountHistory> MemeberAmountHistory { get; set; }
    }
}