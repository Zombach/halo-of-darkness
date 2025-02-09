using FluentAssertions;
using HaloOfDarkness.UnitTests.Abstraction.Impl;
using HaloOfDarkness.UnitTests.Abstraction.SupportedOs;

namespace HaloOfDarkness.Shared.Exceptions.Tests;

public sealed class ExampleSharedTests
{
    [Theory, CustomAutoData]
    public void ExampleTest(string source)
    {
        Console.WriteLine("test output");
        source.Length.Should().BeGreaterThan(0);
    }

    [Fact(Skip = "Причина пропуска")]
    public void ExampleSkipTest()
    {
        Console.WriteLine("test skip");
    }

    [Fact]
    [SupportedOs(SupportedOses.Windows, SupportedOses.Linux)]
    public void ExampleSupportedOsesWindowsAndLinuxTest()
    {
        Console.WriteLine("test supportedOs Windows and Linux");
    }

    [Fact]
    [SupportedOs(SupportedOses.FreeBSD)]
    public void ExampleSupportedOsOnlyFreeBSDTest()
    {
        Console.WriteLine("test supportedOs FreeBSD");
    }

    [Fact(Explicit = true)]
    public void ExplicitlyTest()
    {
        // Этот тест запустится только при явном выборе.
    }
}
