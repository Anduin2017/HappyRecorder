using System.Text.Json;
using Anduin.HappyRecorder.PluginFramework.Models;

namespace Anduin.HappyRecorder.PluginFramework.Services;

public class Database
{
    private readonly DatabaseManager _databaseManager;
    private List<Event>? _db;

    public Database(DatabaseManager databaseManager) => _databaseManager = databaseManager;

    public async Task<List<Event>> GetEvents()
    {
        if (_db == null)
        {
            var dbFile = await _databaseManager.GetDbLocation();
            var dbContent = await File.ReadAllTextAsync(dbFile);
            if (string.IsNullOrWhiteSpace(dbContent))
            {
                _db = new List<Event>();
            }
            else
            {
                _db = JsonSerializer.Deserialize<List<Event>>(dbContent) ?? new List<Event>();
            }
        }

        return _db;
    }

    public async Task SaveChangesAsync()
    {
        var dbFile = await _databaseManager.GetDbLocation();
        var dbContent = JsonSerializer.Serialize(await GetEvents(), new JsonSerializerOptions
        {
            WriteIndented = true
        });
        await File.WriteAllTextAsync(dbFile, dbContent);
    }
}