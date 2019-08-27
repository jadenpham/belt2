using Microsoft.EntityFrameworkCore;

namespace retake_two.Models
{
    public class MyContext: DbContext
    {
        public MyContext(DbContextOptions options) : base(options) { }

        public DbSet<UserReg> Users {get; set;}

        public DbSet<Idea> Ideas {get; set;}

        public DbSet<Like> Likes {get; set;}
    }
}