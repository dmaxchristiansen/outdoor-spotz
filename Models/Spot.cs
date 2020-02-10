using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    [Table("spots")]
    public class Spot
    {
        [Key]
        public int SpotId {get;set;}

        [Display(Name="Spot Name")]
        public string Name {get;set;}

        public string County {get;set;}

        public string State {get;set;}

        [Display(Name="How to find the Spot...")]
        public string Directions {get;set;}

        [Display(Name="Tell us why this Spot is awesome...")]
        public string Description {get;set;}

        public string Image {get;set;}

        public int CreatorId {get;set;}

        public User Creator {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;

        public DateTime UpdatedAt {get;set;} = DateTime.Now;

        public List<Comment> Comments {get;set;}
    }
}