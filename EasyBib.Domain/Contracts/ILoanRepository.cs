// ============================================================
// Domain: Erweiterte Loan-Entität mit Prozessmethoden
// (Status-Setter ist privat, Übergänge nur über Methoden)
// ============================================================
using EasyBib.Domain.Entities;

namespace EasyBib.Domain.Contracts;

public interface ILoanRepository
{
    // Bewusst KEIN Update: Statusübergänge nur über die Loan-Methoden
    Loan? GetById(Guid id);
    Task Add(Loan loan);
    Task Save(Loan loan);
    IReadOnlyList<Loan> GetActiveLoansFor(Guid membershipId);
}
