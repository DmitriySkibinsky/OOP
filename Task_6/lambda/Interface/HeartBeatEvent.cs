using System;

namespace MeasuringDevice
{
    public class HeartBeatEventArgs : EventArgs
    {
        public DateTime TimeStamp { get; }

        public HeartBeatEventArgs()
        {
            TimeStamp = DateTime.Now;
        }
    }

    public delegate void HeartBeatEventHandler(object sender, HeartBeatEventArgs args);
}