using System;

class Program
{
    static double F(double x, double y) => Math.Sin(x) - y - 0.6;
    static double G(double x, double y) => Math.Cos(y) + x - 1.1;

    static void Main()
    {
        // Початковi данi
        double xStart = 0.5;
        double yStart = 0.5;
        double[] epsilons = { 1e-4, 1e-5 };

        Console.WriteLine("{0,-15} | {1,-10} | {2,-10} | {3,-10} | {4}", "Метод", "Точнiсть", "X", "Y", "iтерацiї");
        Console.WriteLine(new string('-', 65));

        foreach (double eps in epsilons)
        {
            SimpleIteration(xStart, yStart, eps);
            NewtonMethod(xStart, yStart, eps);
        }
    }

    // 1. МЕТОД ПРОСТОЇ iТЕРАЦiЇ
    static void SimpleIteration(double x0, double y0, double eps)
    {
        double xPrev = x0, yPrev = y0;
        int iter = 0;

        while (true)
        {
            iter++;
            // Обчислюємо новi значення на основi попереднiх
            double xNext = 1.1 - Math.Cos(yPrev);
            double yNext = Math.Sin(xPrev) - 0.6;

            // Перевiрка на досягнення точностi
            if (Math.Abs(xNext - xPrev) < eps && Math.Abs(yNext - yPrev) < eps)
            {
                Console.WriteLine("{0,-15} | {1,-10} | {2,-10:F6} | {3,-10:F6} | {4}", "iтерацiї", eps, xNext, yNext, iter);
                break;
            }

            xPrev = xNext;
            yPrev = yNext;

            if (iter > 1000) break; 
        }
    }

    // 2. МЕТОД НЬЮТОНА
    static void NewtonMethod(double x0, double y0, double eps)
    {
        double x = x0, y = y0;
        int iter = 0;

        while (true)
        {
            iter++;
            
            
            double dfdx = Math.Cos(x);
            double dfdy = -1;
            double dgdx = 1;
            double dgdy = -Math.Sin(y);

            // Визначник матрицi Якобi
            double det = dfdx * dgdy - dfdy * dgdx;

            // Обчислення поправок (дельт)
            double dx = (-F(x, y) * dgdy + G(x, y) * dfdy) / det;
            double dy = (-G(x, y) * dfdx + F(x, y) * dgdx) / det;

            x += dx;
            y += dy;

            if (Math.Abs(dx) < eps && Math.Abs(dy) < eps)
            {
                Console.WriteLine("{0,-15} | {1,-10} | {2,-10:F6} | {3,-10:F6} | {4}", "Ньютона", eps, x, y, iter);
                break;
            }

            if (iter > 1000) break;
        }
    }
}