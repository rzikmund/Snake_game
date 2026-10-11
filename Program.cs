//---variables---//
using System.Reflection;

Console.CursorVisible = false;

//border variables
int border_x = Console.WindowWidth;
int border_y = Console.WindowHeight;
string border_symbol = "X";

//snake variables
int snake_head_x = border_x/2;
int snake_head_y = border_y/2;
int snake_long = 1;
string snake_head_symbol = "*";

(int X, int Y)[] snake_location = new (int, int)[(border_x-2)*(border_y-2)];
snake_location[0] = (snake_head_x, snake_head_y);

//---Actions---//
Action border_creator = () =>
{
    for (int x = 0; x < border_x; x++)
    {
        Console.SetCursorPosition(x, 0);
        Console.Write(border_symbol);
        Console.SetCursorPosition(x, border_y - 1);
        Console.Write(border_symbol);
    }

    for (int y = 0; y < border_y; y++)
    {
        Console.SetCursorPosition(0, y);
        Console.Write(border_symbol);
        Console.SetCursorPosition(border_x - 1, y);
        Console.Write(border_symbol);
    }
};

Action snake = () =>
{
    //player movement
    if (Console.KeyAvailable)
    {
        ConsoleKeyInfo key = Console.ReadKey(true);
        while (Console.KeyAvailable)
        {
            key = Console.ReadKey(true);
        }

        if (key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow)
        {
            snake_head_y--;            
        }

        if (key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow)
        {
            snake_head_y++;
        }

        if (key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow)
        {
            snake_head_x--;
        }

        if (key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow)
        {
            snake_head_x++;
        }
    }

        //snake render
        for (int i = 0; i < snake_long; i++)
        {
            snake_location[i] = (snake_head_x, snake_head_y);

            Console.SetCursorPosition(snake_location[i].X, snake_location[i].Y);
            Console.Write(snake_head_symbol);
        }
    
};



//----gameloop----//
border_creator();
while (true)
{
    snake();
}