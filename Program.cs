//---variables---//
using System.Reflection;

Console.CursorVisible = false;

//border variables
int border_x = Console.WindowWidth;
int border_y = Console.WindowHeight;
string border_symbol = "X";

//snake variables
int snake_head_x = border_x / 2;
int snake_head_y = border_y / 2;
int snake_long = 1;
string snake_direction = "D";
string snake_head_symbol = "*";

(int X, int Y)[] snake_location = new (int, int)[(border_x - 2) * (border_y - 2)];
snake_location[0] = (snake_head_x, snake_head_y);

//apple variables
int apple_count = 1;
(int X, int Y)[] apple_location = new (int, int)[apple_count];
string apple_symbol = "@";

//game variables
int game_speed = 100;
bool game_over = true;
bool win = false;



//---Actions---//
Action border_creator = () =>
{
    for (int x = 0; x < border_x; x++)
    {
        Console.ForegroundColor = ConsoleColor.White;
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

        if ((key.Key == ConsoleKey.W || key.Key == ConsoleKey.UpArrow) && snake_direction != "S")
        {
            snake_direction = "W";
        }

        else if ((key.Key == ConsoleKey.S || key.Key == ConsoleKey.DownArrow) && snake_direction != "W")
        {
            snake_direction = "S";
        }

        else if ((key.Key == ConsoleKey.A || key.Key == ConsoleKey.LeftArrow) && snake_direction != "D")
        {
            snake_direction = "A";
        }

        else if ((key.Key == ConsoleKey.D || key.Key == ConsoleKey.RightArrow) && snake_direction != "A")
        {
            snake_direction = "D";
        }

    }

    //snake movement
    switch (snake_direction)
    {
        case "W":
            snake_head_y--;
            break;

        case "S":
            snake_head_y++;
            break;

        case "A":
            snake_head_x--;
            break;

        case "D":
            snake_head_x++;
            break;
    }

    //snake delete
    var tail = snake_location[snake_long - 1];
    Console.SetCursorPosition(tail.X, tail.Y);
    Console.Write(" ");

    //snake shift
    for (int i = snake_long - 1; i > 0; i--)
    {
        snake_location[i] = snake_location[i - 1];
    }

    snake_location[0] = (snake_head_x, snake_head_y);   //head location (index0)

    //snake render
    for (int i = 0; i < snake_long; i++)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(snake_location[i].X, snake_location[i].Y);
        Console.Write(snake_head_symbol);
    }

};

Action apple = () =>
{
    apple_location[0] = (Random.Shared.Next(1, border_x - 1), Random.Shared.Next(1, border_y - 1));
    bool remake = true;
    while (remake == true)
    {
        for (int i = 0; i < snake_long; i++)
        {
            if (snake_location[i].X == apple_location[0].X)
            {
                apple_location[0] = (Random.Shared.Next(1, border_x - 1), Random.Shared.Next(1, border_y - 1));
            }
            else if (snake_location[i].Y == apple_location[0].Y)
            {
                apple_location[0] = (Random.Shared.Next(1, border_x - 1), Random.Shared.Next(1, border_y - 1));
            }
            else
            {
                remake = false;
            }
        }
    }

    //render
    Console.ForegroundColor = ConsoleColor.Red;
    Console.SetCursorPosition(apple_location[0].X, apple_location[0].Y);
    Console.Write(apple_symbol);

};

Action snake_colision = () =>
{
    //apple eating
    if (snake_location[0] == apple_location[0])
    {
        apple();
        snake_long++;
        Task.Run(() => Console.Beep(370, 500));
    }

    //wall
    if (snake_location[0].X >= border_x - 1)
    {
        game_over = true;
    }
    else if (snake_location[0].X <= 1)
    {
        game_over = true;
    }
    else if (snake_location[0].Y <= 1)
    {
        game_over = true;
    }
    else if (snake_location[0].Y >= border_y - 1)
    {
        game_over = true;
    }

    //snake
    for (int i = 1; i < snake_long; i++)
    {
        if (snake_location[0] == snake_location[i])
        {
            game_over = true;
        }
    }

    //win
    if (snake_long == ((border_x - 2) * (border_y - 2)))
    {
        game_over = true;
        win = true;
    }

};


