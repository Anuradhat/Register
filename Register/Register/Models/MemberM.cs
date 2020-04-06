using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Register.Models
{
    public class MemberM
    {
        [DisplayName("ID")]
        public string ID { get; set; }
        [DisplayName("Member ID")]
        public string MemberID { get; set; }
        [DisplayName("NIC / Passport")]
        public string NIC { get; set; }
        [DisplayName("Full Name")]
        public string FullName { get; set; }
        [DisplayName("Name With Initials")]
        public string NameWithInitials { get; set; }
        [DisplayName("Calling Name")]
        public string CallingName { get; set; }

        [DisplayName("Date Of Birth")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("Marital Status")]
        public string MaritalStatusCode { get; set; }
        [DisplayName("Gender")]
        public string GenderCode { get; set; }
        [DisplayName("Religion")]
        public string ReligionCode { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("City")]
        public string City { get; set; }
        [DisplayName("Country")]
        public string Country { get; set; }
        [DisplayName("Mobile No")]
        public int MobileNo { get; set; }
        [DisplayName("Home Tel")]
        public int HomeTel { get; set; }
        [DisplayName("Email")]
        public string Email { get; set; }
        [DisplayName("GCE O/L")]
        public bool GCEOL { get; set; }
        [DisplayName("GCE A/L")]
        public bool GCEAL { get; set; }
        [DisplayName("Other Qualification")]
        public string OtherQualification { get; set; }
        public DateTime TrnDate { get; set; }
        public string TrnUser { get; set; }
        [DisplayName("Active Member")]
        public bool ActiveMember { get; set; }
        [DisplayName("Comments")]
        public string Comments { get; set; }
    }
}