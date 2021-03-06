﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orlen.API.Authorization;
using Orlen.API.Filters;
using Orlen.Core;
using Orlen.Services.BusService;
using Orlen.Services.IssueTypeService;
using Orlen.Services.PointService;
using Orlen.Services.RouteService;
using Orlen.Services.SectionService;
using Orlen.Services.UserService;
using Swashbuckle.AspNetCore.Swagger;

namespace Orlen.API
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
            services.AddMvc(options =>
            {
                options.MaxModelValidationErrors = 50;
                options.Filters.Add(typeof(ValidateModelStateFilter));
            });

            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connection));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ISectionService, SectionService>();
            services.AddScoped<IPointService, PointService>();
            services.AddScoped<IIssueTypeService, IssueTypeService>();
            services.AddScoped<IRouteService, RouteService>();
            services.AddScoped<IBusService, BusService>();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddOptions();

            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info { Title = "Orlen Transportation Managment API", Version = "v1" }); });

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = TokenHelper.GetJwtBearerOptions();
                options.RequireHttpsMetadata = false;
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, DataContext dataContext)
        {
            DbInitializer.Migrate(dataContext);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Orlen Transportation Managment API"); });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
