using System;
using System.Diagnostics;
using System.Linq;

namespace MatrixMult
{
    class Program
    {
        static void Main(string[] args)
        {
            int m, n, k, count;

            Console.Write("Enter matrixes' size (M, N, K) where the first one is M x N size and the second one is N x K size \r\n>");
            var lineSize = Console.ReadLine();

            int[] numbers = lineSize?.Split(',')?.Where(s => string.IsNullOrWhiteSpace(s) == false)?.Select(s => int.Parse(s.Trim()))?.ToArray();
            if (numbers == null || numbers.Length != 3)
            {
                Console.WriteLine("Incorrect parameters count");
                Console.ReadKey();
                return;
            }
            m = numbers[0];
            n = numbers[1];
            k = numbers[2];
            if (m <= 0 || n <= 0 || k <= 0)
            {
                Console.WriteLine("Incorrect parameters");
                Console.ReadKey();
                return;
            }
            Console.Write("Enter iteration quantity \r\n>");
            var lineCount = Console.ReadLine();
            count = int.Parse(lineCount);

            var statistics = new long[count];
            var sw = new Stopwatch();

            for (int i = 0; i < count; i++)
            {
                var left = GenerateMatrix(m, n);
                var right = GenerateMatrix(n, k);

                sw.Start();
                Multiply(left, right);
                sw.Stop();
                statistics[i] = sw.ElapsedMilliseconds;
                sw.Reset();
            }
            var avg = statistics.Sum() / count;

            Console.WriteLine($"Average time: {avg} ms");
            Console.WriteLine($"Capacity: {2.0d * m * n * k / avg}");

            Console.ReadKey();
        }


        private static double[,] GenerateMatrix(int m, int n) {
            var matrix = new double[m,n];
            var rnd = new Random((int)Environment.TickCount);
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    matrix[i, j] = rnd.NextDouble();
                }
            }
            return matrix;
        }

        private static void Multiply(double[,] matrix_1, double[,] matrix_2)
        {
            var resultMatrix = new double[matrix_1.GetLength(0), matrix_2.GetLength(1)];
            for (int i = 0; i < resultMatrix.GetLength(0); i++)
            {
                for (int j = 0; j < resultMatrix.GetLength(1); j++)
                {
                    resultMatrix[i, j] = 0;
                    for (int k = 0; k < matrix_1.GetLength(0); k++)
                    {
                        resultMatrix[i, j] += matrix_1[i, k] * matrix_2[k, j];
                    }
                }
            }
        }
    }
}
