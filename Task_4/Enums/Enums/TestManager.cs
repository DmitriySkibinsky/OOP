using StressTest;
using System;

namespace Enums
{
    public static class TestManager
    {
        private static Random random = new Random();

        public static TestCaseResult GenerateResult()
        {
            // Simulate a random test result
            TestResult result = random.Next(2) == 0 ? TestResult.Pass : TestResult.Fail;
            string reasonForFailure = result == TestResult.Fail ? "Random failure reason" : "";

            return new TestCaseResult(result, reasonForFailure);
        }
    }
}