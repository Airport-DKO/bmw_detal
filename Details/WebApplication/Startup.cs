using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApplication.Repositories.Entities;
using WebApplication.Repositories.Interfaces;

namespace WebApplication
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
            var connectionString = "Server=185.159.130.85;Port=5432;Database=details;User Id=postgres; Password=nm34kd27;";

            services.AddTransient<IBaseRepository, BaseRepository>(provider => new BaseRepository(connectionString));
            services.AddTransient<ICallMeTicketRepository, CallMeTicketRepository>(provider => new CallMeTicketRepository(connectionString));
            services.AddTransient<IDetailRepository, DetailRepository>(provider => new DetailRepository(connectionString));
            services.AddTransient<IOrderRepository, OrderRepository>(provider => new OrderRepository(connectionString));
            services.AddTransient<IDeliveryMethodRepository, DeliveryMethodRepository>(provider => new DeliveryMethodRepository(connectionString));


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                            ValidateIssuer = true,
                            // строка, представляющая издателя
                            ValidIssuer = AuthOptions.ISSUER,

                            // будет ли валидироваться потребитель токена
                            ValidateAudience = true,
                            // установка потребителя токена
                            ValidAudience = AuthOptions.AUDIENCE,
                            // будет ли валидироваться время существования
                            ValidateLifetime = true,

                            // установка ключа безопасности
                            IssuerSigningKey = AuthOptions.KEY,
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                        };
                    });

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseAuthentication();

            app.UseMvc();
        }
    }

    public class AuthOptions
    {
        private const string _key = "details!_13579__details!"; // ключ для формирования токена

        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "MyUser"; // потребитель токена
        public const int LIFETIME = 10; // время жизни токена - 10 минут
        public static SymmetricSecurityKey KEY => new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_key));
    }
}