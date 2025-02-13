﻿using System;
using System.Windows;

namespace Critical_state
{
    public enum CoolantSystemStatus { OK, Check, Fail }
    public enum SuccessFailureResult { Success, Fail }

    public class Switch
    {
        /// <summary>
        /// Utilty object for simulation
        /// </summary>
        private Random rand = new Random();

        /// <summary>
        /// Disconnect from the external power generation systems
        /// </summary>
        /// <returns>Success or Failure</returns>
        /// <exception cref="PowerGeneratorCommsException">Thrown when the physical switch cannot establish a connection to the power generation system</exception>
        public SuccessFailureResult DisconnectPowerGenerator()
        {
            SuccessFailureResult r = SuccessFailureResult.Fail;
            if (rand.Next(1, 10) > 2) r = SuccessFailureResult.Success;
            if (rand.Next(1, 20) > 18) throw new PowerGeneratorCommsException("Network failure accessing Power Generator monitoring system");
            return r;
        }

        /// <summary>
        /// Runs a diagnostic check against the primary coolant system
        /// </summary>
        /// <returns>Coolant System Status (OK, Fail, Check)</returns>
        /// <exception cref="CoolantTemperatureReadException">Thrown when the switch cannot read the temperature from the coolant system</exception>
        /// <exception cref="CoolantPressureReadException">Thrown when the switch cannot read the pressure from the coolant system</exception>
        public CoolantSystemStatus VerifyPrimaryCoolantSystem()
        {
            CoolantSystemStatus c = CoolantSystemStatus.Fail;
            int r = rand.Next(1, 10);
            if (r > 5)
            {
                c = CoolantSystemStatus.OK;
            }
            else if (r > 2)
            {
                c = CoolantSystemStatus.Check;
            }
            if (rand.Next(1, 20) > 18) throw new CoolantTemperatureReadException("Failed to read primary coolant system temperature");
            if (rand.Next(1, 20) > 18) throw new CoolantPressureReadException("Failed to read primary coolant system pressure");
            return c;
        }

        /// <summary>
        /// Runs a diagnostic check against the backup coolant system
        /// </summary>
        /// <returns>Coolant System Status (OK, Fail, Check)</returns>
        /// <exception cref="CoolantTemperatureReadException">Thrown when the switch cannot read the temperature from the coolant system</exception>
        /// <exception cref="CoolantPressureReadException">Thrown when the switch cannot read the pressure from the coolant system</exception>
        public CoolantSystemStatus VerifyBackupCoolantSystem()
        {
            CoolantSystemStatus c = CoolantSystemStatus.Fail;
            int r = rand.Next(1, 10);
            if (r > 5)
            {
                c = CoolantSystemStatus.OK;
            }
            else if (r > 2)
            {
                c = CoolantSystemStatus.Check;
            }
            if (rand.Next(1, 20) > 19) throw new CoolantTemperatureReadException("Failed to read backup coolant system temperature");
            if (rand.Next(1, 20) > 19) throw new CoolantPressureReadException("Failed to read backup coolant system pressure");
            return c;
        }

        /// <summary>
        /// Reads the temperature from the reactor core
        /// </summary>
        /// <returns>Temperature</returns>
        /// <exception cref="CoreTemperatureReadException">Thrown when the switch cannot access the temperature data</exception>
        public double GetCoreTemperature()
        {
            if (rand.Next(1, 20) > 18) throw new CoreTemperatureReadException("Failed to read core reactor system temperature");
            return rand.NextDouble() * 1000;
        }

        /// <summary>
        /// Instructs the reactor to insert the control rods to shut the reactor down
        /// </summary>
        /// <returns>Success or failure</returns>
        /// <exception cref="RodClusterReleaseException">Thrown if the switch device cannot read the rod position</exception>
        public SuccessFailureResult InsertRodCluster()
        {
            SuccessFailureResult r = SuccessFailureResult.Fail;
            if (rand.Next(1, 100) > 5) r = SuccessFailureResult.Success;
            if (rand.Next(1, 10) > 8) throw new RodClusterReleaseException("Sensor failure, cannot verify rod release");
            return r;
        }

