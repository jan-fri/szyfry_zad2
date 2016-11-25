using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szyfr_przestawieniowy
{
    class Program
    {
        static char[,] CharMatrix { get; set; }
        static string PlainText { get; set; }
        static StringBuilder CipherText { get; set; }
        static string[] CipherOrder { get; set; }

        static void CreateCharMatrix(int columns)
        {
            double lines;
            int character = 0;
            int characters = PlainText.Length;

            int addNoofSpaces;
            lines = Math.Ceiling((double)characters / columns);

            if (characters % columns != 0)
                addNoofSpaces = columns - characters % columns;

            CharMatrix = new char[(int)lines, columns];
            for (int i = 0; i < (int)lines; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    if (character == characters)
                    {
                        CharMatrix[i, j] = '_';
                    }
                    else
                    {
                        CharMatrix[i, j] = PlainText[character];
                        character++;
                    }
                }
            }
        }

        static void SetColumnOrder(string order)
        {
            char delimiter = '-';
            CipherOrder = order.Split(delimiter);
        }

        static void Cipher()
        {
            CipherText = new StringBuilder();
            foreach (var next in CipherOrder)
            {
                int n = int.Parse(next);
                n--;
                for (int i = 0; i < CharMatrix.GetLength(0); i++)
                {
                    CipherText.Append(CharMatrix[i, n]);
                }
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
            
            
            Console.WriteLine("podaj ilosc kolumn macierzy");
            int columns = int.Parse(Console.ReadLine());
            CreateCharMatrix(columns);

            bool correctOrder;
            do
            {
                Console.WriteLine("podaj kolejnosc kolumn, numery kolumn powinny oddzielone '-', np. 3-1-2");
                SetColumnOrder(Console.ReadLine());
                correctOrder = CipherOrder.Length != CharMatrix.GetLength(1);
                if (correctOrder)
                {
                    Console.WriteLine("ilosc kolumn podana w kolejnosci nie zgadza sie z rozmiarem macierzy");
                }           
            } while (correctOrder);

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
