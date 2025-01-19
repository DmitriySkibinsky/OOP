using DeviceController;

namespace MeasuringDevice
{
    public class MeasureMassDevice : MeasureDataDevice
    {
        public MeasureMassDevice(Units units)
        {
            unitsToUse = units;
            measurementType = DeviceType.MASS;
        }

        public override decimal MetricValue()
        {
            if (unitsToUse == Units.Metric)
                return mostRecentMeasure;
            else
                return mostRecentMeasure * 0.4536m;
        }

        public override decimal ImperialValue()
        {
            if (unitsToUse == Units.Imperial)
                return mostRecentMeasure;
            else
                return mostRecentMeasure * 2.2046m;
        }
    }
}