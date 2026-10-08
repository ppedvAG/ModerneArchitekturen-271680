using EasyBib.Domain.Contracts;
using EasyBib.Domain.Entities;
using EasyBib.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyBib.WebApi;

// ============================================================
// MediaItemsController (reines CRUD)
// ============================================================
[ApiController]
[Route("api/[controller]")]
public class MediaItemsController : ControllerBase
{
    private readonly IMediaItemRepository _mediaItemRepository;

    public MediaItemsController(IMediaItemRepository mediaItemRepository)
        => _mediaItemRepository = mediaItemRepository;

    [HttpGet]
    public ActionResult<IReadOnlyList<MediaItemDto>> GetAll() =>
        Ok(_mediaItemRepository.List()
            .Select(m => new MediaItemDto(m.Id, m.Title, m.EAN, m.Type))
            .ToList());

    [HttpGet("{id}")]
    public ActionResult<MediaItemDto> GetById(Guid id)
    {
        var item = _mediaItemRepository.GetById(id);
        if (item is null) return NotFound();
        return Ok(new MediaItemDto(item.Id, item.Title, item.EAN, item.Type));
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateMediaItemRequest request)
    {
        var item = new MediaItem
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            EAN = request.EAN,
            Type = request.Type
        };
        _mediaItemRepository.Add(item);
        return CreatedAtAction(nameof(GetById), new { id = item.Id },
            new MediaItemDto(item.Id, item.Title, item.EAN, item.Type));
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, [FromBody] CreateMediaItemRequest request)
    {
        var item = _mediaItemRepository.GetById(id);
        if (item is null) return NotFound();
        item.Title = request.Title;
        item.EAN = request.EAN;
        item.Type = request.Type;
        _mediaItemRepository.Update(item);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var item = _mediaItemRepository.GetById(id);
        if (item is null) return NotFound();
        _mediaItemRepository.Remove(item);
        return NoContent();
    }
}
