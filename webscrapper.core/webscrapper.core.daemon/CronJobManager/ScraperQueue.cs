using System.Threading.Channels;

namespace webscrapper.core.daemon.CronJobManager
{
    public record ScraperTask(string Title, string Cron);

    public class ScraperQueue
    {
        private readonly Channel<ScraperTask> _channel = Channel.CreateUnbounded<ScraperTask>();

        public ChannelWriter<ScraperTask> Writer => _channel.Writer;
        public ChannelReader<ScraperTask> Reader => _channel.Reader;
    }
}
