using Newtonsoft.Json;
using System.Linq;
using System.Text.Json;


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
Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");


Dictionary<char, int> d1 = new Dictionary<char, int>()
{
    {'a', 100},
    {'b', 200},
    {'c', 300}
};


Dictionary<char, int> d2 = new Dictionary<char, int>()
{
    {'a', 300 },
    {'b', 200 },
    {'d', 400 }
};
Dictionary<char, int> d3 = new Dictionary<char, int>();

d3 = d2.Concat(d1)
       .GroupBy(pair => pair.Key)
       .ToDictionary(group => group.Key,group => group.Sum(pair => pair.Value)
       );

string json = JsonConvert.SerializeObject(d3);
Console.WriteLine(json);


Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");


List<int> just_numbers = new List<int>() { -1, -2, 3, 4, -5, -6, 7, -8, 9};

List<int> not_just_numbers = new List<int>();

not_just_numbers = just_numbers.Where(p => p%2 == 0).Where( p=> p<0).ToList();
not_just_numbers.Reverse();
foreach(var item in not_just_numbers)
    Console.WriteLine(item);








