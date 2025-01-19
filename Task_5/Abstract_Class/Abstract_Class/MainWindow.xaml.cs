using MeasuringDevice;
using System.Windows;

namespace MeasuringDeviceApp
{
    public partial class MainWindow : Window
    {
        private MeasureDataDevice device;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void createInstance_Click(object sender, RoutedEventArgs e)
        {
            if (rbMetric.IsChecked == true)
            {
                if (rbLength.IsChecked == true)
                    device = new MeasureLengthDevice(Units.Metric);
                else if (rbMass.IsChecked == true)
                    device = new MeasureMassDevice(Units.Metric);
            }
            else if (rbImperial.IsChecked == true)
            {
                if (rbLength.IsChecked == true)
                    device = new MeasureLengthDevice(Units.Imperial);
                else if (rbMass.IsChecked == true)
                    device = new MeasureMassDevice(Units.Imperial);
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
                if (rbLength.IsChecked == true)
                    tblength.Text = $"Length Value: {device.MetricValue()}";
                else if (rbMass.IsChecked == true)
                    tbMass.Text = $"Mass Value: {device.MetricValue()}";
            }
        }

        private void getImperialValue_Click(object sender, RoutedEventArgs e)
        {
            if (device != null)
            {
                if (rbLength.IsChecked == true)
                    tblength.Text = $"Length Value: {device.ImperialValue()}";
                else if (rbMass.IsChecked == true)
                    tbMass.Text = $"Mass Value: {device.ImperialValue()}";
            }
        }
    }
}