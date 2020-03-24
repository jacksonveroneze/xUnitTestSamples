using System;
using Xunit;

namespace xUnitTestSamples.Basic.Tests
{
    public class CalcTests
    {
        [Fact(DisplayName = "Deve somar corretamente")]
        [Trait("Category", "Calc Test")]
        public void Calc_Sum_DeveSomarCorretamente()
        {
            //Arrange
            Calc calc = new Calc();

            // Act
            int result = calc.Sum(1, 1);

            // Assert
            Assert.Equal(2, result);
        }

        [Theory(DisplayName = "Deve subtrair corretamente")]
        [Trait("Category", "Calc Test")]
        [InlineData(2, 1, 1)]
        [InlineData(10, 9, 1)]
        [InlineData(50, 50, 0)]
        [InlineData(100, 1, 99)]
        public void Calc_Sub_DeveSubtrairCorretamente(int n1, int n2, int valueResult)
        {
            // Arrange
            Calc calc = new Calc();

            // Act
            int result = calc.Sub(n1, n2);

            // Assert
            Assert.Equal(valueResult, result);
        }
    }
}