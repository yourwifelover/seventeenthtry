using System;
using System.Linq;

class EigenSolver
{
    static void Main()
    {
        // Матриця A (Симетрична)
        double[,] A = {
            { 0.5, 1.4, 2.0, 1.0 },
            { 1.4, 1.0, 0.0, 1.5 },
            { 2.0, 0.0, 2.5, 2.0 },
            { 1.0, 1.5, 2.0, 1.0 }
        };

        // Матриця B (Несиметрична)
        double[,] B = {
            { 1, 1, 2, 3 },
            { 3, -1, -1,-2},
            { 2, 3, -1, -1 },
            { 1, 2, 3, -1 }
        };

        double[] epsilons = { 0.01, 0.001 };

        foreach (var eps in epsilons)
        {
            Console.WriteLine($"\n======= ТОЧНІСТЬ: {eps} =======");

            Console.WriteLine("--- Матриця А (Симетрична) ---");
            SolveJacobi((double[,])A.Clone(), eps);
            SolveQR((double[,])A.Clone(), eps);

            Console.WriteLine("\n--- Матриця B (Несиметрична) ---");
            // Для несиметричної матриці метод Якобі не викликаємо!
            SolveQR((double[,])B.Clone(), eps);
        }
    }

    static void SolveJacobi(double[,] matrix, double eps)
    {
        int n = matrix.GetLength(0);
        int iterations = 0;
        double max;
        do
        {
            iterations++;
            int p = 0, q = 1;
            max = Math.Abs(matrix[0, 1]);
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                    if (Math.Abs(matrix[i, j]) > max)
                    {
                        max = Math.Abs(matrix[i, j]);
                        p = i; q = j;
                    }

            double phi = 0.5 * Math.Atan2(2 * matrix[p, q], matrix[p, p] - matrix[q, q]);
            double c = Math.Cos(phi), s = Math.Sin(phi);

            double app = c * c * matrix[p, p] + 2 * s * c * matrix[p, q] + s * s * matrix[q, q];
            double aqq = s * s * matrix[p, p] - 2 * s * c * matrix[p, q] + c * c * matrix[q, q];
            matrix[p, p] = app;
            matrix[q, q] = aqq;
            matrix[p, q] = matrix[q, p] = 0;

            for (int i = 0; i < n; i++)
            {
                if (i != p && i != q)
                {
                    double aip = c * matrix[i, p] + s * matrix[i, q];
                    double aiq = -s * matrix[i, p] + c * matrix[i, q];
                    matrix[i, p] = matrix[p, i] = aip;
                    matrix[i, q] = matrix[q, i] = aiq;
                }
            }
        } while (max > eps && iterations < 500); // Запобіжник

        Console.WriteLine($"Jacobi: ітерацій: {iterations}, Значення: {string.Join("; ", GetDiag(matrix))}");
    }

    static void SolveQR(double[,] matrix, double eps)
    {
        int n = matrix.GetLength(0);
        int iterations = 0;
        double norm;
        do
        {
            iterations++;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    double rho = Math.Sqrt(matrix[i, i] * matrix[i, i] + matrix[j, i] * matrix[j, i]);
                    if (rho < 1e-15) continue;
                    double c = matrix[i, i] / rho;
                    double s = matrix[j, i] / rho;

                    for (int k = 0; k < n; k++)
                    {
                        double m_ik = matrix[i, k];
                        matrix[i, k] = c * m_ik + s * matrix[j, k];
                        matrix[j, k] = -s * m_ik + c * matrix[j, k];
                    }
                    for (int k = 0; k < n; k++)
                    {
                        double m_ki = matrix[k, i];
                        matrix[k, i] = c * m_ki + s * matrix[k, j];
                        matrix[k, j] = -s * m_ki + c * matrix[k, j];
                    }
                }
            }
            norm = GetOffDiagNorm(matrix);
        } while (norm > eps && iterations < 500); // ОБОВ'ЯЗКОВО для матриці B

        Console.WriteLine($"QR-метод: ітерацій: {iterations}, Значення: {string.Join("; ", GetDiag(matrix))}");
    }

    static double GetOffDiagNorm(double[,] m)
    {
        double sum = 0;
        for (int i = 1; i < m.GetLength(0); i++)
            for (int j = 0; j < i; j++)
                sum += m[i, j] * m[i, j];
        return Math.Sqrt(sum);
    }

    static string[] GetDiag(double[,] m) =>
        Enumerable.Range(0, m.GetLength(0)).Select(i => m[i, i].ToString("F4")).ToArray();
}