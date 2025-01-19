using DeviceController;
using System;
using System.Threading;

namespace MeasuringDevice
{
    public abstract class MeasureDataDevice
    {
        protected Units unitsToUse;
        protected int[] dataCaptured;
        protected int mostRecentMeasure;
        protected DeviceController.DeviceController controller;
        protected DeviceType measurementType;

        public abstract decimal MetricValue();
        public abstract decimal ImperialValue();

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