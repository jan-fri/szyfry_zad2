using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szyfr_homofoniczny
{
    class Program
    {
        static Dictionary<char, int[]> HomofoneTable;
        static string PlainText;
        static StringBuilder CipherText;

        static void CreateHomofoneTable()
        {
            HomofoneTable = new Dictionary<char, int[]>
            {
                {'A',new int[]{01, 35, 28, 59, 82}},
                {'B',new int[]{02, 33, 22}},
                {'C',new int[]{01, 11, 13}},
                {'D',new int[]{48, 58}},
                {'E',new int[]{27, 69, 72, 87}},
                {'F',new int[]{37, 60}},
                {'G',new int[]{06, 71}},
                {'H',new int[]{63, 98, 73}},
                {'I',new int[]{08, 31, 88, 99}},
                {'J',new int[]{29, 70}},
                {'K',new int[]{32, 54, 64, 74}},
                {'L',new int[]{45, 56, 86}},
                {'M',new int[]{04, 62}},
                {'N',new int[]{15, 57}},
                {'O',new int[]{40, 47, 66, 77}},
                {'P',new int[]{27, 79}},
                {'R',new int[]{42, 68, 94}},
                {'S',new int[]{12, 55, 97}},
                {'T',new int[]{22, 50, 67, 92}},
                {'U',new int[]{14, 76, 46}},
                {'W',new int[]{52, 78}},
                {'Y',new int[]{37, 80}},
                {'Z',new int[]{19, 51, 65, 75, 85}},
                {' ', new int[]{23, 41, 36, 93}}
            };
        }

        static void Cipher()
        {
            CreateHomofoneTable();
            CipherText = new StringBuilder();
            Random rnd = new Random();
            foreach (var letter in PlainText.ToUpper())
            {
                List<int> numbers = HomofoneTable[letter].ToList();
                int index = rnd.Next(numbers.Count - 1);
                int cypher = numbers[index];
                CipherText.Append(cypher);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("podaj sciezke do pliku, ktory ma zostac zaszyfrowany");
            var path = Console.ReadLine();
            using (StreamReader sr = new StreamReader(path))
            {
                PlainText = sr.ReadToEnd();
            }

            Cipher();
            Console.WriteLine(CipherText);

            Console.WriteLine("czy zapisac szyfr do pliku? (y/n)");
            var write = Console.ReadLine();
            if (write == "y")
            {
                System.IO.StreamWriter file = new System.IO.StreamWriter(path, true);
                file.WriteLine(Environment.NewLine + "wiadomosc zaszyfrowana: " + Environment.NewLine + CipherText);

                file.Close();
            }
        }
    }
}
