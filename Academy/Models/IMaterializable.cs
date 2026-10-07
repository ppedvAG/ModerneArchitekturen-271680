using Academy.Events;

namespace Academy.Models
{
    public interface IMaterializable<T> where T : class
    {
        T Apply(DomainEvent @event);
    }
}