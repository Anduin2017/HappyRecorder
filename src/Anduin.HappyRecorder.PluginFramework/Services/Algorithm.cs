using Microsoft.Extensions.Logging;

namespace Anduin.HappyRecorder.PluginFramework.Services;

public class Algorithm
{
    private readonly ILogger<Algorithm> _logger;
    private readonly Database _database;

    public Algorithm(
        ILogger<Algorithm> logger,
        Database database)
    {
        _logger = logger;
        _database = database;
    }
    
    public async Task<double> GetPoints(bool showHistory, DateTime measureTime)
    {
        var recordsObjects = (await _database.GetEvents()).Where(e => e.HappenTime < measureTime);
        var totalPoints = 100.0;
        var lastCalculatingPoint = DateTime.MinValue;
        foreach (var record in recordsObjects.OrderBy(t => t.HappenTime))
        {
            // Convert the past time as points. Add those.
            var elapsed = record.HappenTime - lastCalculatingPoint;
            var points = GetPointsFromWaitingTime(elapsed);
            totalPoints = AddScore(totalPoints, points);

            // Since this is a triggered event. Count it!
            var pointsLater = WastePoint(totalPoints);
            if (showHistory)
            {
                Console.WriteLine($"Event in DB, at {record.HappenTime}. {totalPoints:N1} -> {pointsLater:N1}");
            }
            else
            {
                _logger.LogTrace("You recorded at {RecordHappenTime}. {TotalPoints} -> {PointsLater}", record.HappenTime, totalPoints.ToString("N1"), pointsLater.ToString("N1"));
            }
            totalPoints = pointsLater;

            // Record last point.
            lastCalculatingPoint = record.HappenTime;
        }

        var finalElapsed = measureTime - lastCalculatingPoint;
        var pointsLast = GetPointsFromWaitingTime(finalElapsed);
        totalPoints = AddScore(totalPoints, pointsLast);

        return totalPoints;
    }
    
    private double GetPointsFromWaitingTime(TimeSpan input)
    {
        var x = input.TotalDays;
        double points;
        if (x > 9)
        {
            points = 100;
        }
        else
        {
            //points = 10.0 * x;
            points = -0.5 * x * x + 15.5 * x;
        }
        return points;
    }
    
    private double WastePoint(double sourceWastedBefore)
    {
        return sourceWastedBefore / 2.0 - 5;
        //return sourceWastedBefore / 2.0;
    }

    private double AddScore(double sourceScore, double scoreToAdd)
    {
        return Math.Min(100, sourceScore + scoreToAdd);
    }
}