using Coravel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using webscrapper.core.daemon.CronJobManager;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<ScraperQueue>();
builder.Services.AddTransient<ScraperJob>();
builder.Services.AddHostedService<ScraperProcessor>();

builder.Services.AddScheduler();

var host = builder.Build();

host.Services.UseScheduler(scheduler =>
{
    //var scraperData = new List<(string Title, string Cron)>
    //{
    //    ("cron job 1", "* * * * *"),
    //    ("cron job 2", "*/5 * * * *")
    //};

    //foreach (var item in scraperData)
    //{
    //    scheduler.ScheduleWithParams<ScraperJob>(new ScraperTask(item.Title, item.Cron))
    //             .Cron(item.Cron);
    //}

    scheduler.Schedule<ScraperJob>().EveryMinute();
});

await host.RunAsync();