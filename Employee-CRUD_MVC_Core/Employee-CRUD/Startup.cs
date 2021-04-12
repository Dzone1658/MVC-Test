using Employee_CRUD.Bll;
using Employee_CRUD.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Employee_CRUD
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews( );
            //services.AddDbContext<DataContext>(options =>
            //{
            //    options.UseNpgsql(Configuration.GetConnectionString("NpgsqlConnection"));
            //});
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("GlobalMssqlConnection"));
            });

            //services.AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<QuotesBll>>());

            //Dependency resolver
            services.AddSession();
            services.AddTransient<IQuotesBll, QuotesBll>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if ( env.IsDevelopment( ) )
            {
                app.UseDeveloperExceptionPage( );
            }
            else
            {
                app.UseExceptionHandler("/Employee/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts( );
            }
            app.UseHttpsRedirection( );
            app.UseStaticFiles( );

            app.UseRouting( );

            app.UseAuthorization( );
            app.UseSession();
            app.UseEndpoints( endpoints =>
             {
                 endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller=Account}/{action=Login}/{id?}");
             } );
        }
    }
}
