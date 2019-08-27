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
using ZLibrary.Web.Controllers;
using ZLibrary.Web.Converters;
using ZLibrary.Web.Validators;
using ZLibrary.Web.LookUps;

namespace ZLibrary.Web
{
    public partial class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }


            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add InMemoryDbContext
            //services.AddDbContext<ZLibraryContext>(o => o.UseInMemoryDatabase("ZLibrary_Dev"));

            // Add DbContext
            var connectionString = Configuration.GetConnectionString("SqlServerDatabase");

            var featureSettingsSection = Configuration.GetSection("FeatureSettings");
            services.Configure<FeatureSettings>(featureSettingsSection);

            services.AddDbContext<ZLibraryContext>(o => o.UseSqlServer(connectionString));

            var jwtOptions = BuildJwtOptions();
            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = jwtOptions.SigningCredentials.Key,
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,
                RequireExpirationTime = false
            };

            services.AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = tokenValidationParameters;
                });

            // Add framework services.
            services.AddMvc().AddJsonOptions(
                options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.Add(new ServiceDescriptor(typeof(ClientOptions), provider => BuildClientOptions(), ServiceLifetime.Singleton));


            //services
            services.AddTransient<IBookFacade, BookFacade>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IPublisherService, PublisherService>();
            services.AddTransient<IReservationService, ReservationService>();
            services.AddTransient<ILoanService, LoanService>();
            services.AddTransient<IServiceDataLookUp, DefaultServiceDataLookUp>();

            //repositories
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IReservationRepository, ReservationRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IPublisherRepository, PublisherRepository>();
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<ILoanRepository, LoanRepository>();

            services.AddTransient<ITokenFactory, JsonWebTokenFactory>();
            services.AddTransient<IAuthenticationApi, SlackApi>();
            
            services.AddTransient<IValidationResultDataLookUp, DefaultValidationResultDataLookUp>();
            services.AddTransient<ValidationResult, ValidationResult>();

            //converters
            services.AddTransient<AuthorConverter, AuthorConverter>();
            services.AddTransient<PublisherConverter, PublisherConverter>();
            services.AddTransient<BookConverter, BookConverter>();
            services.AddTransient<LoanConverter, LoanConverter>();
            services.AddTransient<ReservationConverter, ReservationConverter>();
            services.AddTransient<UserConverter, UserConverter>();

            //validators
            services.AddTransient<AuthorDtoValidator, AuthorDtoValidator>();
            services.AddTransient<IsbnValidator, IsbnValidator>();

            var featureSettings = new FeatureSettings();
            Configuration.GetSection("FeatureSettings").Bind(featureSettings);

            if (featureSettings.AllowCoverImage)
            {
                services.AddTransient<IImageService, ImageService>();
            }
            else
            {
                services.AddTransient<IImageService, NoImageService>();
            }

            services.Configure<JwtOptions>(o =>
            {
                var options = BuildJwtOptions();

                o.Issuer = options.Issuer;
                o.Audience = options.Audience;
                o.Authority = options.Authority;
                o.Expires = options.Expires;
                o.SigningCredentials = options.SigningCredentials;
            });

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(Configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //InMemory DB
            //SeedDatabase(app);

            //Sql Server
            var serviceScope = app.ApplicationServices.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ZLibraryContext>();
            context.Database.Migrate();


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

        private ClientOptions BuildClientOptions()
        {
            var options = Configuration.GetSection("ClientOptions");
            return new ClientOptions() { ClientUrl = options["Url"] };
        }
    }
}
