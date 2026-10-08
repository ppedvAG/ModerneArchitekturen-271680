using EasyBib.Domain.Contracts;
using EasyBib.Domain.Entities;
using EasyBib.WebMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace EasyBib.WebMvc.Controllers;

public class MediaItemController : Controller
{
    private readonly IMediaItemRepository _repository;

    public MediaItemController(IMediaItemRepository repository)
    {
        _repository = repository;
    }

    // GET: /MediaItem
    public IActionResult Index(string? search)
    {
        var items = _repository.List();

        if (!string.IsNullOrWhiteSpace(search))
        {
            items = items
                .Where(x => x.EAN.Contains(
                    search,
                    StringComparison.OrdinalIgnoreCase))
                .ToList();

            ViewBag.Search = search;
        }

        var model = items
            .Select(MapToViewModel)
            .ToList();

        return View(model);
    }

    // GET: /MediaItem/Details/{id}
    public IActionResult Details(Guid id)
    {
        var item = _repository.GetById(id);

        if (item is null)
            return NotFound();

        return View(MapToViewModel(item));
    }

    // GET: /MediaItem/Create
    public IActionResult Create()
    {
        return View(new MediaItemViewModel());
    }

    // POST: /MediaItem/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(MediaItemViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var item = new MediaItem
        {
            Id = Guid.NewGuid(),
            EAN = model.EAN
        };

        await _repository.Add(item);

        TempData["Success"] = "Das Medium wurde erfolgreich angelegt.";

        return RedirectToAction(nameof(Index));
    }

    // GET: /MediaItem/Edit/{id}
    public IActionResult Edit(Guid id)
    {
        var item = _repository.GetById(id);

        if (item is null)
            return NotFound();

        return View(MapToViewModel(item));
    }

    // POST: /MediaItem/Edit
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(MediaItemViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var item = _repository.GetById(model.Id);

        if (item is null)
            return NotFound();

        item.EAN = model.EAN;

        await _repository.Update(item);

        TempData["Success"] = "Das Medium wurde erfolgreich geändert.";

        return RedirectToAction(nameof(Index));
    }

    // POST: /MediaItem/Delete
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        var item = _repository.GetById(id);

        if (item is null)
            return NotFound();

        await _repository.Remove(item);

        TempData["Success"] = "Das Medium wurde gelöscht.";

        return RedirectToAction(nameof(Index));
    }

    private static MediaItemViewModel MapToViewModel(MediaItem item)
    {
        return new MediaItemViewModel
        {
            Id = item.Id,
            EAN = item.EAN
        };
    }
}