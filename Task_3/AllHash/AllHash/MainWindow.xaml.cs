using System;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using Org.BouncyCastle.Crypto.Digests;

namespace HashCalculator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void SHA256Button_Click(object sender, RoutedEventArgs e) => OutputTextBox.Text = ComputeHash(InputTextBox.Text, SHA256.Create());
        private void MD5Button_Click(object sender, RoutedEventArgs e) => OutputTextBox.Text = ComputeHash(InputTextBox.Text, MD5.Create());
        private void SHA1Button_Click(object sender, RoutedEventArgs e) => OutputTextBox.Text = ComputeHash(InputTextBox.Text, SHA1.Create());
        private void SHA224Button_Click(object sender, RoutedEventArgs e) => OutputTextBox.Text = ComputeSHA224Hash(InputTextBox.Text);
        private void SHA384Button_Click(object sender, RoutedEventArgs e) => OutputTextBox.Text = ComputeHash(InputTextBox.Text, SHA384.Create());
        private void SHA512Button_Click(object sender, RoutedEventArgs e) => OutputTextBox.Text = ComputeHash(InputTextBox.Text, SHA512.Create());
        private void RIPEMD160Button_Click(object sender, RoutedEventArgs e) => OutputTextBox.Text = ComputeHash(InputTextBox.Text, RIPEMD160.Create());
        private void HMACSHA1Button_Click(object sender, RoutedEventArgs e) => OutputTextBox.Text = ComputeHMAC(InputTextBox.Text, new HMACSHA1(Encoding.UTF8.GetBytes("your-secret-key")));

        private void HMACSHA256Button_Click(object sender, RoutedEventArgs e) => OutputTextBox.Text = ComputeHMAC(InputTextBox.Text, new HMACSHA256(Encoding.UTF8.GetBytes("your-secret-key")));

        private string ComputeHMAC(string input, HMAC hmacAlgorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = hmacAlgorithm.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes).ToLowerInvariant();
        }
        private void CopyToClipboardButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(OutputTextBox.Text))
            {
                Clipboard.SetText(OutputTextBox.Text);
                MessageBox.Show("Hash copied to clipboard!", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private string ComputeHash(string input, HashAlgorithm hashAlgorithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = hashAlgorithm.ComputeHash(inputBytes);
            return Convert.ToHexString(hashBytes).ToLowerInvariant();
        }

        private string ComputeSHA224Hash(string input)
        {
            var sha224Digest = new Sha224Digest();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            sha224Digest.BlockUpdate(inputBytes, 0, inputBytes.Length);

            byte[] result = new byte[sha224Digest.GetDigestSize()];
            sha224Digest.DoFinal(result, 0);

            return BitConverter.ToString(result).Replace("-", "").ToLowerInvariant();
        }

        
    }
}
