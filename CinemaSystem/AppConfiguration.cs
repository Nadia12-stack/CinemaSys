using CinemaSystem.Repositories.IRepositories;
using CinemaSystem.Utitlies;
using CinemaSystem.Utitlies.DBInitilizer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

namespace CinemaSystem
{
    public static class AppConfiguration
    {
        public static void RegisterConfig(this IServiceCollection services, string connection)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                //option.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                //option.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
                option.UseSqlServer(connection);
            });

            services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                option.User.RequireUniqueEmail = true;
                option.Password.RequiredLength = 8;
                option.Password.RequireNonAlphanumeric = false;
                option.SignIn.RequireConfirmedEmail = true;
            })
                //.AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Identity/Account/Login"; // Default login path
                options.AccessDeniedPath = "/Identity/Account/AccessDenied"; // Default access denied path
            });

            services.AddTransient<IEmailSender, EmailSender>();

            //services.AddScoped<IRepository<Category>, Repository<Category>>();
            //services.AddScoped<IRepository<MovieSubImage>, Repository<MovieSubImage>>();
            //services.AddScoped<IRepository<Actor>, Repository<Actor>>();
            //services.AddScoped<IRepository<Promotion>, Repository<Promotion>>();
            //services.AddScoped<IRepository<ApplicationUserOTP>, Repository<ApplicationUserOTP>>();
            //services.AddScoped<IMovieRepository, MovieRepository>();

            services.AddScoped<IDBInitializer, DBInitializer>();
        }
    }
}
