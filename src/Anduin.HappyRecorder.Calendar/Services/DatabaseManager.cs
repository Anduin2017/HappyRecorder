using Microsoft.Extensions.Logging;

namespace Anduin.HappyRecorder.Calendar.Services;

public class DatabaseManager
{
    private readonly ILogger<DatabaseManager> _logger;

    public DatabaseManager(ILogger<DatabaseManager> logger)
    {
        _logger = logger;
    }
    
    public static readonly string AppDirectoryLocation = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
        "HappyRecorder");

    public static readonly string ConfigFileName = "config.txt";
    
    public static readonly string DatabaseFileName = "database.json";

    public static readonly string ConfileFileLocation = Path.Combine(AppDirectoryLocation, ConfigFileName);

    public static readonly string DatabaseFileLocation = Path.Combine(AppDirectoryLocation, DatabaseFileName);

    private Task<string> GetConfigFileContent()
    {
        _logger.LogTrace("Getting config file content...");
        if (!Directory.Exists(AppDirectoryLocation))
        {
            _logger.LogTrace("The directory for config file {AppDirectory} was not found. Creating it...", AppDirectoryLocation);
            Directory.CreateDirectory(AppDirectoryLocation);
        }

        if (!File.Exists(ConfileFileLocation))
        {
            _logger.LogTrace("The file for config {ConfileFileLocation} was not found. Creating it...", ConfileFileLocation);
            File.Create(ConfileFileLocation).Close();
        }

        _logger.LogTrace("Reading config file: {ConfileFileLocation} ...", ConfileFileLocation);
        return File.ReadAllTextAsync(ConfileFileLocation);
    }

    private Task SetConfigFileContent(string newContent)
    {
        _logger.LogTrace("Setting config file content...");
        if (!Directory.Exists(AppDirectoryLocation))
        {
            _logger.LogTrace("The directory for config file {AppDirectory} was not found. Creating it...", AppDirectoryLocation);
            Directory.CreateDirectory(AppDirectoryLocation);
        }

        _logger.LogTrace("Writing config file: {ConfileFileLocation} ...", ConfileFileLocation);
        return File.WriteAllTextAsync(ConfileFileLocation, newContent);
    }

    public async Task<string> GetDbLocation()
    {
        _logger.LogTrace("Getting database location...");
        var databaseLocation = await GetConfigFileContent();

        if (string.IsNullOrWhiteSpace(databaseLocation))
        {
            _logger.LogTrace("The database location from config file was empty. Setting it to default, which is {DatabaseFileLocation}", DatabaseFileLocation);
            await SetConfigFileContent(DatabaseFileLocation);

            _logger.LogTrace("Getting database location...");
            databaseLocation = await GetConfigFileContent();
        }

        try
        {
            if (!File.Exists(databaseLocation))
            {
                _logger.LogTrace("The database file {DatabaseLocation} was not found. Creating it...", databaseLocation);
                File.Create(databaseLocation).Close();
            }
        }
        catch (Exception e)
        {
            _logger.LogTrace(e, "Error when creating database file {DatabaseLocation}", databaseLocation);
        }
        
        return databaseLocation;
    }

    public async Task SetDbLocatgion(string newLocation)
    {
        _logger.LogTrace("Setting database location to {NewLocation}", newLocation);
        var oldDbLocation = await GetDbLocation();

        if (!Directory.Exists(newLocation))
        {
            _logger.LogError("The directory: {NewLocation} was not found!", newLocation);
            throw new DirectoryNotFoundException($"The directory: {newLocation} was not found!");
        }

        var newDatabaseLocation = Path.Combine(newLocation, DatabaseFileName);

        // Ensure the new database file can be created
        if (!File.Exists(newDatabaseLocation))
        {
            _logger.LogTrace("The database file {NewDatabaseLocation} was not found. Creating it...", newDatabaseLocation);
            File.Create(newDatabaseLocation).Close();
        }

        _logger.LogTrace("Setting database location to {NewDatabaseLocation} in config file", newDatabaseLocation);
        await SetConfigFileContent(newDatabaseLocation);

        // Migrate the database to the new location
        if (File.Exists(oldDbLocation))
        {
            _logger.LogTrace("Migrating database from {OldDbLocation} to {NewDatabaseLocation}", oldDbLocation, newDatabaseLocation);
            var oldDb = await File.ReadAllTextAsync(oldDbLocation);
            await File.WriteAllTextAsync(newDatabaseLocation, oldDb);
            
            if (File.Exists(newDatabaseLocation) && File.Exists(oldDbLocation) && oldDbLocation != newDatabaseLocation)
            {
                _logger.LogTrace("Deleting old database file {OldDbLocation}...", oldDbLocation);
                File.Delete(oldDbLocation);
            }
        }
    }
}