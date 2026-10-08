using EasyBib.Domain.Entities;

namespace EasyBib.Domain.Contracts;

public interface IMemberRepository
{
    Member? GetById(Guid id);
    Task Add(Member member);
    Task Update(Member member);
    Task Remove(Member member);
}
