using AppConstant.Examples;
using FluentAssertions;

namespace AppConstant.Unit;

public class GetTests
{
    public static TheoryData<char, Letter, bool> GetFixture => new TheoryData<char, Letter, bool>
    {
        { 'Z', Letter.Z, true },
        { 'z', Letter.Z, false},
        { 'N', Letter.N, true },
        { '1', Letter.K, false }
    };

    [Theory]
    [MemberData(nameof(GetFixture))]
    public void GetAppConstant_WithValue_ReturnsExpected(char value, Letter letter, bool expected)
    {
        bool result;
        
        try
        {
            result = Letter.Get(value) == letter;
        }
        catch (ArgumentException)
        {
            result = false;
        }
        
        result.Should().Be(expected);
    }
    
    public static TheoryData<char, Letter, bool> TryGetFixture => new TheoryData<char, Letter, bool>
    {
        { 'Z', Letter.Z, true },
        { 'z', Letter.Z, false},
        { 'N', Letter.N, true },
        { '1', Letter.K, false }
    };
    
    [Theory]
    [MemberData(nameof(TryGetFixture))]
    public void TryGetAppConstant_WithValue_ReturnsExpected(char value, Letter letter, bool expected)
    {
        bool result = Letter.TryGetValue(value, out var resultLetter);
        
        result = result && resultLetter == letter;
        
        result.Should().Be(expected);
    }
    
    [Fact]
    public void GetAppConstant_WithInvalidValue_ThrowsArgumentException()
    {
        Action action = () => Letter.Get('1');
        
        action.Should().Throw<ArgumentException>();
    }
    
    [Fact]
    public void TryGetAppConstant_WithInvalidValue_ReturnsFalse()
    {
        bool result = Letter.TryGetValue('1', out var resultLetter);
        
        result.Should().BeFalse();
        resultLetter.Should().BeNull();
    }
    
    [Fact]
    public void GetAppConstant_WithNullValue_ThrowsArgumentNullException()
    {
        Action action = () => Role.Get(null!);
        action.Should().Throw<ArgumentNullException>();
    }
    
    [Fact]
    public void TryGetAppConstant_WithNullValue_ReturnsFalse()
    {
        bool result = Role.TryGetValue(null!, out var resultRole);
        
        result.Should().BeFalse();
        resultRole.Should().BeNull();
    }
    
    [Fact]
    public void GetAppConstant_WithAppConstantValue_ReturnsAppConstant()
    {
        var role = Role.Get(Role.Admin);
        
        role.Should().Be(Role.Admin);
    }
}