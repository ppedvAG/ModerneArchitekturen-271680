using EasyBib.Domain.Contracts;
using EasyBib.Domain.Entities;
using EasyBib.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace EasyBib.Infrastructure;

public class MemberRepository : IMemberRepository
{
    private readonly EasyBibDbContext _context;

    public MemberRepository(EasyBibDbContext context) => _context = context;

    public Member? GetById(Guid id) =>
        _context.Members.Include(m => m.Membership).FirstOrDefault(m => m.Id == id);

    public Task Add(Member member)
    {
        _context.Members.Add(member);
        return _context.SaveChangesAsync();
    }

    public Task Update(Member member)
    {
        _context.Members.Update(member);
        return _context.SaveChangesAsync();
    }

    public Task Remove(Member member)
    {
        _context.Members.Remove(member);
        return _context.SaveChangesAsync();
    }
}
