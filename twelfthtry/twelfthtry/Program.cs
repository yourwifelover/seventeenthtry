using twelfthtry;

//Банкiвська картка.Банк випускає картки клiєнтам. Кожна картка має унiкальний номер, власника та лiмiт витрат.
//Номер змiнювати не можна. Потрiбно знати кiлькiсть створених карток. Створiть клас BankCard iз закритими полями (private).
//Номер має бути незмiнним (readonly). Для пiдрахунку використайте спiльне поле (static). Передбачте створення картки з початковим лiмiтом i без нього (лiмiт = 0).
//Реалiзуйте метод змiни лiмiту (значення не може бути вiд’ємним) та метод перевiрки, чи операцiя перевищує лiмiт. Лiмiт отримувати через властивiсть тiльки для читання (property get).
//Додайте статичний метод пiдрахунку (static method) i перевизначте ToString() (override). У Main() протестуйте роботу.

using System;

namespace twelfthtry
{
    class Program
    {
        static void Main(string[] args)
        {
            List<BankCard> listforbank = new List<BankCard>();
            bool work = true;

            do
            {
                Console.WriteLine("Ви хочете отримати картку?");
                Console.WriteLine("Введiть yes для продовдення, та no для вiдмови");

                string work_or_no = Console.ReadLine()!.ToLower();

                if (work_or_no == "no")
                {
                    work = false;
                    break;
                }
                else if (work_or_no != "yes")
                {
                    Console.WriteLine("Неправильний ввід\n");
                    continue;
                }

                Console.WriteLine("Введiть ваше iм'я");
                string owner = Console.ReadLine()!;

                Console.WriteLine("Введiть бажаний номер ");
                int number = int.Parse(Console.ReadLine()!);

                Console.WriteLine("Який встановити лiмiт");
                int limit = int.Parse(Console.ReadLine()!);

                BankCard card1 = new BankCard(owner, number, limit);
                listforbank.Add(card1);

            } while (work);

            Console.WriteLine($"Створено карток: {BankCard.Count()}");
        }
    }
}






