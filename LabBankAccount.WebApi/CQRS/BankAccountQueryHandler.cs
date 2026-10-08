using LabEventSourcing.Events;
using MediatR;

namespace LabBankAccount.WebApi.CQRS;

public record StatementActionDto(string action, DateTime Timestamp, decimal? Amount);

public class BankAccountQueryHandler : 
    IRequestHandler<GetBalanceQuery, decimal>, 
    IRequestHandler<GetStatementQuery, IEnumerable<StatementActionDto?>>
{
    private readonly BankAccountFactory _factory;

    private BankAccount Account 
        => _factory.CreateFromRouteParam();

    public BankAccountQueryHandler(BankAccountFactory factory) 
        => _factory = factory;

    public Task<decimal> Handle(GetBalanceQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Account.GetBalance());
    }

    public Task<IEnumerable<StatementActionDto?>> Handle(GetStatementQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Account.Select(ToDto));
    }

    private static StatementActionDto? ToDto(Event e) => e switch
    {
        AccountOpenedEvent o => new StatementActionDto("new", o.Timestamp, o.InitialBalance),
        MoneyDepositedEvent d => new StatementActionDto("in", d.Timestamp, d.Amount),
        MoneyWithdrawnEvent w => new StatementActionDto("out", w.Timestamp, -w.Amount),
        AccountClosedEvent c => new StatementActionDto("~", c.Timestamp, null),
        _ => null
    };
}