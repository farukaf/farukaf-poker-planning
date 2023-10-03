using static PokerPlanning.Extensions.StringExtension;

namespace PokerPlanning.Test
{
    public class StringExtensionTest
    {
        [Theory]
        [InlineData("1", "1")]
        [InlineData("1a", "1")]
        [InlineData("a1", "1")]
        [InlineData("", "")]
        [InlineData("☕", "")]
        public void Test1(string input, string result)
        {
            // Arrange
            // Act
            var output = input.ToDigitsOnly();
            // Assert
            Assert.Equal(result, output);
        }
    }
}