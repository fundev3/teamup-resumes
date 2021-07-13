using System;
using Xunit;
using static test_pipeline.Program;

namespace TestCase
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Rectangulo rectangulo = new Rectangulo();
            var result = rectangulo.CalcularArea(1, 2);
            Assert.Equal(2, result);
        }
    }
}
