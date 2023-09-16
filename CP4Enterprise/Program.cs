using CP4Enterprise.Domain.Interfaces;
using CP4Enterprise.Infra;
using CP4Enterprise.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CP4Enterprise
{
    public class Program
    {
        static void Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton<ICLTService, CLTService>();
                services.AddSingleton<IPJService, PJService>();
                services.AddSingleton<IEmployeeService, EmployeeService>();
                services.AddSingleton<IMenuService, MenuService>();

                services.AddSingleton<IEmployeeRepository, EmployeeRepository>();

                services.AddSingleton<App>();
            })
            .Build();

            var app = host.Services.GetService<App>();
            App.Run(args);
        }
    }
}