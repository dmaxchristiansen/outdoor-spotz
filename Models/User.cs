using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    [Table("users")]
    public class User
    {
        
        [Key]
        public int UserId {get;set;}


        [Required]
        [Display(Name="First Name")]
        public string FirstName {get;set;}


        [Required]
        [Display(Name="Last Name")]
        public string LastName {get;set;}


        [Required]
        [EmailAddress]
        public string Email {get;set;}


        [Required]
        [DataType(DataType.Password)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "Passwords must be at least 8 characters and contain at 3 of 4 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        [MinLength(8, ErrorMessage="Password must be at least 8 characters")]
        public string Password {get;set;}


        [NotMapped]
        [Display(Name="Confirm Password")]
        [Compare("Password")]
        [DataType(DataType.Password)]
        public string Confirm {get;set;}


        public DateTime CreatedAt {get;set;} = DateTime.Now;


        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        
    }
}