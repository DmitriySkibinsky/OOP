namespace MeasuringDevice
{
    public interface IMeasuringDevice
    {
        decimal MetricValue();
        decimal ImperialValue();
        void StartCollecting();
        void StopCollecting();
        int[] GetRawData();
    }
}