        /// <summary>
        /// Reads the radiation level from the reactor core
        /// </summary>
        /// <returns>Temperature</returns>
        /// <exception cref="CoreRadiationLevelReadException">Thrown when the switch cannot access the radiation level data</exception>
        public double GetRadiationLevel()
        {
            if (rand.Next(1, 20) > 18) throw new CoreRadiationLevelReadException("Failed to read core reactor system radiation levels");
            return rand.NextDouble() * 500;
        }

        /// <summary>
        /// Sends a broadcast message to PA system notifying shutdown complete
        /// </summary>
        /// <exception cref="SignallingException">Thrown if the switch cannot connect to the PA system over the network</exception>
        public void SignalShutdownComplete()
        {
            if (rand.Next(1, 20) > 18) throw new SignallingException("Network failure connecting to broadcast systems");
        }
    }

    public class PowerGeneratorCommsException : Exception
    {
        public PowerGeneratorCommsException(string message) : base(message) { }
    }

    public class CoolantSystemException : Exception
    {
        public CoolantSystemException(string message) : base(message) { }
    }

    public class CoolantTemperatureReadException : CoolantSystemException
    {
        public CoolantTemperatureReadException(string message) : base(message) { }
    }

    public class CoolantPressureReadException : CoolantSystemException
    {
        public CoolantPressureReadException(string message) : base(message) { }
    }

    public class CoreTemperatureReadException : Exception
    {
        public CoreTemperatureReadException(string message) : base(message) { }
    }

    public class CoreRadiationLevelReadException : Exception
    {
        public CoreRadiationLevelReadException(string message) : base(message) { }
    }

    public class RodClusterReleaseException : Exception
    {
        public RodClusterReleaseException(string message) : base(message) { }
    }

    public class SignallingException : Exception
    {
        public SignallingException(string message) : base(message) { }
    }

    public partial class MainWindow : Window
    {
        private Switch switch_;

        /// <summary>
        /// Инициализация
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            switch_ = new Switch();
        }

        /// <summary>
        /// Обработчик для кнопки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TxtBox.Text = "";

            // Отключение генератора
            try
            {
                switch_.DisconnectPowerGenerator();
            }
            catch (PowerGeneratorCommsException ex)
            {
                TxtBox.Text += $"Ошибка связи с генератором: {ex.Message}\n";
            }

            // Проверка основной системы охлаждения
            try
            {
                switch_.VerifyPrimaryCoolantSystem();
            }
            catch (CoolantTemperatureReadException ex)
            {
                TxtBox.Text += $"Ошибка чтения температуры основной системы охлаждения: {ex.Message}\n";
            }
            catch (CoolantPressureReadException ex)
            {
                TxtBox.Text += $"Ошибка чтения давления основной системы охлаждения: {ex.Message}\n";
            }

            // Проверка резервной системы охлаждения
            try
            {
                switch_.VerifyBackupCoolantSystem();
            }
            catch (CoolantTemperatureReadException ex)
            {
                TxtBox.Text += $"Ошибка чтения температуры резервной системы охлаждения: {ex.Message}\n";
            }
            catch (CoolantPressureReadException ex)
            {
                TxtBox.Text += $"Ошибка чтения давления резервной системы охлаждения: {ex.Message}\n";
            }

            // Вставка управляющих стержней
            try
            {
                switch_.InsertRodCluster();
            }
            catch (RodClusterReleaseException ex)
            {
                TxtBox.Text += $"Ошибка освобождения стержней: {ex.Message}\n";
            }

            // Завершение отключения
            try
            {
                switch_.SignalShutdownComplete();
            }
            catch (SignallingException ex)
            {
                TxtBox.Text += $"Ошибка завершающего сигнала: {ex.Message}\n";
            }

            TxtBox.Text += "Отключение завершено\n";
        }
    }
}