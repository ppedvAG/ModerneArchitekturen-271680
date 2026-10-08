using EasyBib.Domain.Enums;

namespace EasyBib.Domain.Models;

public class MediaItem
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string EAN { get; set; } = string.Empty;
    public MediaType Type { get; set; }
}
