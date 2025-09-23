using FiftyOne.Common.Wrappers;
using System;

namespace FiftyOne.Common.TestHelpers;

/// <summary>
/// Test implementation of <see cref="IDateTimeWrapper"/> used to artificially 
/// control the time returned to workers and builders for testing.
/// </summary>
public class TestDateTime : IDateTimeWrapper
{
    public DateTime Now => Current.ToLocalTime();

    public DateTime UtcNow => Current;

    public DateTime Today => Current.Date;

    /// <summary>
    /// The current date time that the test service will return.
    /// </summary>
    public DateTime Current { get; private set; }

    /// <summary>
    /// Constructs a new instance of <see cref="TestDateTime"/>.
    /// </summary>
    /// <param name="dateTime"></param>
    public TestDateTime(DateTime dateTime)
    {
        Current = dateTime;
    }

    /// <summary>
    /// Increment the date time returned.
    /// </summary>
    /// <param name="increment"></param>
    public void Increment(TimeSpan increment)
    {
        Current = Current.Add(increment);
    }

    /// <summary>
    /// Explicitly sets the current datetime.
    /// </summary>
    /// <param name="value"></param>
    public void Set(DateTime value)
    {
        Current = value;
    }
}