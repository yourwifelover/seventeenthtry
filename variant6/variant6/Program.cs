using System;

class Program
{
    static void Main()
    {
        // Наша матриця (Варiант 25)
        double[,] A = {
            { 0.5, 1.4, 2.0, 1.0 },
            { 1.4, 1.0, 0.0, 1.5 },
            { 2.0, 0.0, 2.5, 2.0 },
            { 1.0, 1.5, 2.0, 1.0 }
        };

        double[] epsilons = { 0.01, 0.001 };

        foreach (double eps in epsilons)
        {
            Console.WriteLine($"=== ТОЧНiСТЬ: {eps} ===");

            // а) Степеневий метод
            RunPowerMethod(A, eps);

            // б) Метод зворотних iтерацiй
            RunInverseIteration(A, eps);

            // в) Метод Релея
            RunRayleighMethod(A, eps);

            Console.WriteLine();
        }
    }

    // --- а) Степеневий метод (Максимальне власне значення) ---
    static void RunPowerMethod(double[,] A, double eps)
    {
        double[] x = { 1, 1, 1, 1 };
        double lambda = 0;
        double oldLambda;
        int iters = 0;

        do
        {
            iters++;
            oldLambda = lambda;

            double[] y = MultiplyMatrixVector(A, x);
            lambda = y[0] / x[0]; // Наближення значення
            x = NormalizeVector(y); // Наближення вектора

        } while (Math.Abs(lambda - oldLambda) > eps);

        Console.WriteLine($"Степеневий:  Iter = {iters}, Lambda = {lambda:F4}");
    }

    // --- б) Метод зворотних iтерацiй (Мiнiмальне власне значення) ---
    static void RunInverseIteration(double[,] A, double eps)
    {
        double[] x = { 1, 1, 1, 1 };
        double lambda = 0;
        double oldLambda;
        int iters = 0;

        do
        {
            iters++;
            oldLambda = lambda;

            // Ay = x  => знайти y (це як зворотне множення)
            double[] y = SolveGauss(A, x);

            // Для мiнiмального значення формула: x*x / y*x
            double dotXX = DotProduct(x, x);
            double dotYX = DotProduct(y, x);
            lambda = dotXX / dotYX;

            x = NormalizeVector(y);

        } while (Math.Abs(lambda - oldLambda) > eps);

        Console.WriteLine($"Зворотнi:    Iter = {iters}, Lambda = {lambda:F4}");
    }

    // --- в) Метод Релея (Максимальне через вiдношення Релея) ---
    static void RunRayleighMethod(double[,] A, double eps)
    {
        double[] x = { 1, 1, 1, 1 };
        double lambda = 0;
        double oldLambda;
        int iters = 0;

        do
        {
            iters++;
            oldLambda = lambda;

            double[] y = MultiplyMatrixVector(A, x);

            // Формула Релея: (Ax * x) / (x * x)
            lambda = DotProduct(y, x) / DotProduct(x, x);

            x = NormalizeVector(y);

        } while (Math.Abs(lambda - oldLambda) > eps);

        Console.WriteLine($"Метод Релея: Iter = {iters}, Lambda = {lambda:F4}");
    }

    // --- ПРОСТi ДОПОМiЖНi ФУНКЦiЇ ---

    // Множення матрицi 4х4 на вектор
    static double[] MultiplyMatrixVector(double[,] matrix, double[] vector)
    {
        double[] result = new double[4];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
                result[i] += matrix[i, j] * vector[j];
        }
        return result;
    }

    // Скалярний добуток векторiв
    static double DotProduct(double[] a, double[] b)
    {
        double sum = 0;
        for (int i = 0; i < 4; i++) sum += a[i] * b[i];
        return sum;
    }

    // Нормування вектора (щоб довжина була 1)
    static double[] NormalizeVector(double[] v)
    {
        double norm = Math.Sqrt(DotProduct(v, v));
        double[] res = new double[4];
        for (int i = 0; i < 4; i++) res[i] = v[i] / norm;
        return res;
    }

    // Простий метод Гаусса для розв'язання Ay = x
    static double[] SolveGauss(double[,] matrix, double[] vector)
    {
        int n = 4;
        double[,] a = (double[,])matrix.Clone();
        double[] b = (double[])vector.Clone();

        for (int i = 0; i < n; i++)
        {
            for (int k = i + 1; k < n; k++)
            {
                double factor = a[k, i] / a[i, i];
                for (int j = i; j < n; j++) a[k, j] -= factor * a[i, j];
                b[k] -= factor * b[i];
            }
        }

        double[] x = new double[n];
        for (int i = n - 1; i >= 0; i--)
        {
            double sum = 0;
            for (int j = i + 1; j < n; j++) sum += a[i, j] * x[j];
            x[i] = (b[i] - sum) / a[i, i];
        }
        return x;
    }
}