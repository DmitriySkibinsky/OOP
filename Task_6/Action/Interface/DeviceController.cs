namespace DeviceController
{
    public enum DeviceType
    {
        LENGTH,
        MASS
    }

    public class DeviceController
    {
        public static DeviceController StartDevice(DeviceType type)
        {
            return new DeviceController();
        }

        public void StopDevice()
        {
        }

        public int TakeMeasurement()
        {
            return new Random().Next(1, 100);
        }
    }
}