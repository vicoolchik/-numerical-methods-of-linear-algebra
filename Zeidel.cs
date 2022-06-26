using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace чмла_таск
{
    class Zeidel
    {
        double[,] A;
        double[] b;
        double[] x;
        double[] r;
        double[] xZiro;
        double[] tempX;
        double eps = 0;
        int size;

        public Zeidel()
        {
            AppendMatrix();
            size = A.GetLength(0);
            IsMCorrect();
            PrintMatrix(A);

            Solve();

            for (int i = 0; i < A.GetLength(0); i++)
            {
                Console.WriteLine($"x[{i}] = {x[i]}");
            }
            Checking();

        }

        private void Solve()
        {
            x = new double[size];
            xZiro = new double[size];
            tempX = new double[size];
            eps = Math.Pow(10, -6);
            double maxVal = 0;
            int numOfIt = 0;
            do
            {
                MetodZeidela();
                numOfIt += 1;
                for (int i = 0; i < size; i++)
                {
                    tempX[i] = x[i] - xZiro[i];
                    tempX[i] = Math.Abs(tempX[i]);

                }
                maxVal = tempX.Max();
            } while (maxVal >= eps);
            Console.WriteLine($"Number of iteration = {maxVal = numOfIt}\n");
        }

        private void Checking()
        {
            r = new double[A.GetLength(0)];
            for (int row = 0; row < A.GetLength(0); row++)
            {
                for (int col = 0; col < A.GetLength(0); col++)
                {
                    r[row] += (A[row, col] * x[col]);
                }
                r[row] = b[row] - r[row];
                r[row] = Math.Abs( r[row]);
            }
            double maxVal = r.Max();

                Console.WriteLine($"r = {maxVal}");

        }

        private void PrintMatrix(double[,] A)
        {
            for (int row = 0; row < A.GetLength(0); row++)
            {
                for (int col = 0; col < A.GetLength(1); col++)
                {
                    Console.Write($"{A[row, col]}\t");
                }
                Console.Write($"| {b[row]}\n");
            }
        }

        private void AppendMatrix()
        {
            Console.Write("Enter size : ");
            int size = Int32.Parse(Console.ReadLine());
            A = new double[size, size];
            b = new double[size];
            for (int row = 0; row < A.GetLength(0); row++)
            {
                for (int col = 0; col < A.GetLength(1); col++)
                {
                    Console.Write($"A[{row},{col}]: ");
                    A[row, col] = double.Parse(Console.ReadLine());
                }
                Console.Write($"b[{row}]: ");
                b[row] = double.Parse(Console.ReadLine());
                Console.WriteLine();
            }
        }

        private void MetodZeidela()
        {

            for (int i = 0; i < size; i++)
            {
                xZiro[i] = x[i];
            }
            double tempSum = 0;
            for (int i = 0; i < size ; i++)
            {
                for (int j = 0; j < size ; j++)
                {
                    if (j >i)
                    {
                        tempSum += A[i, j] * xZiro[j];
                    }
                    if (j < i)
                    {
                        tempSum += A[i, j] * x[j];
                    }
                }
                x[i] = -(tempSum - b[i]) / A[i, i];
                tempSum = 0;
            }
        }

        private void IsMCorrect()
        {
            double tempSum = 0;

            for (int i = 0; i < size ; i++)
            {
                for (int j = 0; j < size ; j++)
                {
                    tempSum += Math.Abs( A[i, j]);
                }
                if (Math.Abs(A[i, i]) < (Math.Abs(tempSum) - Math.Abs(A[i, i]))) Console.WriteLine("error");
                tempSum = 0;
            }
        }
    }
}
