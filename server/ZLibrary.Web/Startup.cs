using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using ZLibrary.API;
using ZLibrary.Core;
using ZLibrary.Persistence;
using ZLibrary.Web.Factory;
using ZLibrary.Web.Factory.Impl;
using ZLibrary.Web.Options;

namespace ZLibrary.Web
{
    public partial class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add DbContext
            services.AddDbContext<ZLibraryContext>(o => o.UseInMemoryDatabase());

            // Add framework services.
            services.AddMvc();

            // Add application services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITokenFactory, JsonWebTokenFactory>();

            services.Configure<JwtOptions>(o =>
            {
                var options = BuildJwtOptions();

                o.Issuer = options.Issuer;
                o.Audience = options.Audience;
                o.Authority = options.Authority;
                o.Expires = options.Expires;
                o.SigningCredentials = options.SigningCredentials;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            ConfigureAuth(app);
            SeedDatabase(app);

            // TODO: Stricter CORS rules in Production
            app.UseCors(builder =>
            {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
            
            app.UseMvc();
        }

        private JwtOptions BuildJwtOptions()
        {
            var options = Configuration.GetSection("JwtOptions");
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options["Key"]));
            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            return new JwtOptions()
            {
                Issuer = options["Issuer"],
                Audience = options["Audience"],
                Authority = options["Authority"],
                Expires = long.TryParse(options["ExpiresInHours"], out long expiresResult)
                    ? TimeSpan.FromHours(expiresResult)
                    : (TimeSpan?)null,
                SigningCredentials = signingCredentials,
            };
        }
    }
}
