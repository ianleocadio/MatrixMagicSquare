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

            Teste2(s3);

             
            //Console.WriteLine($"Eficiência: {Teste(s3)}");
        }

        public static void Sum(int[][] s, int i, int j, out int Vertical, out int Horizontal, out int Diagonal)
        {
            int tempV = 0, tempH = 0, tempD = 0;

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
                },
                () =>
                {
                    if ((i == j))
                    {
                        for (int d = 0; d < s.Length; d++)
                        {
                            tempD += s[d][d];
                        }
                    }
                    else if (i + j == s.Length - 1)
                    {
                        for (int d = 0; d < s.Length; d++)
                        {
                            tempD += s[d][s.Length - 1 - d];
                        }
                    }
                }
                );

            Horizontal = tempH;
            Vertical = tempV;
            Diagonal = tempD;
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



        public static void Teste2(int[][] s)
        {
            PrintMatriz(s);
            var n = s.Length;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == j && j == n - 2) continue;

                    int valor, valorOposto;
                    if (i == j)
                    {
                        valor = s[i][i];
                        valorOposto = s[i + n - 1][i + n - 1];

                        if (valor + valorOposto == 10)
                            continue;

                        if (valor > valorOposto)
                            s[i][i] = 10 - valorOposto;
                        else
                            s[i + n - 1][i + n - 1] = 10 - valor;

                    }
                    else if (i + j == s.Length - 1)
                    {
                        valor = s[i][j];
                        valorOposto = s[i + n - 1][i];

                        if (valor + valorOposto == 10)
                            continue;

                        if (valor > valorOposto)
                            s[i][j] = 10 - valorOposto;
                        else
                            s[i + n - 1][i] = 10 - valor;
                    }
                    else
                    {
                        valor = s[i][j];
                        int iO, jO;
                        if (i > j)
                        {
                            valor = s[i][j];
                            if (j > 0)
                            {
                                iO = i - n + 1;
                                jO = j;
                                valorOposto = s[i - n + 1][j];
                            }
                            else
                            {
                                iO = i;
                                jO = j + n - 1;
                                valorOposto = s[i][j + n - 1];
                            }

                        }
                        else
                        {
                            if (i > 0)
                            {
                                iO = i;
                                jO = j - n + 1;
                                valorOposto = s[i][j - n + 1];
                            }
                            else
                            {
                                iO = i + n - 1;
                                jO = j;
                                valorOposto = s[i + n - 1][j];
                            }
                        }
                        if (valor + valorOposto == 10)
                            continue;

                        if (valor > valorOposto)
                            s[i][j] = 10 - valorOposto;
                        else
                            s[iO][jO] = 10 - valor;
                    }

                    
                }
            }
            PrintMatriz(s);
        }


    }
}
