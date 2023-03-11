using AppConstant.Examples;
using FluentAssertions;

namespace AppConstant.Unit;

public class ComparisonTests
{
    public static TheoryData<Number, Number, bool> GreaterThanFixture => new TheoryData<Number, Number, bool>
    {
        { Number.One, Number.One, false },
        { Number.One, Number.Two, false },
        { Number.Two, Number.One, true },
        { Number.Two, Number.Three, false },
        { Number.Ten, Number.Nine, true },
    };
    
    [Theory]
    [MemberData(nameof(GreaterThanFixture))]
    public void AppConstant_GreaterThan_ShouldBeExpected(Number a, Number b, bool expected)
    {
        bool result = a > b;
        bool result2 = a.CompareTo(b) > 0;
        
        result.Should().Be(expected);
        result2.Should().Be(expected);
    }
    
    public static TheoryData<Anniversary, Anniversary, bool> LessThanFixture => new TheoryData<Anniversary, Anniversary, bool>
    {
        { Anniversary.StartDate, Anniversary.StartDate, false },
        { Anniversary.StartDate, Anniversary.FiveYears, true },
        { Anniversary.FiveYears, Anniversary.StartDate, false },
        { Anniversary.FiveYears, Anniversary.TenYears, true },
        { Anniversary.TenYears, Anniversary.FiveYears, false },
        { Anniversary.TenYears, Anniversary.FifteenYears, true },
        { Anniversary.FifteenYears, Anniversary.TenYears, false },
        { Anniversary.FifteenYears, Anniversary.TwentyYears, true },
        { Anniversary.TwentyYears, Anniversary.FifteenYears, false },
    };

    [Theory]
    [MemberData(nameof(LessThanFixture))]
    public void AppConstant_LessThan_ShouldBeExpected(Anniversary a, Anniversary b, bool expected)
    {
        bool result = a < b;
        bool result2 = a.CompareTo(b) < 0;
        
        result.Should().Be(expected);
        result2.Should().Be(expected);
    }
    
    public static TheoryData<Number, Number, bool> GreaterThanOrEqualFixture => new TheoryData<Number, Number, bool>
    {
        { Number.One, Number.One, true },
        { Number.One, Number.Two, false },
        { Number.Two, Number.One, true },
        { Number.Two, Number.Three, false },
        { Number.Ten, Number.Nine, true },
    };
    
    [Theory]
    [MemberData(nameof(GreaterThanOrEqualFixture))]
    public void AppConstant_GreaterThanOrEqual_ShouldBeExpected(Number a, Number b, bool expected)
    {
        bool result = a >= b;
        bool result2 = a.CompareTo(b) >= 0;
        
        result.Should().Be(expected);
        result2.Should().Be(expected);
    }
    
    public static TheoryData<Anniversary, Anniversary, bool> LessThanOrEqualFixture => new TheoryData<Anniversary, Anniversary, bool>
    {
        { Anniversary.StartDate, Anniversary.StartDate, true },
        { Anniversary.StartDate, Anniversary.FiveYears, true },
        { Anniversary.FiveYears, Anniversary.StartDate, false },
        { Anniversary.FiveYears, Anniversary.TenYears, true },
        { Anniversary.TenYears, Anniversary.FiveYears, false },
        { Anniversary.TenYears, Anniversary.FifteenYears, true },
        { Anniversary.FifteenYears, Anniversary.TenYears, false },
        { Anniversary.FifteenYears, Anniversary.TwentyYears, true },
        { Anniversary.TwentyYears, Anniversary.FifteenYears, false },
    };
    
    [Theory]
    [MemberData(nameof(LessThanOrEqualFixture))]
    public void AppConstant_LessThanOrEqual_ShouldBeExpected(Anniversary a, Anniversary b, bool expected)
    {
        bool result = a <= b;
        bool result2 = a.CompareTo(b) <= 0;
        
        result.Should().Be(expected);
        result2.Should().Be(expected);
    }
}