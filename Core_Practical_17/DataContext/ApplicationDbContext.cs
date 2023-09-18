using Core_Practical_17.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;

namespace Core_Practical_17.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
          
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<User>().Ignore(i => i.ConfirmPassword);
            
            modelBuilder.Entity<UserRole>().HasKey(i => new { i.UserId,i.RoleId });

            modelBuilder.Entity<UserRole>()
          .HasOne(x => x.User)
          .WithMany(x => x.Roles)
          .HasForeignKey(x => x.UserId);



            modelBuilder.Entity<UserRole>()
                .HasOne(x => x.Role)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.RoleId);

            modelBuilder.Entity<User>().HasData(new User() { Id=1,FirstName = "Jil",LastName = "Patel",Email="jil@gmail.com",Phone="9999999999",Password="123123"},
                new User() {Id = 2, FirstName = "Janvi", LastName = "Patel", Email = "janvi@gmail.com", Phone = "9999999999", Password = "123123" });
           
            
            modelBuilder.Entity<Role>().HasData(new Role() {Id=1, RoleName="Admin"},new Role() {Id=2, RoleName="Member"});


            modelBuilder.Entity<Student>().HasData(new Student() { Id = 1, Name = "Bhavin", Email = "bhavin@gmail.com", Age = 21, DOB = Convert.ToDateTime("17/04/2001").Date });
           
            modelBuilder.Entity<UserRole>().HasData(new UserRole() { RoleId =1,UserId=1}, new UserRole() { RoleId = 2, UserId = 2 });
            

            
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
    }
}
