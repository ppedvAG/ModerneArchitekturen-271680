using EasyBib.Domain.Contracts;
using EasyBib.Domain.Entities;
using EasyBib.Infrastructure.Persistance;

namespace EasyBib.Infrastructure;

public class MediaItemRepository : IMediaItemRepository
{
    private readonly EasyBibDbContext _context;

    public MediaItemRepository(EasyBibDbContext context) => _context = context;

    public MediaItem? GetById(Guid id) => _context.MediaItems.Find(id);

    public MediaItem? GetByEan(string ean) =>
        _context.MediaItems.FirstOrDefault(m => m.EAN == ean);

    public Task Add(MediaItem item)
    {
        _context.MediaItems.Add(item);
        return _context.SaveChangesAsync();
    }

    public Task Update(MediaItem item)
    {
        _context.MediaItems.Update(item);
        return _context.SaveChangesAsync();
    }

    public Task Remove(MediaItem item)
    {
        _context.MediaItems.Remove(item);
        return _context.SaveChangesAsync();
    }

    public IReadOnlyList<MediaItem> List() => _context.MediaItems.ToList();
}
