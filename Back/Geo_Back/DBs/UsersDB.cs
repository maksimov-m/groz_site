using Geo_Back.Models;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Geo_Back.DBs
{
    public class UsersDB : DbContext
    {

        public DbSet<UserModel> Users { get; set; }

        public DbSet<TeacherModel> Teachers { get; set; }

        public DbSet<StudentModel> Students { get; set; }

        public UsersDB(DbContextOptions<UsersDB> options):
            base(options)
        {
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserModel>().HasAlternateKey(x => x.Login);
        }
    }
}
