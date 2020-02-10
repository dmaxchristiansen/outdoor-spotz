using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    [Table("comments")]
    public class Comment
    {
        [Key]
        public int CommentId {get;set;}

        public string comment {get;set;}

        public int UserId {get;set;}

        public User User {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;

        public int SpotId {get;set;}
    }
}