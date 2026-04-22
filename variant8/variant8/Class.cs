using System;

namespace NumericalMethods.Interpolation
{
    public class LagrangeInterpolation
    {
        private readonly double[] _x;
        private readonly double[] _y;

        /// <summary>
        /// iнiцiалiзацiя класу масивами вузлiв та значень.
        /// </summary>
        public LagrangeInterpolation(double[] x, double[] y)
        {
            if (x.Length != y.Length)
                throw new ArgumentException("Масиви x та y повиннi мати однакову довжину.");

            _x = x;
            _y = y;
        }

        /// <summary>
        /// Обчислює значення полiнома в довiльнiй точцi.
        /// </summary>
        public double Solve(double targetX)
        {
            double result = 0;
            int n = _x.Length;

            for (int i = 0; i < n; i++)
            {
                double basicsPol = 1;
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        basicsPol *= (targetX - _x[j]) / (_x[i] - _x[j]);
                    }
                }
                result += basicsPol * _y[i];
            }

            return result;
        }
    }


    public class NewtonInterpolation
    {
        private readonly double[] _x;
        private readonly double[] _y;
        private readonly double[,] _dividedDifferences;
        private readonly int _n;

        public NewtonInterpolation(double[] x, double[] y)
        {
            _x = x;
            _y = y;
            _n = x.Length;
            _dividedDifferences = new double[_n, _n];

            BuildDifferenceTable();
        }

        private void BuildDifferenceTable()
        {
            // Перший стовпець - це значення y
            for (int i = 0; i < _n; i++)
            {
                _dividedDifferences[i, 0] = _y[i];
            }

            // Обчислення роздiлених рiзниць
            for (int j = 1; j < _n; j++)
            {
                for (int i = 0; i < _n - j; i++)
                {
                    _dividedDifferences[i, j] = (_dividedDifferences[i + 1, j - 1] - _dividedDifferences[i, j - 1])
                                               / (_x[i + j] - _x[i]);
                }
            }
        }

        public double Solve(double targetX)
        {
            double result = _dividedDifferences[0, 0];
            double product = 1.0;

            for (int i = 1; i < _n; i++)
            {
                product *= (targetX - _x[i - 1]);
                result += _dividedDifferences[0, i] * product;
            }

            return result;
        }

        public void PrintTable()
        {
            Console.WriteLine("Таблиця роздiлених рiзниць:");
            for (int i = 0; i < _n; i++)
            {
                Console.Write($"{_x[i]:F3} | ");
                for (int j = 0; j < _n - i; j++)
                {
                    Console.Write($"{_dividedDifferences[i, j]:F4}\t");
                }
                Console.WriteLine();
            }
        }
    }

    public class SplineInterpolation
    {
        private readonly double[] _x;
        private readonly double[] _y;

        public SplineInterpolation(double[] x, double[] y)
        {
            _x = x;
            _y = y;
        }

        // Лінійний сплайн S1(x)
        public double Linear(double targetX)
        {
            int i = FindInterval(targetX);
            return _y[i] + (_y[i + 1] - _y[i]) / (_x[i + 1] - _x[i]) * (targetX - _x[i]);
        }

        // Квадратичний сплайн S2(x) (Спрощена модель для 3-х точок навколо targetX)
        public double Quadratic(double targetX)
        {
            int i = FindInterval(targetX);
            // Вибираємо 3 найближчі точки
            int start = Math.Max(0, Math.Min(i, _x.Length - 3));

            double x0 = _x[start], x1 = _x[start + 1], x2 = _x[start + 2];
            double y0 = _y[start], y1 = _y[start + 1], y2 = _y[start + 2];

            double a = ((y2 - y0) / (x2 - x0) - (y1 - y0) / (x1 - x0)) / (x2 - x1);
            double b = (y1 - y0) / (x1 - x0) - a * (x1 - x0);
            double c = y0;

            return a * Math.Pow(targetX - x0, 2) + b * (targetX - x0) + c;
        }

        // Кубічний сплайн S3(x) (Природний сплайн)
        public double Cubic(double targetX)
        {
            int n = _x.Length;
            double[] h = new double[n - 1];
            for (int i = 0; i < n - 1; i++) h[i] = _x[i + 1] - _x[i];

            double[] alpha = new double[n - 1];
            for (int i = 1; i < n - 1; i++)
                alpha[i] = (3.0 / h[i]) * (_y[i + 1] - _y[i]) - (3.0 / h[i - 1]) * (_y[i] - _y[i - 1]);

            double[] l = new double[n], mu = new double[n], z = new double[n];
            l[0] = 1; mu[0] = 0; z[0] = 0;

            for (int i = 1; i < n - 1; i++)
            {
                l[i] = 2 * (_x[i + 1] - _x[i - 1]) - h[i - 1] * mu[i - 1];
                mu[i] = h[i] / l[i];
                z[i] = (alpha[i] - h[i - 1] * z[i - 1]) / l[i];
            }

            l[n - 1] = 1; z[n - 1] = 0;
            double[] b = new double[n], c = new double[n], d = new double[n];
            c[n - 1] = 0;

            for (int j = n - 2; j >= 0; j--)
            {
                c[j] = z[j] - mu[j] * c[j + 1];
                b[j] = (_y[j + 1] - _y[j]) / h[j] - h[j] * (c[j + 1] + 2 * c[j]) / 3.0;
                d[j] = (c[j + 1] - c[j]) / (3.0 * h[j]);
            }

            int idx = FindInterval(targetX);
            double dx = targetX - _x[idx];
            return _y[idx] + b[idx] * dx + c[idx] * dx * dx + d[idx] * dx * dx * dx;
        }

        private int FindInterval(double x)
        {
            for (int i = 0; i < _x.Length - 1; i++)
                if (x >= _x[i] && x <= _x[i + 1]) return i;
            return x < _x[0] ? 0 : _x.Length - 2;
        }
    }


    public class LeastSquaresApproximation
    {
        private readonly double[] _x;
        private readonly double[] _y;
        private double[] _coefficients; // Коефіцієнти a0, a1, a2...
        public int Degree { get; private set; }

        public LeastSquaresApproximation(double[] x, double[] y)
        {
            _x = x;
            _y = y;
        }

        /// <summary>
        /// Обчислює коефіцієнти полінома заданого степеня m.
        /// </summary>
        public void Calculate(int degree)
        {
            Degree = degree;
            int n = _x.Length;
            int m = degree + 1;

            double[,] matrix = new double[m, m];
            double[] b = new double[m];

            // Формування матриці системи нормальних рівнянь
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    double sumX = 0;
                    for (int k = 0; k < n; k++)
                        sumX += Math.Pow(_x[k], i + j);
                    matrix[i, j] = sumX;
                }

                double sumY = 0;
                for (int k = 0; k < n; k++)
                    sumY += _y[k] * Math.Pow(_x[k], i);
                b[i] = sumY;
            }

            _coefficients = SolveGauss(matrix, b);
        }

        /// <summary>
        /// Повертає значення апроксимуючого полінома в точці x.
        /// </summary>
        public double Predict(double x)
        {
            double result = 0;
            for (int i = 0; i < _coefficients.Length; i++)
                result += _coefficients[i] * Math.Pow(x, i);
            return result;
        }

        /// <summary>
        /// Обчислює середньоквадратичну похибку (RMSE).
        /// </summary>
        public double CalculateRMSE()
        {
            double sumError = 0;
            for (int i = 0; i < _x.Length; i++)
                sumError += Math.Pow(_y[i] - Predict(_x[i]), 2);

            return Math.Sqrt(sumError / _x.Length);
        }

        public double[] GetCoefficients() => _coefficients;

        // Метод Гаусса для розв'язання СЛАР
        private double[] SolveGauss(double[,] matrix, double[] b)
        {
            int n = b.Length;
            for (int i = 0; i < n; i++)
            {
                int max = i;
                for (int k = i + 1; k < n; k++)
                    if (Math.Abs(matrix[k, i]) > Math.Abs(matrix[max, i])) max = k;

                for (int k = i; k < n; k++)
                {
                    double tmp = matrix[max, k];
                    matrix[max, k] = matrix[i, k];
                    matrix[i, k] = tmp;
                }
                double t = b[max]; b[max] = b[i]; b[i] = t;

                for (int k = i + 1; k < n; k++)
                {
                    double factor = matrix[k, i] / matrix[i, i];
                    b[k] -= factor * b[i];
                    for (int j = i; j < n; j++)
                        matrix[k, j] -= factor * matrix[i, j];
                }
            }

            double[] x = new double[n];
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < n; j++)
                    sum += matrix[i, j] * x[j];
                x[i] = (b[i] - sum) / matrix[i, i];
            }
            return x;
        }
    }
}