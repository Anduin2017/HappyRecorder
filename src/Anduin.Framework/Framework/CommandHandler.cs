using System.CommandLine;

namespace Anduin.Framework.Framework;

public abstract class CommandHandler
{
    public abstract string Name { get; }
    public abstract string Description { get; }

    public virtual string[] Alias => Array.Empty<string>();

    public virtual CommandHandler[] GetSubCommandHandlers() => Array.Empty<CommandHandler>();

    public virtual Option[] GetOptions() => Array.Empty<Option>();

    public Command BuildAsCommand()
    {
        var command = new Command(Name, Description);
        foreach (var alias in Alias)
        {
            command.AddAlias(alias);
        }

        foreach (var option in GetOptions())
        {
            command.AddOption(option);
        }

        foreach (var subcommand in  GetSubCommandHandlers())
        {
            command.AddCommand(subcommand.BuildAsCommand());
        }

        OnCommandBuilt(command);

        return command;
    }

    public virtual void OnCommandBuilt(Command command)
    {
        // Set your own options here:
        // OptionsProvider.PathOptions,
        // OptionsProvider.DryRunOption,
        // OptionsProvider.VerboseOption);

        command.SetHandler(Execute);
    }

    public virtual Task Execute()
    {
        Console.WriteLine($"This is the default command action. Please override the method '{nameof(Execute)}' in class '{this.GetType().Name}'.");
        return Task.CompletedTask;
    }
}
