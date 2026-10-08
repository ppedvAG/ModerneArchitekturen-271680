namespace EasyBib.Domain.Models;

public class Member
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public Membership? Membership { get; set; }
}