//----gameloop----//

while (true)
{

    //reset logic
    Console.Clear();
    snake_head_x = border_x / 2;
    snake_head_y = border_y / 2;
    snake_long = 10;
    snake_direction = "D";
    snake_location[0] =(snake_head_x, snake_head_y);
    game_over = true;
    win = true;

    border_creator();    
    apple();

    while (game_over == false)
    {
        snake();
        snake_colision();
        Thread.Sleep(game_speed);
    } 
    
    Task.Run(() => Console.Beep(600, 500));
    Console.Clear();

    while (game_over == true)
    {
        

        if (win == true) 
        { 


            Console.SetCursorPosition(0, 0);
            Console.Write(@" 
██████╗ ██████╗ ███╗   ██╗ ██████╗ ██████╗  █████╗ ████████╗██╗   ██╗██╗      █████╗ ████████╗██╗ ██████╗ ███╗   ██╗
██╔════╝██╔═══██╗████╗  ██║██╔════╝ ██╔══██╗██╔══██╗╚══██╔══╝██║   ██║██║     ██╔══██╗╚══██╔══╝██║██╔═══██╗████╗  ██║
██║     ██║   ██║██╔██╗ ██║██║  ███╗██████╔╝███████║   ██║   ██║   ██║██║     ███████║   ██║   ██║██║   ██║██╔██╗ ██║
██║     ██║   ██║██║╚██╗██║██║   ██║██╔══██╗██╔══██║   ██║   ██║   ██║██║     ██╔══██║   ██║   ██║██║   ██║██║╚██╗██║
╚██████╗╚██████╔╝██║ ╚████║╚██████╔╝██║  ██║██║  ██║   ██║   ╚██████╔╝███████╗██║  ██║   ██║   ██║╚██████╔╝██║ ╚████║
 ╚═════╝ ╚═════╝ ╚═╝  ╚═══╝ ╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝    ╚═════╝ ╚══════╝╚═╝  ╚═╝   ╚═╝   ╚═╝ ╚═════╝ ╚═╝  ╚═══╝
                                                                                                                     ");


            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(File.ReadAllText("Snake_artwork.txt"));

            Console.SetCursorPosition((int)(border_x * 0.75), (int)(border_y * 0.75));
            Console.WriteLine("For reset press R");
            Console.SetCursorPosition((int)(border_x * 0.75), (int)(border_y * 0.75) + 1);
            Console.Write("For ending the game press E");
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                while (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true);
                }

                if ((key.Key == ConsoleKey.R))
                {
                    win = false;
                    game_over = false;
                }

                if ((key.Key == ConsoleKey.E))
                {
                    Environment.Exit(0);
                }

            }

        }
        else
        {            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, 0);
            Console.Write(@"
██╗   ██╗ ██████╗ ██╗   ██╗    ██╗      ██████╗ ███████╗████████╗
╚██╗ ██╔╝██╔═══██╗██║   ██║    ██║     ██╔═══██╗██╔════╝╚══██╔══╝
 ╚████╔╝ ██║   ██║██║   ██║    ██║     ██║   ██║███████╗   ██║   
  ╚██╔╝  ██║   ██║██║   ██║    ██║     ██║   ██║╚════██║   ██║   
   ██║   ╚██████╔╝╚██████╔╝    ███████╗╚██████╔╝███████║   ██║   
   ╚═╝    ╚═════╝  ╚═════╝     ╚══════╝ ╚═════╝ ╚══════╝   ╚═╝   ");

            Console.SetCursorPosition((int)(border_x * 0.75), (int)(border_y * 0.75));
            Console.WriteLine("For reset press R");
            Console.SetCursorPosition((int)(border_x * 0.75), (int)(border_y * 0.75) + 1);
            Console.Write("For ending the game press E");

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                while (Console.KeyAvailable)
                {
                    key = Console.ReadKey(true);
                }

                if ((key.Key == ConsoleKey.R))
                {
                    win = false;
                    game_over = false;
                }

                if ((key.Key == ConsoleKey.E))
                {
                    Environment.Exit(0);
                }

            }

        }
    }
}