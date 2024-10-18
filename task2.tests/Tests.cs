using Xunit;

namespace task2.tests;

public class Tests
{
    private readonly BinaryStringEvaluator _evaluator;

    public Tests()
    {
        _evaluator = new BinaryStringEvaluator();
    }

    [Theory]
    [InlineData("23423423413123")]
    [InlineData("34256786978")]
    [InlineData("2342342344123413123")]
    [InlineData("234234235676sdfs78413123")]
    public void Not_a_binary_string_results_in_false(string notABinaryString)
    {
        var result = _evaluator.IsGood(notABinaryString);
        Assert.False(result);
    }

    [Theory]
    [InlineData("10101101010100101010101011")]
    [InlineData("010101011101000101010110")]
    [InlineData("00110101010101010110")]
    [InlineData("1101010101010101")]
    [InlineData("010101010101010101")]
    public void Incorrect_binary_string_results_in_false(string binaryString)
    { 
        var result = _evaluator.IsGood(binaryString);
        Assert.False(result);
    }

    [Theory]
    [InlineData("10101010101010101010")]
    [InlineData("11111111110000000000")]
    [InlineData("11001100110011001100")]
    public void Correct_binary_string_results_in_true(string binaryString)
    { 
        var result = _evaluator.IsGood(binaryString);
        Assert.True(result);
    }
}