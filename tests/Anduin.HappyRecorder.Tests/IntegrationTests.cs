using Aiursoft.CommandFramework;
using Aiursoft.CommandFramework.Models;
using Anduin.HappyRecorder.Calendar.Handlers.Get;

namespace Anduin.HappyRecorder.Tests;

[TestClass]
public class IntegrationTests
{
    private readonly NestedCommandApp _program = new NestedCommandApp()
        .WithFeature(new CalendarHandler())
        .WithGlobalOptions(CommonOptionsProvider.DryRunOption)
        .WithGlobalOptions(CommonOptionsProvider.VerboseOption);

    [TestMethod]
    public async Task InvokeHelp()
    {
        var result = await _program.TestRunAsync(["--help"]);
        Assert.AreEqual(0, result.ProgramReturn);
    }

    [TestMethod]
    public async Task InvokeVersion()
    {
        var result = await _program.TestRunAsync(["--version"]);
        Assert.AreEqual(0, result.ProgramReturn);
    }

    [TestMethod]
    public async Task InvokeUnknown()
    {
        var result = await _program.TestRunAsync(["--wtf"]);
        Assert.AreEqual(1, result.ProgramReturn);
    }

    [TestMethod]
    public async Task InvokeWithoutArg()
    {
        var result = await _program.TestRunAsync([]);
        Assert.AreEqual(1, result.ProgramReturn);
    }
}
