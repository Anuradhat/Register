using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Register.Models
{
    public class emsUserRoleTUview
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Role ID")]
        public string RoleID { get; set; }

        [Required]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string Username { get; set; }

        public DateTime TrnDate { get; set; }
        public string TrnUser { get; set; }
    }
}