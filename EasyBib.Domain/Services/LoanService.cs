// ============================================================
// Domain: Erweiterte Loan-Entität mit Prozessmethoden
// (Status-Setter ist privat, Übergänge nur über Methoden)
// ============================================================
using EasyBib.Domain.Contracts;
using EasyBib.Domain.Enums;
using EasyBib.Domain.Entities;

namespace EasyBib.Domain.Services;

// ============================================================
// Domain: LoanService (koordiniert Membership + MediaItem)
// ============================================================
public class LoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IMediaItemRepository _mediaItemRepository;

    public LoanService(
        ILoanRepository loanRepository,
        IMemberRepository memberRepository,
        IMediaItemRepository mediaItemRepository)
    {
        _loanRepository = loanRepository;
        _memberRepository = memberRepository;
        _mediaItemRepository = mediaItemRepository;
    }

    public Loan CheckOut(Guid memberId, Guid mediaItemId)
    {
        var member = _memberRepository.GetById(memberId)
            ?? throw new InvalidOperationException($"Member '{memberId}' not found.");

        var membership = member.Membership
            ?? throw new InvalidOperationException("Member has no membership.");

        var mediaItem = _mediaItemRepository.GetById(mediaItemId)
            ?? throw new InvalidOperationException($"MediaItem '{mediaItemId}' not found.");

        var activeLoans = _loanRepository.GetActiveLoansFor(membership.Id);
        if (activeLoans.Count >= membership.MaxActiveLoans)
            throw new InvalidOperationException(
                $"Membership limit reached ({membership.MaxActiveLoans} active loans).");

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var loan = Loan.Create(membership.Id, mediaItem.Id, today.AddDays(membership.LoanPeriodDays));

        _loanRepository.Add(loan);
        return loan;
    }
}