namespace MeasuringDevice
{
    public interface IEventEnabledMeasuringDevice : IMeasuringDevice
    {
        event EventHandler NewMeasurementTaken;
        event HeartBeatEventHandler HeartBeat;
        int HeartBeatInterval { get; }
    }
}