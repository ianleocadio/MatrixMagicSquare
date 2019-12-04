using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Matriz
{
    class Program
    {
        static void Main(string[] args)
        {
            
            int[][] s = new int[3][] {
                new int[] { 5,3,4 },
                new int[] { 1,5,8 },
                new int[] { 6,4,2 }
            };

            int[][] s2 = new int[3][] {
                new int[] { 4,9,2 },
                new int[] { 3,5,7 },
                new int[] { 8,1,5 }
            };

            int[][] s3 = new int[3][] {
                new int[] { 4,8,2 },
                new int[] { 4,5,7 },
                new int[] { 6,1,6 }
            };

            Teste2(s);

            //Console.WriteLine($"Eficiência: {Teste(s3)}");
        }

        public static void Sum(int[][] s, int i, int j, out int Vertical, out int Horizontal)
        {
            int tempV = 0, tempH = 0;

            Parallel.Invoke(
                () =>
                {
                    //Horizontal
                       
                    for (int h = 0; h < s.Length; h++)
                    {
                        tempH += s[i][h];
                    }

                },
                () =>
                {
                    for (int v = 0; v < s.Length; v++)
                    {
                        tempV += s[v][j];
                    }
                });

            Horizontal = tempH;
            Vertical = tempV;
        }


        public static void PrintMatriz(int[][] s)
        {

            Console.WriteLine();

            for (int i = 0; i < s.Length; i++)
            {
                for (int j = 0; j < s.Length; j++)
                {
                    Console.Write(s[i][j] + " ");
                }
                Console.WriteLine();
            }
        }


        static Dictionary<int, Tuple<int, int>> validNumbers;

        public static void InitValidNumbers(int[][] s)
        {

            validNumbers = new Dictionary<int, Tuple<int, int>>();

            for (int i = 0; i < s.Length; i++)
            {
                for (int j = 0; j < s.Length; j++)
                {
                    validNumbers.TryAdd(s[i][j], Tuple.Create(i, j));
                }
            }
        }

        public static void Teste2(int[][] s)
        {
            if (validNumbers == null)
                InitValidNumbers(s);


            PrintMatriz(s);

            foreach (var entry in validNumbers.ToList())
            {
                Teste2Recursivo(s, entry);
            }

            PrintMatriz(s);
        }


        public static bool Teste2Recursivo(int[][] s, KeyValuePair<int, Tuple<int, int>> entry)
        {
            // 5 -> 0, 0 V(12) H(12)
            Sum(s, entry.Value.Item1, entry.Value.Item2, out int V, out int H);
            var value = (V > H) ? V : H; //12
            if (value == 15)
                return true;
            var newValue = 15 - value + s[entry.Value.Item1][entry.Value.Item2]; // 8

            // Contém
            if (!validNumbers.TryGetValue(newValue, out Tuple<int, int> keys))
                return false;

            // valor liberado para uso

            if (!Teste2Recursivo(s, new KeyValuePair<int, Tuple<int, int>>(newValue, Tuple.Create(keys.Item1, keys.Item2))))
            {
                // 8 -> 1,2 V(14) H(14)
                Sum(s, keys.Item1, keys.Item2, out int otherV, out int otherH);
                var otherValue = (otherV > otherH) ? otherV : otherH;
                var otherNewValue = 15 - otherValue + s[keys.Item1][keys.Item2];

                if (15 - otherValue < 15 - value)
                {
                    s[entry.Value.Item1][entry.Value.Item2] = newValue;
                    s[keys.Item1][keys.Item2] = otherNewValue;
                    validNumbers.Remove(newValue);
                    validNumbers.TryAdd(otherNewValue, Tuple.Create(keys.Item1, keys.Item2));
                    return true;
                }
                else
                    return false;
            }

            return true;

        }


    }
}
