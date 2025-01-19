using System;

namespace DeviceController
{
    public class DeviceController : IDisposable
    {
        private IControllableDevice device;

        /// <summary>
        /// A factory method to create and start a new instance of a device.
        /// </summary>
        /// <param name="MeasurementType">Specifies which type of device to start. Must be MASS or LENGTH.</param>
        /// <returns>An instance of the DeviceController class with the controlled device in the started state.</returns>
        public static DeviceController StartDevice(DeviceType MeasurementType)
        {
            DeviceController controller = new DeviceController();
            switch (MeasurementType)
            {
                case DeviceType.LENGTH:
                    controller.device = new FabrikamDevices.LengthMeasuringDevice();
                    break;
                case DeviceType.MASS:
                    controller.device = new ContosoDevices.MassMeasuringDevice();
                    break;
            }
            if (controller.device != null)
            {
                controller.device.StartDevice();
            }
            return controller;
        }

        /// <summary>
        /// Stops the controlled device.
        /// </summary>
        public void StopDevice()
        {
            device.StopDevice();
        }

        /// <summary>
        /// Forces the controlled device to record a measurement.
        /// </summary>
        /// <returns>The measurement taken by the device.</returns>
        public int TakeMeasurement()
        {
            return device.GetLatestMeasure();
        }

        /// <summary>
        /// Disposes the device.
        /// </summary>
        public void Dispose()
        {
            device.Dispose();
        }
    }

    public enum DeviceType
    {
        LENGTH,
        MASS
    }

    public interface IControllableDevice : IDisposable
    {
        void StartDevice();
        void StopDevice();
        int GetLatestMeasure();
    }

    namespace FabrikamDevices
    {
        public class LengthMeasuringDevice : IControllableDevice
        {
            private Random random = new Random();

            public void StartDevice() { }
            public void StopDevice() { }
            public int GetLatestMeasure() => random.Next(1, 100);
            public void Dispose() { }
        }
    }

    namespace ContosoDevices
    {
        public class MassMeasuringDevice : IControllableDevice
        {
            private Random random = new Random();

            public void StartDevice() { }
            public void StopDevice() { }
            public int GetLatestMeasure() => random.Next(1, 100);
            public void Dispose() { }
        }
    }
}