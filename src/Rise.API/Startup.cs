using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rise.Data.Repository.User;
using Rise.Service.User;
using Rise.Utility.Middlewares;
using Swashbuckle.AspNetCore.Swagger;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Rise.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Register connection obj
            var dbConnectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddScoped<IDbConnection>((sp) => new SqlConnection(dbConnectionString));

            // Register specific repositories
            services.AddScoped<IUserRepository, UserRepository>();

            // Register application services
            services.AddScoped<IUserService, UserService>();

            // Register swagger service
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Rise API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseMiddleware(typeof(RiseExceptionMiddleware));

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rise API");
            });

            app.Run(async (context) => await Task.Run(() => context.Response.Redirect("/swagger")));
        }
    }
}
