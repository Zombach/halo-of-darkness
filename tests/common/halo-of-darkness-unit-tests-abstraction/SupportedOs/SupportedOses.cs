namespace HaloOfDarkness.UnitTests.Abstraction.SupportedOs;

// We're using an enum here, because we want the user to be able to pass the value to our attribute,
// and the existing OSPlatform structure is not appropriate for that. Plus, we like the name "macOS"
// better than the name "OSX". :)

public enum SupportedOses
{
    FreeBSD = 1,
    Linux = 2,
    macOS = 3,
    Windows = 4,
}
