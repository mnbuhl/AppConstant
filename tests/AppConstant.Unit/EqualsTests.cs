using AppConstant.Examples;
using FluentAssertions;

namespace AppConstant.Unit;

public class EqualsTests
{
    public static TheoryData<Number, Number, bool> IntFixture => new TheoryData<Number, Number, bool>
    {
        { Number.One, Number.One, true },
        { Number.Nine, Number.Eight, false },
    };
    
    [Theory]
    [MemberData(nameof(IntFixture))]
    public void AppConstantInt_WithSameIntValue_ShouldBeExpected(Number a, Number b, bool expected)
    {
        bool result = a == b;
        bool result2 = a.Equals(b);
        
        result.Should().Be(expected);
        result2.Should().Be(expected);
    }
    
    public static TheoryData<Role, Role, bool> StringFixture => new TheoryData<Role, Role, bool>
    {
        { Role.Admin, Role.Admin, true },
        { Role.Guest, Role.User, false },
    };
    
    [Theory]
    [MemberData(nameof(StringFixture))]
    public void AppConstant_WithSameStringValue_ShouldBeExpected(Role a, Role b, bool expected)
    {
        bool result = a == b;
        bool result2 = a.Equals(b);
        
        result.Should().Be(expected);
        result2.Should().Be(expected);
    }
    
    public static TheoryData<MediaType.VideoType, MediaType.VideoType, bool> NestedFixture => 
        new TheoryData<MediaType.VideoType, MediaType.VideoType, bool>
    {
        { MediaType.VideoType.Mp4, MediaType.VideoType.Mp4, true },
        { MediaType.VideoType.Mp4, MediaType.VideoType.Flv, false },
    };
    
    [Theory]
    [MemberData(nameof(NestedFixture))]
    public void AppConstant_WithSameNestedValue_ShouldBeExpected(MediaType.VideoType a, MediaType.VideoType b, bool expected)
    {
        bool result = a == b;
        bool result2 = a.Equals(b);
        
        result.Should().Be(expected);
        result2.Should().Be(expected);
    }
    
    public static TheoryData<Letter, Letter, bool> CharFixture => new TheoryData<Letter, Letter, bool>
    {
        { Letter.A, Letter.A, true },
        { Letter.Z, Letter.A, false },
    };
    
    [Theory]
    [MemberData(nameof(CharFixture))]
    public void AppConstantChar_WithSameCharValue_ShouldBeExpected(Letter a, Letter b, bool expected)
    {
        bool result = a == b;
        bool result2 = a.Equals(b);
        
        result.Should().Be(expected);
        result2.Should().Be(expected);
    }
    
    public static TheoryData<Anniversary, Anniversary, bool> DateTimeFixture => new TheoryData<Anniversary, Anniversary, bool>
    {
        { Anniversary.TenYears, Anniversary.TenYears, true },
        { Anniversary.FiveYears, Anniversary.TenYears, false },
    };
    
    [Theory]
    [MemberData(nameof(DateTimeFixture))]
    public void AppConstantDateTime_WithSameDateTimeValue_ShouldBeExpected(Anniversary a, Anniversary b, bool expected)
    {
        bool result = a == b;
        bool result2 = a.Equals(b);
        
        result.Should().Be(expected);
        result2.Should().Be(expected);
    }
}