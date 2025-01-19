using MeasuringDevice;
using System.Windows;

namespace MeasuringDeviceApp
{
    public partial class MainWindow : Window
    {
        private IMeasuringDevice device;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void createInstance_Click(object sender, RoutedEventArgs e)
        {
            if (rbMetric.IsChecked == true)
            {
                device = new MeasureLengthDevice(Units.Metric);
            }
            else if (rbImperial.IsChecked == true)
            {
                device = new MeasureLengthDevice(Units.Imperial);
            }
        }

        private void startCollecting_Click(object sender, RoutedEventArgs e)
        {
            device?.StartCollecting();
        }

        private void stopCollecting_Click(object sender, RoutedEventArgs e)
        {
            device?.StopCollecting();
        }

        private void getRawData_Click(object sender, RoutedEventArgs e)
        {
            if (device != null)
            {
                int[] rawData = device.GetRawData();
                lbRawData.Items.Clear();
                foreach (var data in rawData)
                {
                    lbRawData.Items.Add(data);
                }
            }
        }

        private void getMetricValue_Click(object sender, RoutedEventArgs e)
        {
            if (device != null)
            {
                tbMetricValue.Text = $"Metric Value: {device.MetricValue()}";
            }
        }

        private void getImperialValue_Click(object sender, RoutedEventArgs e)
        {
            if (device != null)
            {
                tbImperialValue.Text = $"Imperial Value: {device.ImperialValue()}";
            }
        }
    }
}