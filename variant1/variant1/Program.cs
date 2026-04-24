using System;

class RootFinder
{

    static double F(double x) => Math.Pow(x, 4) - 5.2 * Math.Pow(x, 2) + 1;

   
    static double G(double x) => Math.Pow(x, 4) - 10 * Math.Pow(x, 2) + 25;

    static void Main()
    {
        double[] epsValues = { 1e-4, 1e-5, 1e-6 };

        
        double[,] intervalsF = { { 0, 1 }, { 2, 3 }, { -1, 0 }, { -3, -2 } };
        
        double[,] intervalsG = { { 2, 3 }, { -3, -2 } };

        foreach (double eps in epsValues)
        {
            PrintHeader($"АНАЛІЗ ДЛЯ ТОЧНОСТІ: {eps}");

            // --- ДОСЛІДЖЕННЯ ФУНКЦІЇ F ---
            Console.WriteLine("\n>>> ФУНКЦІЯ f(x) (4 різні корені)");

            Console.WriteLine("\nМетод Бісекції:");
            for (int i = 0; i < 4; i++)
                RunBisection(F, intervalsF[i, 0], intervalsF[i, 1], eps);

            Console.WriteLine("\nМетод Хорд:");
            for (int i = 0; i < 4; i++)
                RunChord(F, intervalsF[i, 0], intervalsF[i, 1], eps);

            Console.WriteLine("\nМетод Січних:");
            for (int i = 0; i < 4; i++)
                RunSecant(F, intervalsF[i, 0], intervalsF[i, 1], eps);

            // --- ДОСЛІДЖЕННЯ ФУНКЦІЇ G ---
            
            Console.WriteLine("\n" + new string('-', 20));
            Console.WriteLine(">>> ФУНКЦІЯ g(x) (кратні корені)");
            Console.WriteLine("Метод Січних:");
            RunSecant(G, intervalsG[0, 0], intervalsG[0, 1], eps);
            RunSecant(G, intervalsG[1, 0], intervalsG[1, 1], eps);
        }

        Console.WriteLine("\nРозрахунки завершено. Натисніть клавішу для виходу...");
        Console.ReadKey();
    }

    
    static void RunBisection(Func<double, double> func, double a, double b, double eps)
    {
        if (func(a) * func(b) > 0) return;

        int iter = 0;
        double x = 0;
        while (Math.Abs(b - a) > eps)
        {
            x = (a + b) / 2.0;
            if (func(a) * func(x) < 0) b = x;
            else a = x;
            iter++;
        }
        PrintResult(x, iter);
    }

  
    static void RunChord(Func<double, double> func, double a, double b, double eps)
    {
        if (func(a) * func(b) > 0) return;

        int iter = 0;
        double x = a;
        double xPrev;
        do
        {
            xPrev = x;
            x = a - (func(a) * (b - a)) / (func(b) - func(a));

            if (func(a) * func(x) < 0) b = x;
            else a = x;
            iter++;
        } while (Math.Abs(x - xPrev) > eps && iter < 1000);
        PrintResult(x, iter);
    }

    
    static void RunSecant(Func<double, double> func, double x0, double x1, double eps)
    {
        int iter = 0;
        double x2;
        while (Math.Abs(x1 - x0) > eps && iter < 1000)
        {
            double den = func(x1) - func(x0);
            if (Math.Abs(den) < 1e-14) break;

            x2 = x1 - func(x1) * (x1 - x0) / den;
            x0 = x1;
            x1 = x2;
            iter++;
        }
        PrintResult(x1, iter);
    }

    static void PrintResult(double root, int iters)
    {
        Console.WriteLine($"Корінь: {root,12:F8} | Ітерацій: {iters,3}");
    }

    static void PrintHeader(string text)
    {
        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine(text);
        Console.WriteLine(new string('=', 50));
    }
}