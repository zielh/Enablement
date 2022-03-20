// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz.Spi;
using SampleRestAPI2.DAL.Repository;
using SampleRestAPI2.SchedulerApp;
using SampleRestAPI2.SchedulerApp.Job;

IHostBuilder builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddDbContext<SampleRestAPI2Context>(opt => opt.UseSqlServer(hostContext.Configuration["ConnectionStrings:DefaultConnection"]));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddHostedService<SchedulerService>();
        services.AddSingleton<IJobFactory, QuartzJobFactory>();
        services.AddTransient<LogInsertJob>();
    });
IHost host = builder.Build();
host.Run();
