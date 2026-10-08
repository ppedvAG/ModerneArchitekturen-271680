
using LabEventSourcing.Events;

namespace LabBankAccount.WebApi;

public class BankAccountFactory
{
    private readonly EventStore _eventStore;
    private readonly HttpContext? _httpContext;

    public BankAccountFactory(EventStore store, IHttpContextAccessor accessor)
    {
        _eventStore = store;
        _httpContext = accessor.HttpContext;
    }

    public BankAccount CreateNew(Guid accountId)
    {
        return new BankAccount(_eventStore, accountId);
    }

    public BankAccount CreateFromRouteParam(string key = "accountId")
    {
        // Extract Account ID from route data or query string
        var accountIdStr = _httpContext.Request.RouteValues[key]?.ToString()
            ?? _httpContext.Request.Query[key].ToString();

        if (Guid.TryParse(accountIdStr, out var accountId))
            return new BankAccount(_eventStore, accountId);

        throw new KeyNotFoundException($"{key} not found in route data or query string.");
    }
}
