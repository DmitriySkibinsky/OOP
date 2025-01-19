using DeviceController;
using System;
using System.ComponentModel;
using System.IO;
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
        private StreamWriter loggingFileWriter;

        public event EventHandler NewMeasurementTaken;

        public string LoggingFileName { get; set; }

        protected virtual void OnNewMeasurementTaken()
        {
            NewMeasurementTaken?.Invoke(this, EventArgs.Empty);
        }

        public MeasureDataDevice(Units units)
        {
            unitsToUse = units;
            LoggingFileName = "measurements.log"; 
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

            // Инициализация StreamWriter для логирования
            if (!string.IsNullOrEmpty(LoggingFileName))
            {
                loggingFileWriter = new StreamWriter(LoggingFileName, true);
            }

            GetMeasurements();
        }

        public void StopCollecting()
        {
            if (dataCollector != null)
            {
                dataCollector.CancelAsync();
            }

            // Закрытие файла лога
            if (loggingFileWriter != null)
            {
                loggingFileWriter.Close();
                loggingFileWriter = null;
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

                // Логирование измерения
                if (loggingFileWriter != null)
                {
                    loggingFileWriter.WriteLine($"Measurement - {mostRecentMeasure}");
                }

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

        public void Dispose()
        {
            if (dataCollector != null)
            {
                dataCollector.Dispose();
            }

            // Освобождение ресурсов StreamWriter
            if (loggingFileWriter != null)
            {
                loggingFileWriter.Close();
                loggingFileWriter.Dispose();
                loggingFileWriter = null;
            }
        }
    }
}