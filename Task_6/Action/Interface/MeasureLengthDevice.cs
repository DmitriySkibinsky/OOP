using DeviceController;
using System;
using System.Threading;

namespace MeasuringDevice
{
    public class MeasureLengthDevice : MeasureDataDevice
    {
        public MeasureLengthDevice(Units units) : base(units)
        {
        }
    }
}