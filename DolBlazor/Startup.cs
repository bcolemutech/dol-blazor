using dol_sdk.Controllers;
using dol_sdk.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Firebase.Auth;
using DolBlazor.Utilities;

namespace DolBlazor
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            IFirebaseAuthProvider auth = new FirebaseAuthProvider(new FirebaseConfig(Configuration["FirebaseApiKey"]));
            
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddSingleton(auth)
                .AddSingleton<ISecurityService, SecurityService>()
                .AddSingleton<ICharacterController, CharacterController>()
                .AddTransient<IAdminController, AdminController>()
                .AddTransient<IAreaController,AreaController>()
                .AddTransient<IAreaAnalyzer,AreaAnalyzer>()
                .AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
