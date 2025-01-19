using StressTest;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Enums
{
    public partial class MainWindow : Window
    {
        // Declare a TestCaseResult array
        private TestCaseResult[] results;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MaterialList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Result.Content = ""; // Очистить перед добавлением нового текста

            if (MaterialList.SelectedItem is ListBoxItem materialItem)
            {
                Material SelMat = (Material)Enum.Parse(typeof(Material), materialItem.Content.ToString());
                Result.Content += $"{SelMat} ";
            }

            if (CrossList.SelectedItem is ListBoxItem crossSectionItem)
            {
                CrossSection SelCroSect = (CrossSection)Enum.Parse(typeof(CrossSection), crossSectionItem.Content.ToString());
                Result.Content += $"{SelCroSect} ";
            }

            if (TestList.SelectedItem is ListBoxItem testResultItem)
            {
                TestResult SelTes = (TestResult)Enum.Parse(typeof(TestResult), testResultItem.Content.ToString());
                Result.Content += $"{SelTes} ";
            }
        }

        private void RunTests_Click(object sender, RoutedEventArgs e)
        {
            TestRunner.RunTests
                (results, reasonsList, successesLabel, failuresLabel);
        }
    }
}