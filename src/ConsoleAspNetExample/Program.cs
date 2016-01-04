using System;
using System.Threading;
using ConsoleAspNetExample.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAspNetExample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = GetAllServices();

            using (var host = new WebHost())
            {
                host.Start(services);

                Console.WriteLine("Web host started");

                var serviceProvider = host.HostServiceProvider;
                var valueHolder = serviceProvider.GetService<IValueHolder>();

                valueHolder.AddOne();
                valueHolder.AddOne();
                valueHolder.AddOne();
                valueHolder.AddOne();
                Console.WriteLine($"Current value: {valueHolder.Get()}");

                while (true)
                {
                    Thread.Sleep(100);
                }
            }
        }

        private static ServiceDescriptor[] GetAllServices()
        {
            return new[] { new ServiceDescriptor(typeof(IValueHolder), typeof(ValueHolder), ServiceLifetime.Singleton) };
        }
    }
}
