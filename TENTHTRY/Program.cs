// Dr. Mario 
using System.Diagnostics;

Random random = new Random();
Stopwatch stopwatch = new Stopwatch();
char[,] bottle = new char[25, 100];
char[] greeting = {'D', 'R', '.', ' ', 'M', 'A', 'R', 'I', 'O'};
int microbs = 0;
//___________________________________________________________________________________________________________________________________//
//___________________________________________________________________________________________________________________________________//
Console.SetCursorPosition(56, 5);
Console.WriteLine($"{greeting[0]}  {greeting[1]} {greeting[2]}  {greeting[3]}  {greeting[4]}  {greeting[5]}  {greeting[6]}  {greeting[7]}  {greeting[8]}");
Console.ReadKey();
Console.Clear();
Console.WriteLine(@"Вибери скланiсть гри:
Легко       Нормально       Складно");
string difficulty = Console.ReadLine()!.ToLower();
if (difficulty == "легко")
    microbs = 5;
if (difficulty == "нормально")
    microbs = 8;
if (difficulty == "складно")
    microbs = 12;


//___________________________________________________________________________________________________________________________________//
//___________________________________________________________________________________________________________________________________//

int time = 1000;
//___________________________________________________________________________________________________________________________________//
//___________________________________________________________________________________________________________________________________//
for (int r = 0; r < bottle.GetLength(0); r++)
{
    for (int c = 0; c < bottle.GetLength(1); c++)
    {
        bottle[r, c] = ' ';

        if ((c == 46 || c == 54) && r <= 5 && r >= 4)
            bottle[r, c] = '|';

        else if (c == 45 && r == 3)
            bottle[r, c] = '\\';

        else if (r == 3 && c == 55)
            bottle[r, c] = '/';

        else if ((c == 44 || c ==56) && r==2)
            bottle[r, c] = '|';

        else if (r == 5 && c >= 40 && c <= 46)
            bottle[r, c] = '_';

        else if (r == 5 && c >= 55 && c <= 60)
            bottle[r, c] = '_';


        else if ((c == 40 || c == 60) && r >= 5 && r <= 24)
            bottle[r, c] = '|';


        else if (r == 24 && c >= 40 && c <= 60)
            bottle[r, c] = '_';
    }
}
static void Print(char[,] bottle)
{
    Console.SetCursorPosition(0, 0);
    for (int r = 0; r < bottle.GetLength(0); r++)
    {
        for (int c = 0; c < bottle.GetLength(1); c++)
        {
            Console.Write(bottle[r, c]);
        }
        Console.WriteLine();
    }
}

char[] micro = { '#', '@', '&' };
//___________________________________________________________________________________________________________________________________//
//___________________________________________________________________________________________________________________________________//
//Microb
static void Microb(char[,] bottle, char[] micro, Random random, int microbs)

{
    for (int i = 0; i < microbs; i++)
    {
        char microb;
        microb = micro[random.Next(0, micro.Length)];
        int r = random.Next(10, 20);
        int c = random.Next(42, 58);
        bottle[r, c] = microb;
    }
}





//Pills
int pillCol = 50;
int pillRow = 5;
char[] chars = { '#', '@', '&' };
char[] pills = new char[2];
void Pills(char[,] bottle, char[] pills,char[] chars, Random random)
{
    
    for (int i = 0; i < pills.Length; i++)
    {
        pills[i] = chars[random.Next(chars.Length)];
    }
}

Microb(bottle, micro, random, microbs);
Pills(bottle, pills,chars, random);
bool horizontal = true;

void ClearPill(char[,] bottle, int pillRow, int pillCol, bool horizontal)
{
    bottle[pillRow, pillCol] = ' ';
    if (horizontal)
        bottle[pillRow, pillCol + 1] = ' ';
    else
        bottle[pillRow + 1, pillCol] = ' ';
}




//___________________________________________________________________________________________________________________________________//
//___________________________________________________________________________________________________________________________________//
void MoveDown(char[,] bottle, ref int pillRow, ref int pillCol, char[] pills, bool horizontal, Stopwatch stopwatch, int time)
{   
    ClearPill(bottle, pillRow, pillCol, horizontal);

    if (horizontal)
    {
        if (bottle[pillRow + 1, pillCol] == ' ' && bottle[pillRow + 1, pillCol + 1] == ' ')
        {
            pillRow++;
            bottle[pillRow, pillCol] = pills[0];
            bottle[pillRow, pillCol + 1] = pills[1];
        }
    }
    else
    {
        if (pillRow + 2 < bottle.GetLength(0) && bottle[pillRow + 2, pillCol] == ' ')
        {
            pillRow++;
            bottle[pillRow, pillCol] = pills[0];
            bottle[pillRow + 1, pillCol] = pills[1];
        }
    }
}




void MoveRight(char[,] bottle, ref int pillRow, ref int pillCol, char[] pills, bool horizontal)
{
    time = 500;
    ClearPill(bottle, pillRow, pillCol, horizontal);

    if (horizontal)
    {
        if (bottle[pillRow, pillCol + 2] == ' ')
            pillCol++;
        bottle[pillRow, pillCol] = pills[0];
        bottle[pillRow, pillCol + 1] = pills[1];
    }
    else if(!horizontal)
    {
        if (bottle[pillRow, pillCol + 1] == ' ')
            pillCol++;
        bottle[pillRow, pillCol] = pills[0];
        bottle[pillRow + 1, pillCol] = pills[1];
    }
}


void MoveLeft(char[,] bottle, ref int pillRow, ref int pillCol, char[] pills, bool horizontal)
{
    ClearPill(bottle, pillRow, pillCol, horizontal);
    time = 500;

    if (horizontal)
    {
        if (bottle[pillRow, pillCol - 1] == ' ' && bottle[pillRow, pillCol] == ' ')
            pillCol--;
        bottle[pillRow, pillCol] = pills[0];
        bottle[pillRow, pillCol + 1] = pills[1];
    }
    else if (!horizontal)
    {
        if (bottle[pillRow, pillCol - 1] == ' ' && bottle[pillRow + 1, pillCol - 1] == ' ')
            pillCol--;
        bottle[pillRow, pillCol] = pills[0];
        bottle[pillRow + 1, pillCol] = pills[1];
    }

}


void Rotate(char[,] bottle, ref int pillRow, ref int pillCol, char[] pills, ref bool horizontal)
{
    ClearPill(bottle, pillRow, pillCol, horizontal);
    if (horizontal && bottle[pillRow + 1, pillCol] == ' ')
    {

        bottle[pillRow, pillCol] = pills[0];
        bottle[pillRow+1, pillCol] = pills[1];

        horizontal = false;
    }


    else
    {
        if (bottle[pillRow, pillCol + 1] == ' ')
        {

            bottle[pillRow, pillCol] = pills[0];
            bottle[pillRow, pillCol + 1] = pills[1];

            horizontal = true;
        }
    }
    time = 500;

}

//___________________________________________________________________________________________________________________________________//
//___________________________________________________________________________________________________________________________________//


void ResetPill(ref int pillRow, ref int pillCol, char[] pills, Random random, ref bool horizontal)
{
    pillRow = 5;
    pillCol = 50;
    Pills(bottle, pills, chars, random);
    horizontal = true;
}

void IfPillLanded(char[,] bottle, ref int pillRow, ref int pillCol, char[] pills, Random random, ref bool horizontal)
{
    if (horizontal)
    {
        if (bottle[pillRow + 1, pillCol] != ' ' || bottle[pillRow + 1, pillCol + 1] != ' ')
        {
            ResetPill(ref pillRow, ref pillCol, pills, random, ref horizontal);
        }
    }
    else
    {
        if (bottle[pillRow + 2, pillCol] != ' ')
        {
            ResetPill(ref pillRow, ref pillCol, pills, random, ref horizontal);
        }
    }
    time = 1000;
}

//___________________________________________________________________________________________________________________________________//
//___________________________________________________________________________________________________________________________________//



void IfMatches(char[,] bottle)
{
    if (horizontal)
    {
        if (bottle[pillRow, pillCol] == bottle[pillRow + 1, pillCol] && bottle[pillRow, pillCol] == bottle[pillRow + 2, pillCol] && bottle[pillRow, pillCol] == bottle[pillRow + 3, pillCol])
        {
            bottle[pillRow, pillCol] = ' ';
            bottle[pillRow + 1, pillCol] = ' ';
            bottle[pillRow + 2, pillCol] = ' ';
            bottle[pillRow + 3, pillCol] = ' ';
            
            pills[0] = ' ';
        }
        else if (bottle[pillRow, pillCol + 1] == bottle[pillRow + 1, pillCol + 1] && bottle[pillRow, pillCol + 1] == bottle[pillRow + 2, pillCol + 1] && bottle[pillRow, pillCol + 1] == bottle[pillRow + 3, pillCol + 1])
        {
            bottle[pillRow, pillCol + 1] = ' ';
            bottle[pillRow + 1, pillCol + 1] = ' ';
            bottle[pillRow + 2, pillCol + 1] = ' ';
            bottle[pillRow + 3, pillCol + 1] = ' ';
            
            pills[1] = ' ';

        }
    }
    else
    {
        if (bottle[pillRow, pillCol] == bottle[pillRow + 1, pillCol] && bottle[pillRow, pillCol] == bottle[pillRow + 2, pillCol] && bottle[pillRow, pillCol] == bottle[pillRow + 3, pillCol])
        {
            bottle[pillRow, pillCol] = ' ';
            bottle[pillRow + 1, pillCol] = ' ';
            bottle[pillRow + 2, pillCol] = ' ';
            bottle[pillRow + 3, pillCol] = ' ';

            pills[0] = ' '; 
        }
        else if (bottle[pillRow + 1, pillCol] == bottle[pillRow + 2, pillCol] && bottle[pillRow + 1, pillCol] == bottle[pillRow + 3, pillCol] && bottle[pillRow + 1, pillCol] == bottle[pillRow + 4, pillCol])
        {
            bottle[pillRow + 1, pillCol] = ' ';
            bottle[pillRow + 2, pillCol] = ' ';
            bottle[pillRow + 3, pillCol] = ' ';
            bottle[pillRow + 4, pillCol] = ' ';

            pills[1] = ' '; 
        }
    }

}

//___________________________________________________________________________________________________________________________________//
//___________________________________________________________________________________________________________________________________//

bool gameOver = false;


//___________________________________________________________________________________________________________________________________//
//___________________________________________________________________________________________________________________________________//
stopwatch.Start();
Print(bottle);
while (true)
{
    if (Console.KeyAvailable)
    {
        var key = Console.ReadKey(true);
        switch (key.Key)
        {
            case ConsoleKey.RightArrow:
                MoveRight(bottle, ref pillRow, ref pillCol, pills, horizontal);
                break;
            case ConsoleKey.LeftArrow:
                MoveLeft(bottle, ref pillRow, ref pillCol, pills, horizontal);
                break;
            case ConsoleKey.DownArrow:
                time = 100;
                break;
            case ConsoleKey.UpArrow:
                Rotate(bottle, ref pillRow, ref pillCol, pills, ref horizontal);
                break;
            case ConsoleKey.Escape:
                return;
        }
    }
    if (stopwatch.ElapsedMilliseconds >= time)
    {
        MoveDown(bottle, ref pillRow, ref pillCol, pills, horizontal, stopwatch, time);
        IfMatches(bottle);
        Print(bottle);
        IfPillLanded(bottle, ref pillRow, ref pillCol, pills, random, ref horizontal);
        stopwatch.Restart();
        time = 1000;
    }
    if ((bottle[5, 50] != ' ' || bottle[5, 51] != ' ') && (bottle[6, 50] != ' ' || bottle[6, 51] != ' '))
    {
        gameOver = true;
    }

    if (gameOver)
    {
        Console.Clear();
        Console.SetCursorPosition(56, 5);
        Console.WriteLine("Game Over!");
        Console.ReadKey();
    }
}
//___________________________________________________________________________________________________________________________________//
//___________________________________________________________________________________________________________________________________//







