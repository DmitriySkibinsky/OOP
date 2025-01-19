using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FileEncryptionApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Кнопка для выбора файла
        private void btnSelectFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                txtFilePath.Text = openFileDialog.FileName;

                // Отображаем информацию о файле
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                string fileDetails = $"File Name: {fileInfo.Name}\n" +
                                     $"File Size: {fileInfo.Length} bytes\n" +
                                     $"Creation Time: {fileInfo.CreationTime}\n" +
                                     $"Last Write Time: {fileInfo.LastWriteTime}";

                MessageBox.Show(fileDetails, "File Information");
            }
        }

        // Кнопка для шифрования
        private void btnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilePath.Text))
            {
                MessageBox.Show("Please select a file first.");
                return;
            }

            string filePath = txtFilePath.Text;

            string encryptionMethod = string.Empty;
            if (cbEncryptionMethod != null && cbEncryptionMethod.SelectedItem != null)
            {
                encryptionMethod = ((ComboBoxItem)cbEncryptionMethod.SelectedItem).Content.ToString();
            }
            else
            {
                MessageBox.Show("Please select an encryption method.");
                return;
            }

            string fileContent = File.ReadAllText(filePath);
            string encryptedContent = Encrypt(fileContent, encryptionMethod);

            txtResult.Text = encryptedContent;
        }

        // Метод для шифрования
        private string Encrypt(string input, string method)
        {
            using (var hashAlgorithm = method == "SHA256" ? (HashAlgorithm)SHA256.Create() : (HashAlgorithm)MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = hashAlgorithm.ComputeHash(inputBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }

        // Кнопка для сохранения в бинарный файл
        private void btnSaveBinary_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtResult.Text))
            {
                MessageBox.Show("No encrypted data to save.");
                return;
            }

            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (var binaryWriter = new BinaryWriter(File.Open(saveFileDialog.FileName, FileMode.Create)))
                {
                    binaryWriter.Write(txtResult.Text);
                }

                MessageBox.Show("Encrypted data saved as binary file.");
            }
        }

        // Кнопка для загрузки из бинарного файла
        private void btnLoadBinary_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Binary files (*.bin)|*.bin|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                using (var binaryReader = new BinaryReader(File.Open(openFileDialog.FileName, FileMode.Open)))
                {
                    string encryptedContent = binaryReader.ReadString();
                    txtResult.Text = encryptedContent;
                }

                MessageBox.Show("Encrypted data loaded from binary file.");
            }
        }

        // Кнопка для сохранения в текстовый файл
        private void btnSaveTxt_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtResult.Text))
            {
                MessageBox.Show("No encrypted data to save.");
                return;
            }

            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                DefaultExt = "txt"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, txtResult.Text);
                MessageBox.Show("Encrypted data saved as text file.");
            }
        }

        // Кнопка для сохранения информации о файле
        private void btnSaveFileInfo_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilePath.Text))
            {
                MessageBox.Show("Please select a file first.");
                return;
            }

            string filePath = txtFilePath.Text;

            FileInfo fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
            {
                MessageBox.Show("The selected file does not exist.");
                return;
            }

            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*",
                DefaultExt = "txt"
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    writer.WriteLine($"File Name: {fileInfo.Name}");
                    writer.WriteLine($"File Path: {fileInfo.FullName}");
                    writer.WriteLine($"File Size: {fileInfo.Length} bytes");
                    writer.WriteLine($"Creation Time: {fileInfo.CreationTime}");
                    writer.WriteLine($"Last Write Time: {fileInfo.LastWriteTime}");
                    writer.WriteLine($"Last Access Time: {fileInfo.LastAccessTime}");
                    writer.WriteLine($"Attributes: {fileInfo.Attributes}");
                }

                MessageBox.Show("File information saved successfully.");
            }
        }

        // Кнопка для загрузки информации о файле
        private void btnLoadFileInfo_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                using (StreamReader reader = new StreamReader(openFileDialog.FileName))
                {
                    string fileInfoContent = reader.ReadToEnd();
                    txtResult.Text = fileInfoContent;
                }

                MessageBox.Show("File information loaded successfully.");
            }
        }
        public dynamic Convert_expliced_out(dynamic input, int choice)
        {
            try
            {
                switch (choice)
                {
                    case 1: // bool
                        return Convert.ToBoolean(input) ? "Успешно" : "Не удалось преобразовать в bool.";
                    case 2: // byte (заменил shyte на byte, так как shyte не является стандартным типом в C#)
                        return Convert.ToByte(input) == input ? "Успешно" : "Не удалось преобразовать в byte.";
                    case 3: // int
                        return Convert.ToInt32(input) == input ? "Успешно" : "Не удалось преобразовать в int.";
                    case 4: // uint
                        return Convert.ToUInt32(input) == input ? "Успешно" : "Не удалось преобразовать в uint.";
                    case 5: // long
                        return Convert.ToInt64(input) == input ? "Успешно" : "Не удалось преобразовать в long.";
                    case 6: // ulong
                        return Convert.ToUInt64(input) == input ? "Успешно" : "Не удалось преобразовать в ulong.";
                    case 7: // float
                        return Convert.ToSingle(input) == input ? "Успешно" : "Не удалось преобразовать в float.";
                    case 8: // double
                        return Convert.ToDouble(input) == input ? "Успешно" : "Не удалось преобразовать в double.";
                    case 9: // decimal
                        return Convert.ToDecimal(input) == input ? "Успешно" : "Не удалось преобразовать в decimal.";
                    case 10: // char
                        return Convert.ToChar(input) == input ? "Успешно" : "Не удалось преобразовать в char.";
                    case 11: // string
                        return input is string ? "Успешно" : "Не удалось преобразовать в string.";
                    case 12: // object
                        return "Успешно"; // всегда успешно
                    case 13: // byte
                        return Convert.ToByte(input) == input ? "Успешно" : "Не удалось преобразовать в byte.";
                    default:
                        return "Неверный выбор.";
                }
            }
            catch
            {
                return "Не удалось преобразовать.";
            }
        }

    }
}