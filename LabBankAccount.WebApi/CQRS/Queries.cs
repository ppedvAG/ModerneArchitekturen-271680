using MediatR;

namespace LabBankAccount.WebApi.CQRS;

public record GetBalanceQuery(Guid AccountId) : IRequest<decimal>;

public record GetStatementQuery(Guid AccountId) : IRequest<IEnumerable<StatementActionDto?>>;
