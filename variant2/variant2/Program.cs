using System;
class RootFinder
{
    static double F(double x) => Math.Pow(x, 4) - 5.2 * Math.Pow(x, 2) + 1;
    static double dF(double x) => 4 * Math.Pow(x, 3) - 10.4 * x;

    static double G(double x) => Math.Pow(x, 4) - 10 * Math.Pow(x, 2) + 25;
    static double dG(double x) => 4 * Math.Pow(x, 3) - 20 * x;
    static double Phi(double x)
    {
        return Math.Log(3 + 2 * Math.Log(x + 1));
    }


    static void Main()
    {
        double[] epsValues = { 1e-4, 1e-5, 1e-6 };

        double[,] intervalsF = { { 0, 1 }, { 2, 3 }, { -1, 0 }, { -3, -2 } };

        double[,] intervalsG = { { 2, 3 }, { -3, -2 } };

        double[,] intervalsS = { { -0.9, 0 }, { 1, 2 } };

        foreach (double eps in epsValues)
        {
            static void NewtonRuns(Func<double, double> func,Func<double, double> deriv,double a,double b,double eps)
            {
                
                double x = (a + b) / 2; // початкове наближення
                double xNext;
                int iterations = 0;

                do
                {
                    xNext = x - func(x) / deriv(x);
                    x = xNext;
                    iterations++;

                } while (Math.Abs(func(x)) > eps);

                Console.WriteLine($"iнтервал [{a},{b}]");
                Console.WriteLine($"Корiнь: {x}");
                Console.WriteLine($"iтерацiй: {iterations}");
                

            }
            Console.WriteLine($"\nФункцiя F:  || Точнiсть: {eps}" );
            for (int i = 0; i < intervalsF.GetLength(0); i++)
            {
                double a = intervalsF[i, 0];
                double b = intervalsF[i, 1];

                NewtonRuns(F, dF, a, b, eps);
                
            }

            Console.WriteLine("======================================");
            Console.WriteLine("======================================");


            
            Console.WriteLine($"\nФункцiя G: || Точнiсть: {eps}");
            for (int i = 0; i < intervalsG.GetLength(0); i++)
            {
                double a = intervalsG[i, 0];
                double b = intervalsG[i, 1];

                NewtonRuns(G, dG, a, b, eps);

            }
            Console.WriteLine("========================================");


            //gfffffffff

         
            Console.WriteLine($"\nФункція S (Simple Iteration): || Точність: {eps}");

            static void SimpleIteration(double a, double b, double eps)
            {
            
                double x0 = (a + b) / 2.0;
                double x1 = 0;
                int iterations = 0;

                // Перевірка, який корінь шукаємо (лівий від'ємний чи правий додатний)
                bool isLeftRoot = (a < 0);

                do
                {
                    if (isLeftRoot)
                    {
                       
                        x1 = Math.Exp((Math.Exp(x0) - 3) / 2.0) - 1;
                    }
                    else
                    {
                        
                        x1 = Math.Log(3 + 2 * Math.Log(x0 + 1));
                    }

                    if (Math.Abs(x1 - x0) < eps)
                        break;

                    x0 = x1;
                    iterations++;

                } while (iterations < 20000); 

                Console.WriteLine($"Інтервал: [{a}, {b}]");
                Console.WriteLine($"Корінь: {x1:F8}"); // Вивід з фіксованою точністю
                Console.WriteLine($"Ітерацій: {iterations}");
            }

            for (int i = 0; i < intervalsS.GetLength(0); i++)
            {
                double a = intervalsS[i, 0];
                double b = intervalsS[i, 1];
                SimpleIteration(a, b, eps);
            }
            Console.WriteLine("========================================");
            Console.WriteLine("========================================");










        }

        
        
    }
}