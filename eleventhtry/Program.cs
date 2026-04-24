

string diarytext = @"Сьогоднi я отримав вiдмiнну оцiнку з математики!
Я дуже радий!
Моя мама буде пишатися мною.
Я люблю вивчати новi речi!
Чи зможу я отримати ще одну вiдмiнну оцiнку завтра?";
File.WriteAllText("diary.txt", diarytext);


List<string> lines = File.ReadAllLines("diary.txt").ToList();
foreach (string line in lines)
{
    Console.WriteLine(line);
}
Console.WriteLine("=========================================");
lines.Reverse();
foreach (string line in lines)
{
    Console.WriteLine(line);
}   
File.WriteAllLines("diary_reversed.txt", lines);
//+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



