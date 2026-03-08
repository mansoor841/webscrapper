using CarrierFeedDownload.CrossCutting.Adapter.Models;

namespace CarrierFeedDownload.CrossCutting.Adapter.Interfaces;

public interface IAdapterTask
{
    string TaskName { get; }
    Task<TaskResult> ExecuteAsync(CancellationToken cancellationToken = default);
}