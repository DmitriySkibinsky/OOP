using System;
using System.Security.Cryptography;
using System.Text;

class Program
{
    static void Main()
    {
        
        string fullName = "Скибинский, Дмитрий, Константинович"; 

        using (SHA256 sha256 = SHA256.Create())
        {
            string hash = ComputeHash(fullName, sha256);
            Console.WriteLine($"SHA256 хэш: {hash}");
        }

        using (MD5 md5 = MD5.Create())
        {
            string hash = ComputeHash(fullName, md5);
            Console.WriteLine($"MD5 хэш: {hash}");
        }
    }

    static string ComputeHash(string input, HashAlgorithm hashAlgorithm)
    {
        // Преобразуем строку в массив байтов
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);

        // Получаем хэш в виде массива байтов
        byte[] hashBytes = hashAlgorithm.ComputeHash(inputBytes);

        // Преобразуем хэш из массива в строку шестнадцатеричных символов
        return Convert.ToHexString(hashBytes).ToLowerInvariant();
    }
}
