using Scraping.Common.Models;

namespace Scraping.Common.Interfaces;

public interface IAuthenticationTask : IScrapingTask
{
    Task<AuthenticationResult> AuthenticateAsync(Credentials credentials, CancellationToken cancellationToken = default);
    Task<bool> IsAuthenticatedAsync(CancellationToken cancellationToken = default);
}