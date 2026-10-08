using EasyBib.Domain.Contracts;
using EasyBib.Domain.Enums;
using EasyBib.Domain.Entities;
using EasyBib.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace EasyBib.Infrastructure;

public class LoanRepository : ILoanRepository
{
    private readonly EasyBibDbContext _context;

    public LoanRepository(EasyBibDbContext context) => _context = context;

    public Loan? GetById(Guid id) =>
        _context.Loans
            .Include(l => l.Membership!)
            .Include(l => l.MediaItem)
            .FirstOrDefault(l => l.Id == id);

    public Task Add(Loan loan)
    {
        _context.Loans.Add(loan);
        return _context.SaveChangesAsync();
    }

    public Task Save(Loan loan)
    {
        // Update nur über Change Tracker; Übergänge liefen über Loan-Methoden
        _context.Loans.Update(loan);
        return _context.SaveChangesAsync();
    }

    public IReadOnlyList<Loan> GetActiveLoansFor(Guid membershipId) =>
        _context.Loans
            .Where(l => l.MembershipId == membershipId
                     && l.Status != LoanStatus.Returned)
            .ToList();
}