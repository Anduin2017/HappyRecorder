namespace Anduin.HappyRecorder.PluginFramework.Services;

public class CalendarRenderer
{
    private readonly Database _database;
    private readonly Algorithm _algorithm;

    public CalendarRenderer(
        Database database,
        Algorithm algorithm)
    {
        _database = database;
        _algorithm = algorithm;
    }

    public async Task Render()
    {
        var now = DateTime.Now;
        var firstDayOfMonth = new DateTime(now.Year, now.Month, 1);
        var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

        Console.WriteLine("| {0,-16} | {1,-16} | {2,-16} | {3,-16} | {4,-16} | {5,-16} | {6,-16} |", "Sunday", "Monday",
            "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday");
        Console.WriteLine("|{0}|{1}|{2}|{3}|{4}|{5}|{6}|", new string('-', 18), new string('-', 18),
            new string('-', 18), new string('-', 18), new string('-', 18), new string('-', 18), new string('-', 18));

        var firstWeekDay = firstDayOfMonth.AddDays(-(int)firstDayOfMonth.DayOfWeek);
        var lastWeekDay = lastDayOfMonth.AddDays(6 - (int)lastDayOfMonth.DayOfWeek);
        var allEvents = await _database.GetEvents();

        for (var date = firstWeekDay; date <= lastWeekDay; date = date.AddDays(1))
        {
            var isToday = date.Day == now.Day && date.Month == now.Month && date.Year == now.Year;
            Console.Write(isToday ? "| {0,-3}* " : "| {0,-4} ", date.Day);

            var color = GetOutputColorFromScore(await GetScoreThatDay(date));
            Console.ForegroundColor = color;
            var eventCountsOnThatDay = (allEvents.Where(t => t.HappenTime.Date == date.Date)).ToArray();
            if (eventCountsOnThatDay.Any())
            {
                var hasBefore = eventCountsOnThatDay.Any(t => t.HappenTime.TimeOfDay < DateTime.Now.TimeOfDay);
                var hasAfter = eventCountsOnThatDay.Any(t => t.HappenTime.TimeOfDay >= DateTime.Now.TimeOfDay);
                if (hasBefore && hasAfter)
                    Console.Write(" !{0,-8}! ", $"({await GetScoreThatDay(date):0.00})");
                else if (hasBefore)
                    Console.Write(" !{0,-9} ", $"({await GetScoreThatDay(date):0.00})");
                else if (hasAfter)
                    Console.Write(" {0,-9}! ", $"({await GetScoreThatDay(date):0.00})");
            }
            else
            {
                Console.Write(" {0,-10} ", $"({await GetScoreThatDay(date):0.00})");
            }

            Console.ResetColor();

            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                Console.WriteLine("|");
            }
        }

        Console.WriteLine();
    }

    private async Task<double> GetScoreThatDay(DateTime thatTime)
    {
        var score = await _algorithm.GetPoints(false, 
            thatTime 
            + DateTime.Now.TimeOfDay);
        return score;
    }

    private static ConsoleColor GetOutputColorFromScore(double score)
    {
        return score switch
        {
            // From 0-50, return red to yellow.
            // From 50-100, return yellow to green.
            >= 90 => ConsoleColor.Green,
            >= 80 => ConsoleColor.DarkGreen,
            >= 70 => ConsoleColor.DarkYellow,
            >= 60 => ConsoleColor.Yellow,
            >= 50 => ConsoleColor.Magenta,
            >= 40 => ConsoleColor.DarkMagenta,
            >= 30 => ConsoleColor.DarkRed,
            >= 20 => ConsoleColor.Red,
            >= 10 => ConsoleColor.Gray,
            >= 0 => ConsoleColor.DarkGray,
            _ => throw new InvalidOperationException()
        };
    }
}