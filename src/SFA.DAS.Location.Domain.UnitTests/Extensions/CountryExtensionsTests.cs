using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.Location.Domain.Extensions;
using System;

namespace SFA.DAS.Location.Domain.UnitTests.Extensions;

[TestFixture]
internal class CountryExtensionsTests
{
    [TestCase("E", "England")]
    [TestCase("W", "Wales")]
    [TestCase("S", "Scotland")]
    [TestCase("N", "Northern Ireland")]
    [TestCase("L", "Channel Islands")]
    [TestCase("M", "Isle of Man")]
    [TestCase("J", "Unknown")]
    public void ToCountry_ShouldReturnExpectedCountry(string code, string expected)
    {
        // Act
        var result = code.ToCountry();

        // Assert
        result.Should().Be(expected);
    }

    [Test]
    public void ToCountry_WithUnknownCode_ShouldThrowArgumentOutOfRangeException()
    {
        // Arrange
        const string unknownCode = "XYZ";

        // Act
        Action act = () => unknownCode.ToCountry();

        // Assert
        act.Should()
            .Throw<ArgumentOutOfRangeException>()
            .WithMessage("Unknown country code: XYZ*");
    }

    [Test]
    public void ToCountry_WithNullOrEmpty_ShouldThrowArgumentOutOfRangeException()
    {
        // Act
        Action actNull = () => ((string) null)!.ToCountry();
        Action actEmpty = () => string.Empty.ToCountry();

        // Assert
        actNull.Should().Throw<ArgumentOutOfRangeException>();
        actEmpty.Should().Throw<ArgumentOutOfRangeException>();
    }
}