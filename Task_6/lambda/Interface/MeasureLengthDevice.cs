using DeviceController;
using System;
using System.Threading;

namespace MeasuringDevice
{
    public class MeasureLengthDevice : MeasureDataDevice
    {
        public MeasureLengthDevice(Units units, int heartBeatInterval) : base(units)
        {
            HeartBeatInterval = heartBeatInterval;
        }

        public MeasureLengthDevice(Units units) : this(units, 1000)
        {
        }
    }
}