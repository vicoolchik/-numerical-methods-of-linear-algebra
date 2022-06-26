using System;
using System.Collections.Generic;
using System.Text;

namespace чмла_таск2
{
    class LU_decompose
    {
        double[,] A;
        double[,] U;
        double[,] L;
        double[,] T;
        double[] b;
        double[] x;
        double[] y;

        public LU_decompose()
        {
            try
            {
                AppendMatrix();
                PrintMatrix(A);
                TempMatrix();
                AppendLU();
                Console.WriteLine("Matrix U :");
                PrintMatrix(U);
                Console.WriteLine("Matrix L :");
                PrintMatrix(L);
                Console.WriteLine();
                FindXY();
                Determinant();
                Console.WriteLine();
                Checking();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void AppendLU()
        {
            double temp = 0;
            U = new double[A.GetLength(0), A.GetLength(1)];
            L = new double[A.GetLength(0), A.GetLength(1)];
            for (int i=0; i<A.GetLength(0); i++)
            {
                for (int j = 0; j<i; j++)
                {
                    //if (i == j) { L[i, j] = 1; };
                    for (int k=0; k < j ; k++)
                    {
                        temp += L[i, k] * U[k, j];
                    }
                    L[i, j] = (1 / U[j, j])*(A[i, j] - temp);
                    temp = 0;
                }

                for (int j = i; j < A.GetLength(0); j++)
                {
                    for (int k = 0; k < i ; k++)
                    {
                        temp += L[i, k] * U[k, j];
                    }
                    U[i, j] =  (A[i, j] - temp);
                    temp = 0;
                }
            }
        }

        private void FindXY()
        {
            x = new double[A.GetLength(0)];
            y = new double[A.GetLength(0)];
            double temp = 0;
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int k = 0; k < i; k++)
                {
                    temp += L[i, k] * y[k];
                }
                y[i] = b[i] - temp;
                temp = 0;
            }
            for (int row = 0; row < A.GetLength(0); row++)
            {
                Console.WriteLine($"y[{row}] = {y[row]}");
            }

            for (int i = A.GetLength(0)-1; i >=0; i--)
            {
                for (int k = i+1; k < A.GetLength(0); k++)
                {
                    temp += U[i, k] * x[k];
                }
                x[i] = (1 / U[i, i]) * (y[i] - temp);
                temp = 0;
            }
            for (int row = 0; row < A.GetLength(0); row++)
            {
                Console.WriteLine($"x[{row}] = {x[row]}");
            }
        }

        private void TempMatrix()
        {
            T = new double[A.GetLength(0), A.GetLength(1)];
            for (int row = 0; row < A.GetLength(0); row++)
            {
                for (int col = 0; col < A.GetLength(0); col++)
                {
                    T[row, col] = A[row, col];
                }
            }
        }

        private void Checking()
        {
            double[] d = new double[A.GetLength(0)];
            for (int row = 0; row < A.GetLength(0); row++)
            {
                for (int col = 0; col < A.GetLength(0); col++)
                {
                    d[row] += (T[row, col] * x[col]);
                }
            }
            for (int row = 0; row < A.GetLength(0); row++)
            {
                Console.WriteLine($"b[{row}] = {d[row]}");
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



        private void Determinant()
        {
            double determinant = 1;
            for (int i = 0; i < A.GetLength(0); i++)
            {
                determinant *= U[i, i];
            }
            Console.WriteLine($"Determinant = {determinant} ;");
        }
    }
}
