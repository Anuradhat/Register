using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Register.Models
{
    public class ViewModalUserDetails
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "EPF")]
        public string Epf { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [NotMapped]
        [Compare("Password", ErrorMessage = "Password doesn't match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Name with initials")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Delivery Address")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Home Tel No")]
        public string Tel { get; set; }

        [Display(Name = "Transaction Date")]
        public DateTime TrnDate { get; set; }

        [Display(Name = "Transaction User")]
        public string TrnUser { get; set; }

        [Display(Name = "Activation")]
        public bool Active { get; set; }

        [Required]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Name Registered with bank")]
        public string NameInUse { get; set; }

        [Required]
        [Display(Name = "Postel Address")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Town/City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "ID Type")]
        public string IDTypeCode { get; set; }

        [Required]
        [Display(Name = "Valid ID No")]
        public string NIC { get; set; }

        [DataType(DataType.Date), Required]
        [Display(Name = "Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "Mobile")]
        public string MobileNo { get; set; }

        [Required]
        [Display(Name = "Bank Account No")]
        public string BankAccountNo { get; set; }

        [Required]
        [Display(Name = "Bank Code")]
        public string BankCode { get; set; }

        [Required]
        [Display(Name = "Bank Branch Code")]
        public string BankBranchCode { get; set; }

        [Required]
        [Display(Name = "Benificiary Name")]
        public string Benificiary { get; set; }

        [Required]
        [Display(Name = "Relationship Of Benificiary")]
        public string RelationshipBenificiary { get; set; }

        [Required]
        [Display(Name = "Mother Maiden/Family Name")]
        public string MotherMaidenName { get; set; }

        [Required]
        [Display(Name = "Agreed Policies & Procedurs")]
        public bool AgreePolicees { get; set; }

        [Required]
        [Display(Name = "Email Confirmation")]
        public string EmailConfirmation { get; set; }

        [Required]
        [Display(Name = "Referrer")]
        public string ReferralID { get; set; }

        [Required]
        [Display(Name = "Placement Sales excitative ID")]
        public string PlacementSEID { get; set; }

        [Required]
        [Display(Name = "Placement")]
        public string Placement { get; set; }

        [Required]
        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [Required]
        [Display(Name = "Bank Branch")]
        public string BranchName { get; set; }
    }
}