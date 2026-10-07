using Academy.Events;
using Academy.Models;

namespace Academy.Store;

public class InMemoryEventStore<T> where T : class, IMaterializable<T>, new()
{
    private readonly Dictionary<Guid, SortedList<DateTime, DomainEvent>> _eventStore = [];

    public void Append(DomainEvent domainEvent)
    {
        if (!_eventStore.ContainsKey(domainEvent.StreamId))
        {
            _eventStore[domainEvent.StreamId] = [];
        }
        domainEvent.CreatedAtUtc = DateTime.UtcNow;
        _eventStore[domainEvent.StreamId].Add(domainEvent.CreatedAtUtc, domainEvent);

        // Projektion aufbauen und im Cache speichern
        var entity = GetEntity(domainEvent.StreamId);
        _entityCache[domainEvent.StreamId] = entity;
    }

    // Entität aus den Events materialisieren
    public T GetEntity(Guid streamId)
    {
        if (!_eventStore.ContainsKey(streamId))
        {
            throw new InvalidOperationException($"No events found for student with ID {streamId}");
        }

        return _eventStore[streamId]
            .Aggregate(new T(), (student, pair) => student.Apply(pair.Value));
    }

    private readonly Dictionary<Guid, T> _entityCache = [];

    public T? GetEntityProjection(Guid streamId)
    {
        return _entityCache.GetValueOrDefault(streamId);
    }
}
