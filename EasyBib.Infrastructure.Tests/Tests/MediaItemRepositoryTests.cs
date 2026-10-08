using EasyBib.Domain;
using EasyBib.Domain.Entities;
using EasyBib.Domain.Enums;
using EasyBib.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EasyBib.Infrastructure.Tests;

public class MediaItemRepositoryTests : LocalDbTestBase
{
    protected MediaItemRepository CreateMediaItemRepository() => new(Context);

    // ---------- GetById ----------

    [Fact]
    public void GetById_ExistingId_ReturnsSeededItem()
    {
        // Arrange
        var repository = CreateMediaItemRepository();

        // Act
        var item = repository.GetById(Seed.MediaItems.SpaceJam);

        // Assert
        Assert.NotNull(item);
        Assert.Equal("Space Jam", item!.Title);
        Assert.Equal(MediaType.Movie, item.Type);
    }

    [Fact]
    public void GetById_UnknownId_ReturnsNull()
    {
        // Arrange
        var repository = CreateMediaItemRepository();

        // Act
        var item = repository.GetById(Guid.NewGuid());

        // Assert
        Assert.Null(item);
    }

    // ---------- GetByEan ----------

    [Fact]
    public void GetByEan_ExistingEan_ReturnsMatchingItem()
    {
        // Arrange
        var repository = CreateMediaItemRepository();

        // Act
        var item = repository.GetByEan("9783866812345");

        // Assert
        Assert.NotNull(item);
        Assert.Equal(Seed.MediaItems.BenderCookbook, item!.Id);
        Assert.Equal(MediaType.Book, item.Type);
    }

    [Fact]
    public void GetByEan_UnknownEan_ReturnsNull()
    {
        // Arrange
        var repository = CreateMediaItemRepository();

        // Act
        var item = repository.GetByEan("0000000000000");

        // Assert
        Assert.Null(item);
    }

    // ---------- List ----------

    [Fact]
    public void List_SeededDatabase_ReturnsAllSeededItems()
    {
        // Arrange
        var repository = CreateMediaItemRepository();

        // Act
        var items = repository.List();

        // Assert
        Assert.Equal(10, items.Count);
        Assert.Contains(items, i => i.Id == Seed.MediaItems.BendersBigScore);
    }

    // ---------- Add ----------

    [Fact]
    public async Task Add_NewItem_PersistsItem()
    {
        // Arrange
        var newItem = new MediaItem
        {
            Id = Guid.NewGuid(),
            Title = "Futurama: Der große Bender-Test",
            EAN = "9783000000001",
            Type = MediaType.Book
        };
        var repository = CreateMediaItemRepository();

        // Act
        await repository.Add(newItem);

        // Assert – frischer Kontext, damit wirklich DB-Persistenz geprüft wird
        await using var verify = CreateVerifyContext();
        var persisted = await verify.MediaItems.SingleAsync(i => i.Id == newItem.Id);
        Assert.Equal(newItem.Title, persisted.Title);
        Assert.Equal(newItem.EAN, persisted.EAN);
    }

    [Fact]
    public async Task Add_DuplicateEan_ThrowsDbUpdateException()
    {
        // Arrange – gleiche EAN wie ein geseedetes Item
        var duplicate = new MediaItem
        {
            Id = Guid.NewGuid(),
            Title = "Piratenausgabe",
            EAN = "9783866812345",
            Type = MediaType.Book
        };
        var repository = CreateMediaItemRepository();

        // Act & Assert – eindeutiger Index auf EAN verhindert zweiten Insert
        await Assert.ThrowsAsync<DbUpdateException>(() => repository.Add(duplicate));
    }

    // ---------- Update ----------

    [Fact]
    public async Task Update_ExistingItem_PersistsChanges()
    {
        // Arrange
        var repository = CreateMediaItemRepository();
        var item = repository.GetById(Seed.MediaItems.FuturamaGame)!;
        item.Title = "Futurama: Das Videospiel – Remaster";

        // Act
        await repository.Update(item);

        // Assert
        await using var verify = CreateVerifyContext();
        var persisted = await verify.MediaItems.SingleAsync(i => i.Id == item.Id);
        Assert.Equal("Futurama: Das Videospiel – Remaster", persisted.Title);
    }

    // ---------- Remove ----------

    [Fact]
    public async Task Remove_ExistingItem_DeletesItemFromDatabase()
    {
        // Arrange
        var repository = CreateMediaItemRepository();
        var item = repository.GetById(Seed.MediaItems.BackInAction)!;

        // Act
        await repository.Remove(item);

        // Assert
        await using var verify = CreateVerifyContext();
        Assert.False(await verify.MediaItems.AnyAsync(i => i.Id == item.Id));
        Assert.Equal(9, await verify.MediaItems.CountAsync());
    }
}