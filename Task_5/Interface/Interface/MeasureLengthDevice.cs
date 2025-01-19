using DeviceController;
using System;
using System.Threading;

namespace MeasuringDevice
{
    public class MeasureLengthDevice : IMeasuringDevice
    {
        private Units unitsToUse;
        private int[] dataCaptured;
        private int mostRecentMeasure;
        private DeviceController.DeviceController controller;
        private const DeviceType measurementType = DeviceType.LENGTH;

        public MeasureLengthDevice(Units units)
        {
            unitsToUse = units;
        }

        public decimal MetricValue()
        {
            if (unitsToUse == Units.Metric)
                return mostRecentMeasure;
            else
                return mostRecentMeasure * 25.4m;
        }

        public decimal ImperialValue()
        {
            if (unitsToUse == Units.Imperial)
                return mostRecentMeasure;
            else
                return mostRecentMeasure * 0.03937m;
        }

        public void StartCollecting()
        {
            controller = DeviceController.DeviceController.StartDevice(measurementType);
            GetMeasurements();
        }

        public void StopCollecting()
        {
            if (controller != null)
            {
                controller.StopDevice();
                controller = null;
            }
        }

        public int[] GetRawData()
        {
            return dataCaptured;
        }

        private void GetMeasurements()
        {
            dataCaptured = new int[10];
            ThreadPool.QueueUserWorkItem((dummy) =>
            {
                int x = 0;
                Random timer = new Random();
                while (controller != null)
                {
                    Thread.Sleep(timer.Next(1000, 5000));
                    dataCaptured[x] = controller != null ? controller.TakeMeasurement() : dataCaptured[x];
                    mostRecentMeasure = dataCaptured[x];
                    x++;
                    if (x == 10)
                    {
                        x = 0;
                    }
                }
            });
        }
    }
}