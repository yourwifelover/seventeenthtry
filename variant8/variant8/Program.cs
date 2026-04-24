using System;
using NumericalMethods.Interpolation;

class Program
{
    static void Main()
    {
        // Данi з Варiанту 25
        double[] xPoints = { 0.132, 0.567, 1.153, 2.414, 3.939 };
        double[] yPoints = { 69.531, 1.112, -1.672, -1.922, -1.925 };

        // Створюємо екземпляр класу
        var interpolator = new LagrangeInterpolation(xPoints, yPoints);

        // Обчислення в конкретнiй точцi
        double testX = 1.0;
        double result = interpolator.Solve(testX);

        Console.WriteLine($"Результат у точцi {testX}: {result:F4}");

        // Приклад для побудови таблицi значень (для графiка)
        Console.WriteLine("\nТаблиця значень для графiка:");
        for (double k = 0.132; k <= 3.939; k += 0.5)
        {
            Console.WriteLine($"{k:F3}\t{interpolator.Solve(k):F4}");
        }

        Console.WriteLine("==============================================================");

        double[] xData = { 0.132, 0.567, 1.153, 2.414, 3.939 };
        double[] yData = { 69.531, 1.112, -1.672, -1.922, -1.925 };

        var newton = new NewtonInterpolation(xData, yData);

        // 1. Вивiд таблицi роздiлених рiзниць
        newton.PrintTable();

        // 2. Обчислення значення в точцi x1 + x2
        double targetX = xData[1] + xData[2]; // 0.567 + 1.153
        double result1 = newton.Solve(targetX);

        Console.WriteLine($"\nТочка обчислення x1 + x2 = {targetX:F3}");
        Console.WriteLine($"Значення полiнома Ньютона Nm(x1+x2) = {result1:F4}");

        // 3. Данi для графiка
        Console.WriteLine("\nДанi для графiка (x | y):");
        for (double l = xData[0]; l <= xData[xData.Length - 1]; l += 0.4)
        {
            Console.WriteLine($"{l:F3}\t{newton.Solve(l):F4}");
        }


        Console.WriteLine("=================================================");


        var splines = new SplineInterpolation(xData, yData);
        var lagrange = new LagrangeInterpolation(xData, yData); // Використовуємо ваш попереднiй клас

        double targetX1 = xData[1] + xData[2]; // x1 + x2 = 1.720

        double s1 = splines.Linear(targetX1);
        double s2 = splines.Quadratic(targetX1);
        double s3 = splines.Cubic(targetX1);
        double lVal = lagrange.Solve(targetX1);

        Console.WriteLine($"Точка x1 + x2 = {targetX1:F3}");
        Console.WriteLine("------------------------------------------");
        Console.WriteLine($"Лiнiйний сплайн S1:    {s1:F4}");
        Console.WriteLine($"Квадратичний сплайн S2: {s2:F4}");
        Console.WriteLine($"Кубiчний сплайн S3:    {s3:F4}");
        Console.WriteLine($"Полiном Лагранжа L:    {lVal:F4}");
        Console.WriteLine("------------------------------------------");

        double diff = Math.Abs(s3 - lVal);
        Console.WriteLine($"Рiзниця мiж S3 та Лагранжем: {diff:F6}");


        Console.WriteLine("=============================================");

        double[] x = { 0.282, 0.614, 0.946, 1.278, 1.610, 1.942, 2.274, 2.606, 2.938 };
        double[] y = { 6.324, 0.848, -0.473, -0.938, -1.062, -1.225, -1.255, -1.433, -1.285 };

        var lsq = new LeastSquaresApproximation(x, y);

        // Вибираємо 2-й степiнь (квадратична апроксимацiя)
        lsq.Calculate(2);

        Console.WriteLine($"Коефiцiєнти полiнома {lsq.Degree}-го степеня:");
        var coeffs = lsq.GetCoefficients();
        for (int i = 0; i < coeffs.Length; i++)
            Console.WriteLine($"a{i} = {coeffs[i]:F4}");

        Console.WriteLine($"\nСередньоквадратична похибка (RMSE): {lsq.CalculateRMSE():F6}");

        // Данi для побудови графiкiв та похибок у вузлах
        Console.WriteLine("\nВузол | Факт y | Полiном | Вiдхилення (Ei)");
        for (int i = 0; i < x.Length; i++)
        {
            double predicted = lsq.Predict(x[i]);
            double error = y[i] - predicted;
            Console.WriteLine($"{x[i]:F3} | {y[i]:F3} | {predicted:F3} | {error:F4}");
        }



    }



}