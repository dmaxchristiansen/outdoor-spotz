using Microsoft.EntityFrameworkCore;

namespace Project.Models
{
    public class MyContext : DbContext
    {
        public MyContext (DbContextOptions options) : base(options) { }

        public DbSet<User> Users {get;set;}

        public DbSet<Spot> Spots {get;set;}

        public DbSet<Comment> Comments {get;set;}
       
    }
}