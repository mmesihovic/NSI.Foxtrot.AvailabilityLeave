using System;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using NSI.Common.Helpers;
using Microsoft.AspNetCore.Http;
using NSI.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using NSI.DAL.Repository;
using NSI.BLL.Services.Interfaces;
using NSI.BLL.Services;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;


namespace NSI
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Method used by during runtime to add services to the container
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(c => {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin());
            });
            
            services.AddControllers();
            String Server = Configuration.GetSection("DBConnection:Host").Value;
            String Port = Configuration.GetSection("DBConnection:Port").Value;
            String Database = Configuration.GetSection("DBConnection:Database").Value;
            String Username = Configuration.GetSection("DBConnection:Username").Value;
            String Password = Configuration.GetSection("DBConnection:Password").Value;
            
            String dbConnection = "Server=" + Server + ";Port=" + Port + ";Database=" + Database +";User name=" + Username + ";Password=" + Password;
            services.AddEntityFrameworkNpgsql().AddDbContext<DAL.AvailabilityLeaveContext>(opt => opt.UseNpgsql(dbConnection, b => b.MigrationsAssembly("NSI.DAL")));

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
            //         .AddAzureAD(options => Configuration.Bind("AzureAd", options));
            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options =>
                Configuration.Bind("AzureAd", options))
                .AddOpenIdConnect(options =>
                {
                    options.ClientId = "09599846-67c3-4f17-b1c2-ed350068ba1e";
                    options.Authority = "https://login.microsoftonline.com/" + "996f7811-bbbf-44c3-9b9e-d8e4d3322386";
                });
            // Configuring Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Absence/Availability/Leave Module API Documentation",
                    Version = "v1",
                    Description = "Documentation for available endpoints for Absence/Availability/Leave Modules on NSI2019 Project",
                    Contact = new OpenApiContact
                    {
                        Name = "NSI 2019 - Team Foxtrot",
                        Email = string.Empty,
                        Url = new Uri("http://etf.unsa.ba")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "NSI2019 Licence"
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var NSIXmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                var DataContractsXmlPath = Path.Combine(AppContext.BaseDirectory, "NSI.DataContracts.xml");
                
                c.IncludeXmlComments(NSIXmlPath);
                c.IncludeXmlComments(DataContractsXmlPath);

            });
            // services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
            // {
            //     options.Authority = options.Authority + "/v2.0/";         // Microsoft identity platform

            //     options.TokenValidationParameters.ValidateIssuer = false; // accept several tenants (here simplified)
            // });
            // TODO: Export this section to another file or automate the map
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfile()); });
                

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc();
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IEventService, EventService>();
            
            services.AddScoped<IEventTypeRepository, EventTypeRepository>();
            services.AddScoped<IEventTypeService, EventTypeService>();

            services.AddScoped<IScheduleRepository, ScheduleRepository>();
            services.AddScoped<IScheduleService, ScheduleService>();

            services.AddScoped<ILeaveBalanceRepository, LeaveBalanceRepository>();
            services.AddScoped<ILeaveTransactionRepository, LeaveTransactionRepository>();
            services.AddScoped<ILeaveService, LeaveService>();

            services.AddScoped<IAvailabilityService, AvailabilityService>();
            services.AddScoped<IAbsenceService, AbsenceService>();

            services.AddScoped<IAvailabilityRuleRepository, AvailabilityRuleRepository>();
            services.AddScoped<IAvailabilityRuleService, AvailabilityRuleService>();
            // Add claims transformation to split the scope claim value

            services.AddMvc(options =>
            {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            })
                .AddDataAnnotationsLocalization();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            // if (env.IsDevelopment())
            // {
                app.UseDeveloperExceptionPage();
            // }
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger Sample");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var supportedCultures = new[]
           {
                new CultureInfo("bs"),
            };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("bs"),
                // Formatting numbers, dates, etc.
                SupportedCultures = supportedCultures,
                // UI strings that we have localized.
                SupportedUICultures = supportedCultures
            });
            app.UseStaticFiles();
         }
    }
}
