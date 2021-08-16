using System;
using System.Collections.Generic;
using System.Text;

namespace TaskManager.Services.Helpers
{
    public class PasswordGenerator
    {
        //private const string LOWER_LETTERS = "qwertyuiopasdfghjklzxcvbnm";
        //private const string UPPER_LETTERS = "QWERTYUIOPASDFGHJKLZXCVBNM";
        //private const string DIGITS = "1234567890";
        //private const string SYMBOLS = "!@#$%^&*()";

        //private static readonly Random _random = new Random();
        //public static string GeneratePassword(int size)
        //{
        //    StringBuilder passwordBuilder = new StringBuilder();
        //    for (int i = 0; i < size; i++)
        //    {
        //        char letter;
        //        if (i % 5 == 0) letter = SYMBOLS[_random.Next(0, SYMBOLS.Length)];
        //        else if (i % 4 == 0) letter = DIGITS[_random.Next(0, DIGITS.Length)];
        //        else if (i % 3 == 0) letter = LOWER_LETTERS[_random.Next(0, LOWER_LETTERS.Length)];
        //        else letter = UPPER_LETTERS[_random.Next(0, UPPER_LETTERS.Length)];
        //        passwordBuilder.Append(letter);
        //    }
        //    return passwordBuilder.ToString();
        //}
        public static string GetDefaultPassword()
        {
            return "1qazxsw2"; 
            // bu tebii ki bele olmalidi deyil... en azindan config ve ya user secret uzerinden olmalidi... amma bu hissesini 16.08.2021 seher yazmisam...
            // yani ki, artiq qalan islerimi tamamlayan zaman.. yaqin ozunuz qalanini basa dusduz ))

        }
    }
}
