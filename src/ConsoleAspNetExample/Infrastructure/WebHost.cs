using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Hosting.Internal;
using Microsoft.AspNet.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAspNetExample.Infrastructure
{
    public class WebHost : IDisposable
    {
        private IHostingEngine _hostingEngine;
        private IApplication _application;

        public IServiceProvider HostServiceProvider => _application?.Services;

        public void Start(ServiceDescriptor[] additionalServices)
        {
            if (_hostingEngine != null)
            {
                throw new InvalidOperationException("Hosting engine already initialized");
            }

            var configurationBuilder = new ConfigurationBuilder();
            //configurationBuilder.AddEnvironmentVariables();
            configurationBuilder.AddInMemoryCollection(new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("server.urls", "http://localhost:5005")
            });

            var config = configurationBuilder.Build();

            _hostingEngine = new WebHostBuilder(config)
                .UseServer("Microsoft.AspNet.Server.Kestrel")
                .UseServices(x =>
                {
                    x.AddMvc();

                    foreach (var additionalService in additionalServices)
                    {
                        x.Add(additionalService);
                    }
                })
                .UseStartup<WebHost>()
                .Build();

            _application = _hostingEngine.Start();
        }

        public void Dispose()
        {
            _application?.Dispose();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
