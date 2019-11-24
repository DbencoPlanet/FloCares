using FloCares.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FloCares.Models.Dtos
{
    public class AccountantDto
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }


        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Accountant ID")]
        public string AccountantReg { get; set; }

        [Display(Name = "Surname")]
        [Required]
        public string SurName { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Other Name")]
        public string OtherName { get; set; }

        public string Fullname
        {
            get
            {
                return SurName + " " + FirstName + " " + OtherName;
            }
        }

        [Display(Name = " Email Address")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }


        //[DataType(DataType.EmailAddress)]
        //[Display(Name = "Confirm Email Address")]
        //[Compare("EmailAddress", ErrorMessage = "The Email Address and confirmation Email Address do not match.")]
        //public string ConfirmEmailAddress { get; set; }

        //public string Username { get; set; }


        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }

        [Display(Name = "MobileNo")]
        [Required]
        public string MobileNo { get; set; }


        [Display(Name = "Sex")]
        //[UIHint("Enum")]
        [EnumDataType(typeof(Sex))]
        public Sex Sex { get; set; }

        [Display(Name = "Picture")]
        public string ProfilePicture { get; set; }

        [Display(Name = "Address")]
        [Required]
        public string Address { get; set; }

        [Display(Name = "Status")]
        public EntityStatus Status { get; set; }
    }
}
