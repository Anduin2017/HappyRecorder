using System.CommandLine;

namespace Aiursoft.CommandFramework.Models;

public static class CommonOptionsProvider
{
    public static readonly Option<string> PathOptions = new(
        aliases: new[] { "--path", "-p" },
        description: "Path of the videos to be parsed.")
    {
        IsRequired = true
    };

    public static readonly Option<bool> DryRunOption = new(
        aliases: new[] { "--dry-run", "-d" },
        description: "Preview changes without actually making them");

    public static readonly Option<bool> VerboseOption = new(
        aliases: new[] { "--verbose", "-v" },
        description: "Show detailed log");
}
