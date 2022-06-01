using ImageServer.Data;
using ImageServer.Services.ImageService;
using ImageServer.Services.NoteService;
using ImageServer.Services.UserService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageServer
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options=> 
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                              policy =>
                              {
                                  policy.WithOrigins("/*http://localhost:3000/*/")
                                  .AllowAnyHeader()
                                  .AllowAnyMethod().AllowAnyOrigin();
                              });
            });
            services.AddControllers();
            services.AddDbContext<DataContext>(options=>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            }
            );
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ImageServer", Version = "v1" });
            });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IImageService, ImageService>();
            services.AddScoped<INoteService, NoteService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ImageServer v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
