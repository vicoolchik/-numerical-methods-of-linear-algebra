using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace чмла_таск
{
    class PM
    {
        private const double Eps = 0.001;

        public PM() 
        {
            var a = new double[,]
             { { 1, 2,3},
              { 2,5, 2,},
              {3, 2, 1}};

            var y = new double[] { 1, 1, 1 };

            Console.WriteLine("Matrix A: ");
            PrintMatrix(a);

            Solve(a, y, Eps);
        }
        public static void PrintMatrix(double[,] a)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    Console.Write(a[i, j] + "\t");
                }
                Console.WriteLine();
            }

        }

        public static double[] Solve(double[,] a, double[] y, double eps)
        {
            var size = y.Length;
            double convergence = 0;
            var x = new double[size];
            var x_new = new double[size];
            var lambda = new double[] { 0, 0, 0 };
            var lambda_new = new double[size];

            for (int i = 0; i < size; i++)
            {
                x[i] = y[i] / Norm(y);
            }

            do
            {
                y = ProductMatrixOnVector(a, x);

                for (int j = 0; j < size; j++)
                {
                    x_new[j] = y[j] / Norm(y);
                }

                for (int j = 0; j < size; j++)
                {
                    lambda_new[j] = y[j] / x[j];
                }

                convergence = Math.Abs(AverageValue(lambda_new) - AverageValue(lambda));
                for (int i = 0; i < size; i++)
                {
                    lambda[i] = lambda_new[i];
                    x[i] = x_new[i];
                }


            } while (convergence > eps);

            Console.Write("Eigen Value: ");
            Console.WriteLine(AverageValue(lambda_new));

            Console.WriteLine("Eigen Vector: ");
            Print(x);

            Console.WriteLine(
                Test(a, ProductSkalarOnVector(AverageValue(lambda_new), x), x, eps)
                    ? "Checking is successed"
                    : "Checking is not successed");

            return x;
        }

        public static double AverageValue(double[] lambda)
        {
            double sum = 0;
            for (int i = 0; i < lambda.Length; i++)
            {
                sum += lambda[i];
            }
            return sum / lambda.Length;
        }
        public static double Norm(double[] y)
        {
            for (int i = 0; i < y.Length; i++)
            {
                y[i] = Math.Abs(y[i]);
            }

            return y.Max();
        }

        public static double[] ProductSkalarOnVector(double lambda, double[] x)
        {
            var product = new double[x.Length];
            for (int i = 0; i < x.Length; i++)
            {
                product[i] = x[i] * lambda;
            }
            return product;
        }
        public static double[] ProductMatrixOnVector(double[,] a, double[] xApp)
        {
            var size = a.GetLength(0);
            double[] res = new double[size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    res[i] += a[i, j] * xApp[j];
                }
            }
            return res;
        }
        public static void Print(double[] vector)
        {
            Console.Write("(");
            for (int i = 0; i < vector.Length; i++)
            {
                if (i == vector.Length - 1)
                {
                    Console.Write(vector[i]);
                }
                else
                {
                    Console.Write(vector[i] + "    ");
                }
            }
            Console.WriteLine(")");
            Console.WriteLine();
        }
        public static bool Test(double[,] a, double[] b, double[] x, double? epsilum = null)
        {
            for (int i = 0; i < a.GetLength(0); i++)
            {
                double sumi = 0.0d;
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    sumi += a[i, j] * x[j];
                }

                if (epsilum.HasValue && (sumi - b[i]) > epsilum)
                {
                    return false;
                }
                else if (!epsilum.HasValue && sumi != b[i])
                {
                    return false;
                }
            }
            return true;
        }
    }

}



