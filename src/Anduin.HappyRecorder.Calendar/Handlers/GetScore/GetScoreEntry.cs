using Microsoft.Extensions.Logging;

namespace Anduin.HappyRecorder.Calendar.Handlers.GetScore;

public class GetScoreEntry
{
    private readonly ILogger<GetScoreEntry> _logger;

    public GetScoreEntry(ILogger<GetScoreEntry> logger)
    {
        _logger = logger;
    }

    public Task OnServiceStartedAsync(bool shouldTakeAction)
    {
        Console.WriteLine("Hello!");
        _logger.LogInformation("Hello world!");
        return Task.CompletedTask;
    }
}
