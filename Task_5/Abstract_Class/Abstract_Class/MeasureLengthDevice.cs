using DeviceController;

namespace MeasuringDevice
{
    public class MeasureLengthDevice : MeasureDataDevice
    {
        public MeasureLengthDevice(Units units)
        {
            unitsToUse = units;
            measurementType = DeviceType.LENGTH;
        }

        public override decimal MetricValue()
        {
            if (unitsToUse == Units.Metric)
                return mostRecentMeasure;
            else
                return mostRecentMeasure * 25.4m;
        }

        public override decimal ImperialValue()
        {
            if (unitsToUse == Units.Imperial)
                return mostRecentMeasure;
            else
                return mostRecentMeasure * 0.03937m;
        }
    }
}