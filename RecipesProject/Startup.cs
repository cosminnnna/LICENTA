/*//RecipesProject

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecipesProject.Data;
using RecipesProject.Services; //Asigurați-vă că importați namespace-ul pentru serviciul de trimitere a e-mailului

namespace RecipesProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Aici se configurează serviciile utilizate de aplicație
        public void ConfigureServices(IServiceCollection services)
        {
            // Adăugați serviciul de trimitere a e-mailului
            services.AddTransient<IEmailSender, EmailSender>();

            // Configurați Identity Framework pentru a solicita confirmările prin e-mail
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            // Alte configurări pentru servicii
            services.AddControllersWithViews(); // sau orice alte servicii de care aveți nevoie în aplicația dvs.
        }

        // Aici se configurează pipeline-ul de cereri HTTP
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Middleware pentru gestionarea erorilor și alte configurări
        }
    }
}
*/