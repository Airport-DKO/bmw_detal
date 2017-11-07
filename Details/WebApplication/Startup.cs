using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            var connectionString =
                "Server=185.159.130.85;Port=5432;Database=details;User Id=postgres; Password=nm34kd27;";
            services.AddTransient<IBaseRepository, BaseRepository>(provider =>
                new BaseRepository(connectionString));
            services.AddTransient<IDetailRepository, DetailRepository>(provider =>
                new DetailRepository(connectionString));
            services.AddTransient<ICallMeTicketRepository, CallMeTicketRepository>(provider =>
                new CallMeTicketRepository(connectionString));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }
    }
}