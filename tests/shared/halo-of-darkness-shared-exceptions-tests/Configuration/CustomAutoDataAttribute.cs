using AutoFixture;
using AutoFixture.Xunit3;

namespace HaloOfDarkness.Shared.Exceptions.Tests.Configuration;

public sealed class CustomAutoDataAttribute
    : AutoDataAttribute
{
    public CustomAutoDataAttribute()
        : base(() => CreateFixture(new Fixture()))
    {
    }

    public CustomAutoDataAttribute(Func<IFixture> fixtureFactory)
        : base(() => CreateFixture(fixtureFactory()))
    {
    }

    private static IFixture CreateFixture(IFixture fixture)
    {
        fixture.Customize(new CompositeCustomization(new SupportMutableValueTypesCustomization()));

        fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(behavior => fixture.Behaviors.Remove(behavior));

        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        return fixture;
    }
}
