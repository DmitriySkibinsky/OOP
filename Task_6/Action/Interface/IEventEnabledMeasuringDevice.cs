namespace MeasuringDevice
{
    public interface IEventEnabledMeasuringDevice : IMeasuringDevice
    {
        event EventHandler NewMeasurementTaken;
    }
}