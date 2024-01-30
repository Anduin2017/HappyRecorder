using Aiursoft.CommandFramework;
using Aiursoft.CommandFramework.Models;
using Anduin.HappyRecorder.Calendar.Handlers.Get;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        var result = await _program.TestRunAsync(new[] { "--help" });
        Assert.AreEqual(0, result.ProgramReturn);
    }

    [TestMethod]
    public async Task InvokeVersion()
    {
        var result = await _program.TestRunAsync(new[] { "--version" });
        Assert.AreEqual(0, result.ProgramReturn);
    }

    [TestMethod]
    public async Task InvokeUnknown()
    {
        var result = await _program.TestRunAsync(new[] { "--wtf" });
        Assert.AreEqual(1, result.ProgramReturn);
    }

    [TestMethod]
    public async Task InvokeWithoutArg()
    {
        var result = await _program.TestRunAsync(Array.Empty<string>());
        Assert.AreEqual(1, result.ProgramReturn);
    }
}
