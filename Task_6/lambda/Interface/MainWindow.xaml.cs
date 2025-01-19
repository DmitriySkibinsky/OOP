using MeasuringDevice;
using System.Windows;

namespace MeasuringDeviceApp
{
    public partial class MainWindow : Window
    {
        private IEventEnabledMeasuringDevice device;
        private EventHandler newMeasurementTaken;

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
            if (device != null)
            {
                newMeasurementTaken = new EventHandler(device_NewMeasurementTaken);
                device.NewMeasurementTaken += newMeasurementTaken;
                device.HeartBeat += (o, args) =>
                {
                    heartBeatTimeStamp.Content = $"HeartBeat Timestamp: {args.TimeStamp}";
                };
                device.StartCollecting();
            }
        }

        private void stopCollecting_Click(object sender, RoutedEventArgs e)
        {
            if (device != null)
            {
                device.NewMeasurementTaken -= newMeasurementTaken;
                device.StopCollecting();
            }
        }

        private void getRawData_Click(object sender, RoutedEventArgs e)
        {
            if (device != null)
            {
                lbRawData.ItemsSource = device.GetRawData();
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

        private void device_NewMeasurementTaken(object sender, EventArgs e)
        {
            if (device != null)
            {
                tbMetricValue.Text = $"Metric Value: {device.MetricValue()}";
                tbImperialValue.Text = $"Imperial Value: {device.ImperialValue()}";
                lbRawData.ItemsSource = null;
                lbRawData.ItemsSource = device.GetRawData();
            }
        }
    }
}