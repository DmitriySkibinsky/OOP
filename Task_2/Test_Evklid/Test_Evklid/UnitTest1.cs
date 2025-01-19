
using GCDApp;

namespace GCDApp.Tests

{
    public class Tests
    {
        [SetUp]
        public void Setup() { }
        
        [Test]
        public void TestFindGCD_TwoNumbers()
        {
            // Arrange
            int a = 10;
            int b = 15;
            int expectedGCD = 5;

            // Act
            int actualGCD = GCDAlg.FindGCD(a, b);

            // Assert
            Assert.AreEqual(expectedGCD, actualGCD);
        }

        [Test]
        public void TestFindGCD_ThreeNumbers()
        {
            // Arrange
            int a = 10;
            int b = 15;
            int c = 25;
            int expectedGCD = 5;

            // Act
            int gcdAB = GCDAlg.FindGCD(a, b);
            int actualGCD = GCDAlg.FindGCD(gcdAB, c);

            // Assert
            Assert.AreEqual(expectedGCD, actualGCD);
        }

        [Test]
        public void TestFindGCD_AllZeroes()
        {
            // Arrange
            int a = 0;
            int b = 0;
            int expectedGCD = 0;

            // Act
            int actualGCD = GCDAlg.FindGCD(a, b);

            // Assert
            Assert.AreEqual(expectedGCD, actualGCD);
        }

        [Test]
        public void TestFindGCD_OneZero()
        {
            // Arrange
            int a = 10;
            int b = 0;
            int expectedGCD = 10;

            // Act
            int actualGCD = GCDAlg.FindGCD(a, b);

            // Assert
            Assert.AreEqual(expectedGCD, actualGCD);
        }

        [Test]
        public void TestFindGCD_PrimeNumbers()
        {
            // Arrange
            int a = 17;
            int b = 19;
            int expectedGCD = 1;

            // Act
            int actualGCD = GCDAlg.FindGCD(a, b);

            // Assert
            Assert.AreEqual(expectedGCD, actualGCD);
        }

        [Test]
        public void TestFindGCD_LargeNumbers()
        {
            // Arrange
            int a = 123456;
            int b = 789012;
            int expectedGCD = 12;

            // Act
            int actualGCD = GCDAlg.FindGCD(a, b);

            // Assert
            Assert.AreEqual(expectedGCD, actualGCD);
        }
    }
}