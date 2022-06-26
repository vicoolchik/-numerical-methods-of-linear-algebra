using System;
using System.Collections.Generic;
using System.Text;

namespace чмла_таск
{
    class LeftRun
    {
        double[,] A;
        double[] b;
        double[] y;
        double[] a;
        double[] c;
        double[] f;
        double[] ksi;
        double[] eta;
        int size;

        public LeftRun()
        {
            AppendMatrix();
            PrintMatrix();
            size = A.GetLength(0);
            IsMCorrect();
            FinstStep();
            SecondStep();
            IsYCorrect();
        }

        private void AppendMatrix()
        {
            Console.Write("Enter size : ");
            int size = Int32.Parse(Console.ReadLine());
            A = new double[size, size];
            a = new double[size-1];
            b = new double[size-1];
            c = new double[size];
            f = new double[size];
            y = new double[size];
            for (int row = 0; row < A.GetLength(0)-1; row++)
            {
                Console.Write($"a[{row+1}]: ");
                a[row] = double.Parse(Console.ReadLine());
            }
            Console.WriteLine();
            for (int row = 0; row < A.GetLength(0); row++)
            {
                Console.Write($"c[{row}]: ");
                c[row] = double.Parse(Console.ReadLine());
            }
            Console.WriteLine();
            for (int row = 0; row < A.GetLength(0)-1; row++)
            {
                Console.Write($"b[{row}]: ");
                b[row] = double.Parse(Console.ReadLine());
            }
            Console.WriteLine();
            for (int row = 0; row < A.GetLength(0); row++)
            {
                Console.Write($"f[{row}]: ");
                f[row] = double.Parse(Console.ReadLine());   
            }
            Console.WriteLine();
            for (int row = 0; row < A.GetLength(0); row++)
            {
                for (int col = 0; col < A.GetLength(1); col++)
                {
                    if (row == col)
                    {
                        A[row, col] = c[row];
                    }
                    else if (row == col-1)
                    {
                        A[row, col] = b[row];
                    }
                    else if (row-1 == col)
                    {
                        A[row, col] = a[row-1];
                    }
                    else
                    {
                        A[row, col] = 0;
                    }
                }
            }
        }
        private void PrintMatrix()
        {
            for (int row = 0; row < A.GetLength(0); row++)
            {
                for (int col = 0; col < A.GetLength(1); col++)
                {
                    Console.Write($"{A[row, col]}\t");
                }
                Console.Write($"| {f[row]}\n");
            }
        }
        private void FinstStep()
        {
            ksi = new double[size-1];
            eta = new double[size-1];
            ksi[size - 2] = -(a[size - 2] / c[size - 1]);
            eta[size - 2] = (f[size - 1] / c[size - 1]);
            for (int i = A.GetLength(0) - 2; i > 0; i--)
            {
                ksi[i-1] = -(a[i-1] / (c[i] + b[i] * ksi[i ]));
                eta[i-1] = ((f[i] - b[i] * eta[i])/(c[i] + b[i] * ksi[i]));
            }
        }
        private void SecondStep()
        {
            y[0] = (f[0] - b[0] * eta[0]) / (c[0] + b[0] * ksi[0]);
            for(int i=1; i< A.GetLength(0); i++)
            {
                y[i] = ksi[i-1] * y[i - 1] + eta[i-1];
            }
            Console.WriteLine();
            for (int i = 0; i < A.GetLength(0); i++)
            {
                Console.WriteLine($"y[{i}] = {y[i]}");
            }
        }

        private void IsYCorrect()
        {
            if (Math.Round(c[0] * y[0] + b[0] * y[1], 5) != f[0]) Console.WriteLine("erroe");
            if (Math.Round(c[size-1] * y[size-1] + a[size-2] * y[size-2], 5) != f[size-1]) Console.WriteLine("erroe");
            for (int i=1; i<size-1; i++)
            {
                if (Math.Round(a[i-1] * y[i - 1]+c[i] * y[i] + b[i] * y[i+1], 5) != f[i]) Console.WriteLine("erroe");
            }
        }

        private void IsMCorrect()
        {
            if(Math.Abs(c[0])< Math.Abs(b[0])) Console.WriteLine("erroe");
            if(Math.Abs(c[size-1])< Math.Abs(a[size-2])) Console.WriteLine("erroe");
            for (int i = 1; i < size - 2; i++)
            {
                if (Math.Abs(c[i]) < Math.Abs(a[i])+ Math.Abs(b[i])) Console.WriteLine("erroe");
            }
        }
    }
}
