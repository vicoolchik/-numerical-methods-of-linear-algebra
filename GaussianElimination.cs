using System;
using System.Collections.Generic;
using System.Text;

namespace чмла_таск1
{
    class GaussianElimination
    {
        double[,] A;
        double[,] T;
        double[] b;
        double[] x;
        int permutation;

        public GaussianElimination()
        {
            try
            {
                permutation = 0;
                AppendMatrix();
                PrintMatrix();
                TempMatrix();
                Console.WriteLine();
                RowEchelonForm();
                PrintMatrix();
                Console.WriteLine();
                ReverseCourse();
                Determinant();
                Console.WriteLine();
                Checking();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void TempMatrix()
        {
            T= new double[A.GetLength(0), A.GetLength(1)];
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
            double[] d= new double[A.GetLength(0)];
            for (int row = 0; row < A.GetLength(0); row++)
            {
                for (int col = 0; col < A.GetLength(0); col++)
                {
                    d[row] += T[row, col] * x[col];
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
            int size= Int32.Parse(Console.ReadLine());
            A = new double[size, size];
            b = new double[size];
            for(int row=0; row<A.GetLength(0); row++)
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

        private void PrintMatrix()
        {
            for (int row = 0; row < A.GetLength(0); row++)
            {
                for (int col = 0; col < A.GetLength(1); col++)
                {
                    Console.Write($"{A[row,col]}\t");
                }
                Console.Write($"| {b[row]}\n");
            }
        }

        private void RowEchelonForm()
        {
            for (int k = 0; k < A.GetLength(0); k++)
            {
                FindMaxInCol(k);
                Console.WriteLine();
                PrintMatrix();
                Console.WriteLine();
                for (int row = k+1; row < A.GetLength(0); row++)
                {
                    double m = -(A[row, k] / A[k, k]);
                    b[row] = b[row] + (m * b[k]);
                    for (int col = 0; col < A.GetLength(1); col++)
                    {
                        A[row, col] = A[row, col] + (m * A[k, col]);
                    }
                }
            }
        }

        private void ReverseCourse()
        {
            x = new double[A.GetLength(0)] ;
            double s = 0;
            for (int row = A.GetLength(0)-1; row >=0; row--)
            {
                for (int col = row+1; col<A.GetLength(1); col++)
                {
                        s += A[row, col] * x[col];                  
                }
                x[row] = (b[row] - s) / A[row, row];
                s = 0;
            }
            for (int row = 0; row < A.GetLength(0); row++)
            {
                Console.WriteLine($"x[{row}] = {x[row]}");
            }
        }

        private void Determinant()
        {
            double determinant = 1;
            for(int i=0; i<A.GetLength(0); i++)
            {
                determinant*= A[i, i];
            }
            determinant *= Math.Pow((-1), permutation);
            Console.WriteLine();
            Console.WriteLine($"Determinant = {determinant} ;");
        }

        private void FindMaxInCol(int k)
        {
            double max = Math.Abs(A[k, k]);
            
            int pos = k;

            for ( int i = k; i<A.GetLength(1); i++)
            {
                if (Math.Abs(A[i, k]) > max) { max = Math.Abs(A[i, k]); pos = i; }
            }
            if (max == 0)
            {
                throw new Exception("\nError : Degenerate matrix!!!\n");
            }

            if (pos != k)
            {
                permutation++;
                double temp;
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    temp = A[k, j];
                    A[k, j] = A[pos, j];
                    A[pos, j] = temp;
                }
                temp = b[k];
                b[k] = b[pos];
                b[pos] = temp;

            }

        }
    }
}
