using Scraping.Common.Interfaces;
using Scraping.Common.Models;

namespace Scraping.Common.Abstractions;

public abstract class BaseAuthenticationTask : BaseScrapingTask, IAuthenticationTask
{
    protected AuthenticationResult? CurrentAuthentication { get; private set; }

    public abstract Task<AuthenticationResult> AuthenticateAsync(Credentials credentials, CancellationToken cancellationToken = default);

    public virtual Task<bool> IsAuthenticatedAsync(CancellationToken cancellationToken = default)
    {
        if (CurrentAuthentication == null)
        {
            return Task.FromResult(false);
        }

        if (CurrentAuthentication.ExpiresAt.HasValue && CurrentAuthentication.ExpiresAt.Value < DateTime.UtcNow)
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(CurrentAuthentication.IsAuthenticated);
    }

    protected void SetAuthentication(AuthenticationResult result) => CurrentAuthentication = result;
}