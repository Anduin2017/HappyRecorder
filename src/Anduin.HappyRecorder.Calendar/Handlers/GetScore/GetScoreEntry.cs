using Microsoft.Extensions.Logging;

namespace Anduin.HappyRecorder.Calendar.Handlers.GetScore;

public class GetScoreEntry
{
    private readonly ILogger<GetScoreEntry> logger;

    public GetScoreEntry(ILogger<GetScoreEntry> logger)
    {
        this.logger = logger;
    }

    public Task OnServiceStartedAsync()
    {
        Console.WriteLine("Hello!");
        logger.LogInformation("Hello world!");
        return Task.CompletedTask;
    }
}
