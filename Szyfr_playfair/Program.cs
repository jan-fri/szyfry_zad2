using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szyfr_playfair
{
    class Program
    {
        static char[,] PlayfairMatrix { get; set; }
        static string PlainText { get; set; }
        static StringBuilder CipherText { get; set; }

        static void CreateCharMatrix()
        {
            PlayfairMatrix = new char[,]
            {
               {'H', 'A', 'R', 'P', 'S'},
               {'I', 'C', 'O', 'D', 'B'},
               {'E', 'F', 'G', 'K', 'L'},
               {'M', 'N', 'Q', 'T', 'U'},
               {'V', 'W', 'X', 'Y', 'Z'}
            };
        }

        static void Cipher()
        {
            CreateCharMatrix();
            StringBuilder twoCharString = new StringBuilder();
            CipherText = new StringBuilder();

            int j = 1;
            foreach (var character in PlainText)
            {
                if (twoCharString.Length != 0 && j == 3)
                {
                    twoCharString.Clear();
                    j = 1;
                }

                twoCharString.Append(character);
                if (twoCharString.Length == 2)
                {
                    CipherText.Append(ChangeChars(twoCharString));
                }
                j++;
            }
        }

        static string ChangeChars(StringBuilder text)
        {
            StringBuilder cypheredText = new StringBuilder();
            string t = text.ToString().ToUpper();
            List<int> x = new List<int>();
            List<int> y = new List<int>();
            char newChar1 = ' ';
            char newChar2 = ' ';

            if (t.Contains("J"))
                t = t.Replace("J", "I");            

            foreach (var character in t)
            {
                for (int i = 0; i < PlayfairMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < PlayfairMatrix.GetLength(1); j++)
                    {
                        if (character == PlayfairMatrix[i, j])
                        {
                            x.Add(i);
                            y.Add(j);
                        }
                    }
                }
            }
            

            if (x[0] == x[1]) // jezeli znaki znajduja sie w tym samym wierszu 
            {
                if (y[0] + 1 != y[1] || y[1] + 1 != y[0]) //jezeli znaki nie sasiaduja ze soba
                {
                    if (y[0] == PlayfairMatrix.GetLength(1) - 1) //jezeli znak m1 jest w ostatniej kolumnie
                    {
                        if (y[1] != 0) //jezeli m2 nie znajduje sie w pierwszej kolumnie
                        {
                            newChar1 = PlayfairMatrix[x[0], 0];
                            newChar2 = PlayfairMatrix[x[0], y[1] + 1];
                        }
                        else if (y[1] == 0) //jezeli znak m2  znajduje sie w pierwszej kolumnie
                        {
                            newChar1 = PlayfairMatrix[x[0], 2];
                            newChar2 = PlayfairMatrix[x[0], 1];
                        }
                    }
                    else if (y[1] == PlayfairMatrix.GetLength(1) - 1)//jezeli znak m2 jest w ostatniej kolumnie
                    {
                        if (y[0] != 0) //jezeli znak m1 nie znajduje sie w pierwszej kolumnie
                        {
                            newChar1 = PlayfairMatrix[x[0], y[0] + 1];
                            newChar2 = PlayfairMatrix[x[0], 0];
                        }
                        else if (y[0] == 0) //jezeli znak m1 znajduje sie w pierwszej kolumnie
                        {
                            newChar1 = PlayfairMatrix[x[0], 1];
                            newChar2 = PlayfairMatrix[x[0], 2];
                        }
                    }
                    else //jezeli znaki nie sasiaduja ze soba i zaden znak nie znajduje sie w ostatniej kolumnie
                    {
                        newChar1 = PlayfairMatrix[x[0], y[0] + 1];
                        newChar2 = PlayfairMatrix[x[0], y[1] + 1];
                    }
                }
                else if (y[0] + 1 == y[1] || y[1] + 1 == y[0])  // jezeli znaki sasiaduja ze soba w tym samym wierszu 
                {
                    if (y[0] == PlayfairMatrix.GetLength(1) - 1) //jezeli znak m1 jest w ostatniej kolumnie
                    {
                        newChar1 = PlayfairMatrix[x[0], 1];
                        newChar2 = PlayfairMatrix[x[0], 0];
                    }
                    else if (y[1] == PlayfairMatrix.GetLength(1) - 1) //jezeli znak m2 jest w ostatniej kolumnie
                    {
                        newChar1 = PlayfairMatrix[x[0], 0];
                        newChar2 = PlayfairMatrix[x[0], 1];
                    }
                    else //jezeli znaki sasiaduja ze soba ale zaden znak nie znajduje sie w ostatniej kolumnie
                    {
                        if (y[0] == PlayfairMatrix.GetLength(1) - 2) // jezeli znak m1 znajduje sie w przedostatniej kolumnie
                        {
                            newChar1 = PlayfairMatrix[x[0], 0];
                            newChar2 = PlayfairMatrix[x[0], y[1] + 2];
                        }
                        else if (y[1] == PlayfairMatrix.GetLength(1) - 2)// jezeli znak m2 znajduje sie w przedostatniej kolumnie
                        {
                            newChar1 = PlayfairMatrix[x[0], y[0] + 2];
                            newChar2 = PlayfairMatrix[x[0], 0];
                        }
                        else
                        {
                            newChar1 = PlayfairMatrix[x[0], y[0] + 2];
                            newChar2 = PlayfairMatrix[x[0], y[1] + 2];
                        }
                    }
                }
            }

            if (y[0] == y[1]) // jezeli znaki znajduja sie w tej samej kolumnie
            {
                if (x[0] + 1 != x[1] || x[1] + 1 != x[0]) //jezeli znaki nie sasiaduja ze soba
                {
                    if (x[0] == PlayfairMatrix.GetLength(0) - 1) // jezeli znak m1 znajduje sie w ostatnim wierszu
                    {
                        if (x[1] != 0) //jezeli znak m2 nie znajduje sie w pierwszym wierszu
                        {
                            newChar1 = PlayfairMatrix[0, y[0]];
                            newChar2 = PlayfairMatrix[x[1] + 1, y[0]];
                        }
                        else if (x[1] == 0) //jezeli znak m2 znajduje sie w pierwszym wierszu
                        {
                            newChar1 = PlayfairMatrix[2, y[0]];
                            newChar2 = PlayfairMatrix[1, y[0]];
                        }
                    }
                    else if (x[1] == PlayfairMatrix.GetLength(0) - 1) // jezeli znak m2 znajduje sie w ostatnim wierszu
                    {
                        if (x[0] != 0) //jezeli znak m1 nie znajduje sie w pierwszym wierszu
                        {
                            newChar1 = PlayfairMatrix[x[0] + 1, y[0]];
                            newChar2 = PlayfairMatrix[0, y[0]];
                        }
                        else if (x[0] == 0) //jezeli znak m1 znajduje sie w pierwszym wierszu
                        {
                            newChar1 = PlayfairMatrix[1, y[0]];
                            newChar2 = PlayfairMatrix[2, y[0]];
                        }
                    }
                    else //jezeli znaki nie sasiaduja ze soba i zaden nie znajduje sie w ostatnim wierszu
                    {
                        newChar1 = PlayfairMatrix[x[0] + 1, y[0]];
                        newChar2 = PlayfairMatrix[x[1] + 1, y[0]];
                    }
                }
                else if (x[0] + 1 == x[1] || x[1] + 1 == x[0]) //jezeli znaki sasiaduja ze soba 
                {
                    if (x[0] == PlayfairMatrix.GetLength(0) - 1) //jezeli znak m1 znajduje sie w ostatnim wierszu
                    {
                        newChar1 = PlayfairMatrix[1, y[0]];
                        newChar2 = PlayfairMatrix[0, y[0]];
                    }
                    else if (x[1] == PlayfairMatrix.GetLength(0) - 1) //jezeli znak m2 znajduje sie w ostatnim wierszu
                    {
                        newChar1 = PlayfairMatrix[0, y[0]];
                        newChar2 = PlayfairMatrix[1, y[0]];
                    }
                    else if (x[0] == PlayfairMatrix.GetLength(0) - 2) //jezeli znak m1 znajduje sie w przedostatnim wierszu
                    {
                        newChar1 = PlayfairMatrix[0, y[0]];
                        newChar2 = PlayfairMatrix[x[1] + 2, y[0]];
                    }
                    else if (x[0] == PlayfairMatrix.GetLength(0) - 2) //jezeli znak m2 znajduje sie w przedostatnim wierszu
                    {
                        newChar1 = PlayfairMatrix[x[0] + 2, y[0]];
                        newChar2 = PlayfairMatrix[0, y[0]];
                    }
                    else
                    {
                        newChar1 = PlayfairMatrix[x[0] + 2, y[0]];
                        newChar2 = PlayfairMatrix[x[1] + 2, y[0]];
                    }
                }
            }

            if (x[0] != x[1] && y[0] != y[1]) // jezeli znaki nie znajduja sie w tym samym wierszy lub kilumnie
            {
                newChar1 = PlayfairMatrix[x[0], y[1]];
                newChar2 = PlayfairMatrix[x[1], y[0]];
            }

            return cypheredText.Append(newChar1).Append(newChar2).ToString();

        }
        static void Main(string[] args)
        {
            Console.WriteLine("podaj sciezke do pliku, ktory ma zostac zaszyfrowany");
            var path = Console.ReadLine();
            using (StreamReader sr = new StreamReader(path))
            {
                var text = sr.ReadToEnd();
                text = text.Trim(Environment.NewLine.ToCharArray());
                text = text.Replace(" ", string.Empty);

                StringBuilder tempText = new StringBuilder(text);

                if (tempText.Length % 2 == 1)
                    tempText.Append('v');

                PlainText = tempText.ToString();
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
