using Aiursoft.CommandFramework;
using Aiursoft.CommandFramework.Models;
using Anduin.HappyRecorder.Calendar.Handlers.Config;
using Anduin.HappyRecorder.Calendar.Handlers.Get;
using Anduin.HappyRecorder.Calendar.Handlers.Mark;

return await new NestedCommandApp()
    .WithFeature(new GetHandler())
    .WithFeature(new MarkHandler())
    .WithFeature(new ConfigHandler())
    .WithGlobalOptions(CommonOptionsProvider.DryRunOption)
    .WithGlobalOptions(CommonOptionsProvider.VerboseOption)
    .RunAsync(args);
