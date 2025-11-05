
using CinemaSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace CinemaSystem.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Entity> entity { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieCinema> MovieCinema { get; set; }
        public DbSet<SocialLink> SocialLinks { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieSubImage> MovieSubImages { get; set; }
        public DbSet<ApplicationUser> applicationUsers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                    : base(options)
        {
        }

        public ApplicationDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=CinemaSystem;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MovieEntityTypeConfiguration).Assembly);

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<MovieCinema>().ToTable("MovieCinema");
            modelBuilder.Entity<MovieActor>().ToTable("MovieActor");
            modelBuilder.Entity<SocialLink>().ToTable("SocialLink");

            base.OnModelCreating(modelBuilder);
        }

        internal void ToDictionary(Func<object, object> value)
        {
            throw new NotImplementedException();
        }
    }
}

