using StressTest;
using System.Windows.Controls;

namespace Enums
{
    public static class TestRunner
    {
        public static void RunTests(TestCaseResult[] results, ListBox reasonsList, Label successesLabel, Label failuresLabel)
        {
            // Clear the reasonsList list
            reasonsList.Items.Clear();

            // Initialize the results array
            results = new TestCaseResult[10];

            // Fill the results array with data
            for (int i = 0; i < results.Length; i++)
            {
                results[i] = TestManager.GenerateResult();
            }

            // Display the TestCaseResult data
            int passCount = 0;
            int failCount = 0;

            foreach (var result in results)
            {
                if (result.Result == TestResult.Pass)
                {
                    passCount++;
                }
                else if (result.Result == TestResult.Fail)
                {
                    failCount++;
                    reasonsList.Items.Add(result.ReasonForFailure);
                }
            }

            // Display the counts
            successesLabel.Content = $"Successes: {passCount}";
            failuresLabel.Content = $"Failures: {failCount}";
        }
    }
}