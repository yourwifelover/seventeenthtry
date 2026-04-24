using System;

class Program
{
    static void Main()
    {
        // 1. ДАНi ДЛЯ ЗАВДАНЬ 4.1 - 4.2 (4x4)
        double[,] A1 = {
            { 0.63, -0.76,  1.34,  0.37 },
            { 0.54,  0.83, -0.74, -1.27 },
            { 0.24, -0.44,  0.35,  0.55 },
            { 0.43, -1.21,  2.32, -1.41 }
        };
        double[] b1 = { 1.21, 0.86, 0.25, 1.55 };

        Console.WriteLine("=== ЗАВДАННЯ 4.1 - 4.2 ===");
        SolveSystem(A1, b1, 4);

        // 2. ДАНi ДЛЯ ЗАВДАНЬ 4.3 - 4.4 (5x5)
        double[,] A2 = {
            {  0.120905189,  0.055520159,  0.041356057,  0.042215205, -0.038241878 },
            {  0.314079627,  0.144292722,  0.107511695,  0.109677581, -0.099218392 },
            {  0.076533492,  0.035331078,  0.026407765,  0.026763790, -0.023844265 },
            { -0.529751727, -0.242727415, -0.180540296, -0.184846379,  0.168614474 },
            {  0.495636038,  0.227264981,  0.169121068,  0.172979740, -0.157427579 }
        };
        double[] b2 = { 0.333665108, 0.867818520, 0.214252776, -1.453140591, 1.362310269 };

        Console.WriteLine("\n=== ЗАВДАННЯ 4.3 - 4.4 ===");
        SolveSystem(A2, b2, 5);

        // Аналiз похибки для 5x5
        Console.WriteLine("\n--- Аналiз похибки (дiагональ +10^-4) ---");
        for (int i = 0; i < 5; i++) A2[i, i] += 0.0001;
        SolveSystem(A2, b2, 5);
    }

    static double GetMatrixNorm(double[,] matrix, int n)
    {
        double maxRowSum = 0;
        for (int i = 0; i < n; i++)
        {
            double rowSum = 0;
            for (int j = 0; j < n; j++) rowSum += Math.Abs(matrix[i, j]);
            if (rowSum > maxRowSum) maxRowSum = rowSum;
        }
        return maxRowSum;
    }


    static double[,] GetInverseMatrix(double[,] L, double[,] U, int n)
    {
        double[,] inv = new double[n, n];
        for (int col = 0; col < n; col++)
        {
            // Створюємо стовпець одиничної матриці (e_i)
            double[] b = new double[n];
            b[col] = 1;

            // Пряма підстановка Ly = b
            double[] y = new double[n];
            for (int i = 0; i < n; i++)
            {
                double sum = 0;
                for (int j = 0; j < i; j++) sum += L[i, j] * y[j];
                y[i] = b[i] - sum;
            }

            // Зворотна підстановка Ux = y
            for (int i = n - 1; i >= 0; i--)
            {
                double sum = 0;
                for (int j = i + 1; j < n; j++) sum += U[i, j] * inv[j, col];
                inv[i, col] = (y[i] - sum) / U[i, i];
            }
        }
        return inv;
    }

    static void SolveSystem(double[,] A, double[] b, int n)
    {
        double[,] L = new double[n, n];
        double[,] U = new double[n, n];

        // LU-розклад
        for (int i = 0; i < n; i++)
        {
            for (int k = i; k < n; k++)
            {
                double sum = 0;
                for (int j = 0; j < i; j++) sum += L[i, j] * U[j, k];
                U[i, k] = A[i, k] - sum;
            }
            for (int k = i; k < n; k++)
            {
                if (i == k) L[i, i] = 1;
                else
                {
                    double sum = 0;
                    for (int j = 0; j < i; j++) sum += L[k, j] * U[j, i];
                    L[k, i] = (A[k, i] - sum) / U[i, i];
                }
            }
        }

        // Визначник
        double det = 1;
        for (int i = 0; i < n; i++) det *= U[i, i];
        Console.WriteLine($"Визначник: {det:F6}");

        // Розв'язок (Ly = b, потiм Ux = y)
        double[] y = new double[n];
        for (int i = 0; i < n; i++)
        {
            double sum = 0;
            for (int j = 0; j < i; j++) sum += L[i, j] * y[j];
            y[i] = b[i] - sum;
        }

        double[] x = new double[n];
        for (int i = n - 1; i >= 0; i--)
        {
            double sum = 0;
            for (int j = i + 1; j < n; j++) sum += U[i, j] * x[j];
            x[i] = (y[i] - sum) / U[i, i];
        }


        static double GetMatrixNorm(double[,] matrix, int n)
        {
            double maxRowSum = 0;
            for (int i = 0; i < n; i++)
            {
                double rowSum = 0;
                for (int j = 0; j < n; j++) rowSum += Math.Abs(matrix[i, j]);
                if (rowSum > maxRowSum) maxRowSum = rowSum;
            }
            return maxRowSum;
        }


        double normA = GetMatrixNorm(A, n);
        double[,] invA = GetInverseMatrix(L, U, n);
        double normInvA = GetMatrixNorm(invA, n);
        double cond = normA * normInvA;

        Console.WriteLine($"Норма A: {normA:F6}");
        Console.WriteLine($"Число обумовленості cond(A): {cond:F6}");

        Console.Write("Коренi x: ");
        for (int i = 0; i < n; i++) Console.Write($"{x[i]:F6}  ");
        Console.WriteLine();
    }
}