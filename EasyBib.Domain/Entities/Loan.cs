using EasyBib.Domain.Enums;

namespace EasyBib.Domain.Entities;

public class Loan
{
    public Guid Id { get; set; }
    public Guid MembershipId { get; set; }
    public Guid MediaItemId { get; set; }

    public LoanStatus Status { get; set; }
    public DateOnly DueDate { get; set; }

    public Membership? Membership { get; set; }
    public MediaItem? MediaItem { get; set; }

    public Loan() { } // für EF Core

    public static Loan Create(Guid membershipId, Guid mediaItemId, DateOnly dueDate)
    {
        return new Loan
        {
            Id = Guid.NewGuid(),
            MembershipId = membershipId,
            MediaItemId = mediaItemId,
            Status = LoanStatus.Active,
            DueDate = dueDate
        };
    }

    public void MarkReturned()
    {
        if (Status == LoanStatus.Returned)
            throw new InvalidOperationException("Loan is already returned.");

        Status = LoanStatus.Returned;
    }

    public void MarkAsOverdue(DateOnly today)
    {
        if (Status != LoanStatus.Active)
            throw new InvalidOperationException("Only active loans can be marked as overdue.");

        if (DueDate >= today)
            throw new InvalidOperationException("Loan is not due yet.");

        Status = LoanStatus.Overdue;
    }

    public void Extend(int days)
    {
        if (Status != LoanStatus.Active)
            throw new InvalidOperationException("Only active loans can be extended.");

        DueDate = DueDate.AddDays(days);
    }
}