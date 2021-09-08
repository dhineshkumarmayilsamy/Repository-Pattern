using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Model.DomainModel;
using Service.DI;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Middleware;

namespace WebAPI
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

            //CORS
            var origins = new List<string>();
            Configuration.GetSection("Cors:Domains").Bind(origins);

            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder.WithOrigins(origins.ToArray())
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            }));

            // Dependencies


            //Automapper
            services.AddAutoMapper();

            // Db Context
            services.AddDbContext<AppDbContext>(options =>
            {
                // TODO: Configure options.UseMySql() or options.UseSqlServer()
            });

            services.AddControllers();

            //Swagger 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
            }
            else
            {
                // Production
                app.UseStatusCodePages(context =>
                {
                    if (context.HttpContext.Response.StatusCode == 404)
                    {
                        context.HttpContext.Response.Redirect("/");
                    }

                    return Task.CompletedTask;
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //Logger
            app.UseLoggerMiddleware();

            // this will serve wwwroot/index.html when path is '/'
            app.UseDefaultFiles();
            // this will serve js, css, images etc.
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
