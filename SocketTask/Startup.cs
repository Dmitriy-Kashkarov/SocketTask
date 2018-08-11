using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocketTask.DAL;
using SocketTask.Implementarions;
using SocketTask.Interfaces;
using ILogger = SocketTask.Interfaces.ILogger;

namespace SocketTask
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSingleton<IConfigurationManager, ConfigurationManager>();

            services.AddTransient<IDatabaseContext, DatabaseContext>();

            services.AddTransient<IReceiveDataRepository, ReceiveDataRepository>();

            services.AddSingleton<ILogger, Logger>();

            services.AddTransient<ISocketCommunication, SocketCommunication>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=ReceivedData}/{action=Index}");
            });
        }
    }
}
