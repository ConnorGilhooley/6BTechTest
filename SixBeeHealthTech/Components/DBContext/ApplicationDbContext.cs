using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SixBeeHealthTech.Components.Models;
namespace SixBeeHealthTech.Components.DBContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public ApplicationDbContext()
        {
        }


        public DbSet<Appointment> Appointments { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial user
            var userId = "1";
            var roleId = "1";

            var hasher = new PasswordHasher<ApplicationUser>();

            var user = new ApplicationUser
            {
                Id = userId,
                UserName = "Joe Bloggs",
                NormalizedUserName = "JOE BLOGGS",
                Email = "joebloggs@example.com",
                NormalizedEmail = "JOEBLOGGS@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "HelloWorld"),
                SecurityStamp = string.Empty
            };

            var role = new IdentityRole
            {
                Id = roleId,
                Name = "Admin",
                NormalizedName = "ADMIN"
            };

            modelBuilder.Entity<ApplicationUser>().HasData(user);
            modelBuilder.Entity<IdentityRole>().HasData(role);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = userId,
                RoleId = roleId
            });

            modelBuilder.Entity<Appointment>().HasData(
                new Appointment
                {
                    Id = 1,
                    Name = "John Doe",
                    AppointmentDate = DateTime.Today.AddDays(2),
                    AppointmentTime = new TimeOnly(23, 0),
                    ContactNumber = "123-456-7890",
                    EmailAddress = "johndoe@example.com",
                    IsApproved = false
                },
                new Appointment
                {
                    Id = 2,
                    Name = "Jane Smith",
                    AppointmentDate = DateTime.Today.AddDays(3),
                    AppointmentTime = new TimeOnly(11, 0),
                    ContactNumber = "987-654-3210",
                    EmailAddress = "janesmith@example.com",
                    IsApproved = false
                });
        }
    }
}
