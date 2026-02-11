using Coravel.Invocable;

namespace webscrapper.core.daemon.CronJobManager
{
    public class ScraperJob : IInvocable
    {
        private readonly ScraperQueue _queue;

        public ScraperTask TaskToExecute { get; set; }

        public ScraperJob(ScraperQueue queue) => _queue = queue;

        public async Task Invoke()
        {
            await _queue.Writer.WriteAsync(TaskToExecute);

            Console.WriteLine($"[Cron] Scheduled task for: {TaskToExecute.Title}");
        }
    }
}
