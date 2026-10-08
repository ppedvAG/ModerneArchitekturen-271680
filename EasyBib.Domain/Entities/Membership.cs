using EasyBib.Domain.Enums;

namespace EasyBib.Domain.Entities;

public class Membership
{
    public Guid Id { get; set; }
    public Guid MemberId { get; set; }

    public MembershipPlan PlanName { get; set; }
    public int MaxActiveLoans { get; set; }
    public int LoanPeriodDays { get; set; }

    public Member? Member { get; set; }
}
