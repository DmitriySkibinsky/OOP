using DeviceController;
using System;
using System.ComponentModel;
using System.Threading;

namespace MeasuringDevice
{
    public abstract class MeasureDataDevice : IEventEnabledMeasuringDevice, IDisposable
    {
        private Units unitsToUse;
        private int[] dataCaptured;
        private int mostRecentMeasure;
        private DeviceController.DeviceController controller;
        private const DeviceType measurementType = DeviceType.LENGTH;
        private BackgroundWorker dataCollector;
        private BackgroundWorker heartBeatTimer;
        private int heartBeatIntervalTime;

        public event EventHandler NewMeasurementTaken = delegate { };
        public event HeartBeatEventHandler HeartBeat = delegate { };

        public int HeartBeatInterval
        {
            get { return heartBeatIntervalTime; }
            protected set { heartBeatIntervalTime = value; }
        }

        protected virtual void OnNewMeasurementTaken()
        {
            NewMeasurementTaken?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnHeartBeat()
        {
            HeartBeat?.Invoke(this, new HeartBeatEventArgs());
        }

        public MeasureDataDevice(Units units)
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
            StartHeartBeat();
        }

        public void StopCollecting()
        {
            if (dataCollector != null)
            {
                dataCollector.CancelAsync();
            }
        }

        public int[] GetRawData()
        {
            return dataCaptured;
        }

        private void GetMeasurements()
        {
            dataCollector = new BackgroundWorker();
            dataCollector.WorkerSupportsCancellation = true;
            dataCollector.WorkerReportsProgress = true;
            dataCollector.DoWork += dataCollector_DoWork;
            dataCollector.ProgressChanged += dataCollector_ProgressChanged;
            dataCollector.RunWorkerAsync();
        }

        private void dataCollector_DoWork(object sender, DoWorkEventArgs e)
        {
            dataCaptured = new int[10];
            int i = 0;

            while (!dataCollector.CancellationPending && i < 10)
            {
                dataCaptured[i] = controller.TakeMeasurement();
                mostRecentMeasure = dataCaptured[i];

                dataCollector.ReportProgress(0);

                i++;

                // Задержка для уменьшения частоты измерений
                Thread.Sleep(1000); // 1 секунда
            }

            // Остановка сбора данных после 10 измерений
            if (i == 10)
            {
                dataCollector.CancelAsync();
            }
        }

        private void dataCollector_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            OnNewMeasurementTaken();
        }

        private void StartHeartBeat()
        {
            heartBeatTimer = new BackgroundWorker();
            heartBeatTimer.WorkerSupportsCancellation = true;
            heartBeatTimer.WorkerReportsProgress = true;

            heartBeatTimer.DoWork += (o, args) =>
            {
                while (!heartBeatTimer.CancellationPending)
                {
                    Thread.Sleep(HeartBeatInterval);
                    if (disposed) break;
                    heartBeatTimer.ReportProgress(0);
                }
            };

            heartBeatTimer.ProgressChanged += (o, args) =>
            {
                OnHeartBeat();
            };

            heartBeatTimer.RunWorkerAsync();
        }

        private bool disposed = false;

        public void Dispose()
        {
            if (dataCollector != null)
            {
                dataCollector.Dispose();
            }

            if (heartBeatTimer != null)
            {
                heartBeatTimer.Dispose();
            }

            disposed = true;
        }
    }
}