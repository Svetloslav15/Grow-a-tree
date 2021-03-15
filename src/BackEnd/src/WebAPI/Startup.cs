namespace GrowATree.WebAPI
{
    using System.Linq;
    using CloudinaryDotNet;
    using Common.Constants;
    using Common.Interfaces;
    using Common.Services;
    using GrowATree.Application;
    using GrowATree.Application.Common.Interfaces;
    using GrowATree.Infrastructure;
    using GrowATree.Infrastructure.Persistence;
    using GrowATree.WebAPI.Filters;
    using GrowATree.WebAPI.Services;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NSwag;
    using NSwag.Generation.Processors.Security;

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
            services
                .AddApplication()
                .AddInfrastructure(this.Configuration)
                .AddScoped<ICurrentUserService, CurrentUserService>()
                .AddHttpContextAccessor()
                .AddHealthChecks()
                .AddDbContextCheck<ApplicationDbContext>();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsApi",
                    builder => builder.WithOrigins("http://localhost:3000", "http://growatree.eu", "https://growatree.eu")
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
            });

            services.AddControllersWithViews(options =>
                options.Filters.Add(new ApiExceptionFilter()));

            services.AddRazorPages();

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            })
                .Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = Constants.PasswordMinLength;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.User.AllowedUserNameCharacters += " ";
                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddOpenApiDocument(configure =>
            {
                configure.Title = "GrowATree API";
                configure.AddSecurity("JWT", Enumerable.Empty<string>(), new OpenApiSecurityScheme
                {
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Description = "Type into the textbox: Bearer {your JWT token}.",
                });

                configure.OperationProcessors.Add(new AspNetCoreOperationSecurityScopeProcessor("JWT"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage()
                   .UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error")
                   .UseHsts();
            }

            app.UseHealthChecks("/health")
               .UseHttpsRedirection()
               .UseStaticFiles()
               .UseSwaggerUi3(settings =>
               {
                   settings.Path = "/api";
                   settings.DocumentPath = "/api/specification.json";
               })
               .UseRouting()
               .UseCors("CorsApi")
               .UseAuthentication()
               .UseIdentityServer()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
               {
                   endpoints.MapControllerRoute(
                       name: "default",
                       pattern: "{controller}/{action=Index}/{id?}");
                   endpoints.MapRazorPages();
               });
        }
    }
}