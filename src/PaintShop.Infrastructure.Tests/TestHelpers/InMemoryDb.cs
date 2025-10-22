using System;
using Microsoft.EntityFrameworkCore;
using PaintShop.Infrastructure;

namespace PaintShop.Infrastructure.Tests.TestHelpers;

public static class InMemoryDb
{
    // Returns DbContextOptions for AppDbContext using EF Core InMemory provider.
    // If dbName is null, a unique DB name is generated (isolated per call/test).
    public static DbContextOptions<AppDbContext> CreateOptions(string? dbName = null) =>
        new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: dbName ?? Guid.NewGuid().ToString())
            .Options;

    // Helper to create a new AppDbContext instance.
    public static AppDbContext CreateContext(string? dbName = null)
    {
        var options = CreateOptions(dbName);
        return new AppDbContext(options);
    }
}