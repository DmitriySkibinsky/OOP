using System;
using System.Windows;

namespace Critical_state
{
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
            catch (Exception ex)
            {
                TxtBox.Text += $"Ошибка связи с генератором: {ex.Message}\n";
            }

            // Проверка основной системы охлаждения
            try
            {
                switch_.VerifyBackupCoolantSystem();
            }
            catch (CoolantTemperatureReadException ex)
            {
                TxtBox.Text += $"Ошибка чтения температуры: {ex.Message}\n";
            }
            catch (CoolantPressureReadException ex)
            {
                TxtBox.Text += $"Ошибка чтения давления: {ex.Message}\n";
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
            catch (Exception ex)
            {
                TxtBox.Text += $"Ошибка завершающего сигнала: {ex.Message}\n";
            }
        }
    }

    // Класс для управления системой
    public class Switch
    {
        public void DisconnectPowerGenerator()
        {
            // Логика отключения генератора
        }

        public void VerifyBackupCoolantSystem()
        {
            // Пример выбрасывания исключения
            throw new CoolantTemperatureReadException("Температура слишком высокая.");
        }

        public void InsertRodCluster()
        {
            // Логика вставки стержней
            throw new RodClusterReleaseException("Стержни не освобождены.");
        }

        public void SignalShutdownComplete()
        {
            // Логика завершения отключения
        }
    }

    // Исключения
    public class CoolantTemperatureReadException : Exception
    {
        public CoolantTemperatureReadException(string message) : base(message) { }
    }

    public class CoolantPressureReadException : Exception
    {
        public CoolantPressureReadException(string message) : base(message) { }
    }

    public class RodClusterReleaseException : Exception
    {
        public RodClusterReleaseException(string message) : base(message) { }
    }
}
