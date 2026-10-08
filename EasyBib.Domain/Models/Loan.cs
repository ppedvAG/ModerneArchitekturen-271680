using EasyBib.Domain.Enums;

namespace EasyBib.Domain.Models;

public class Loan
{
    public Guid Id { get; set; }
    public Guid MembershipId { get; set; }
    public Guid MediaItemId { get; set; }

    public LoanStatus Status { get; set; }
    public DateOnly DueDate { get; set; }

    public Membership? Membership { get; set; }
    public MediaItem? MediaItem { get; set; }
}