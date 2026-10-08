// ============================================================
// Domain: Erweiterte Loan-Entität mit Prozessmethoden
// (Status-Setter ist privat, Übergänge nur über Methoden)
// ============================================================
using EasyBib.Domain.Entities;

namespace EasyBib.Domain.Contracts;

public interface IMediaItemRepository
{
    MediaItem? GetById(Guid id);
    MediaItem? GetByEan(string ean);
    Task Add(MediaItem item);
    Task Update(MediaItem item);
    Task Remove(MediaItem item);
    IReadOnlyList<MediaItem> List();
}
