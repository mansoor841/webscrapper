using Microsoft.Extensions.Hosting;

namespace webscrapper.core.daemon.CronJobManager
{
    public class ScraperProcessor : BackgroundService
    {
        private readonly ScraperQueue _queue;
        public ScraperProcessor(ScraperQueue queue) => _queue = queue;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await foreach (var task in _queue.Reader.ReadAllAsync(stoppingToken))
            {
                Console.WriteLine($"[Processor] Starting scrape: {task.Title}");

                await Task.Delay(3000, stoppingToken);

                Console.WriteLine($"[Processor] Completed: {task.Title}");
            }
        }
    }
}
