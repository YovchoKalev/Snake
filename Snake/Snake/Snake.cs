using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    struct Position
    {
        public int row;
        public int col;
        public Position(int row, int col)
        {
            this.row = row;
            this.col = col;
        }
    }
    class Snake
    {
        static void Main(string[] args)
        {
            int right = 0;
            int left = 1;
            int down = 2;
            int up = 3;
            Position[] directions = new Position[]
            {
                new Position(0,1), //right
                new Position(0,-1), //left
                new Position(1,0), //down
                new Position(-1,0), //up
            };
            int sleepTime = 100;
            int direction = right; //Which is the direction now
            Random randomNumberGenerator = new Random();

            Console.BufferHeight = Console.WindowHeight;

            Position food = new Position(randomNumberGenerator.Next(0, Console.WindowHeight),
                randomNumberGenerator.Next(0, Console.WindowWidth));
            Console.SetCursorPosition(food.col, food.row);
            Console.Write("@");

            Queue<Position> snakeElements = new Queue<Position>();

            for (int i = 0; i <= 5; i++)
            {
                snakeElements.Enqueue(new Position(0, i));
            }

            foreach (Position position in snakeElements)
            {
                Console.SetCursorPosition(position.col, position.row);
                Console.Write("*");
            }

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    if (userInput.Key == ConsoleKey.LeftArrow)
                    {
                        if(direction !=right) direction = left;
                    }
                    if (userInput.Key == ConsoleKey.RightArrow)
                    {
                        if (direction != left) direction = right;
                    }
                    if (userInput.Key == ConsoleKey.UpArrow)
                    {
                        if (direction != down) direction = up;
                    }
                    if (userInput.Key == ConsoleKey.DownArrow)
                    {
                        if (direction != up) direction = down;
                    }
                }
                Position snakeHeadPosition = snakeElements.Last();
                Position nextDirection = directions[direction];
                Position snakeNewHeadPosition = new Position(snakeHeadPosition.row + nextDirection.row,
                    snakeHeadPosition.col + nextDirection.col);
                if (snakeNewHeadPosition.row < 0 || snakeNewHeadPosition.col < 0 
                    || snakeNewHeadPosition.row >= Console.WindowHeight ||
                    snakeNewHeadPosition.col >= Console.WindowWidth ||snakeElements.Contains(snakeNewHeadPosition))
                {
                    Console.SetCursorPosition(0, 0);
                    Console.WriteLine("Game Over");
                    Console.WriteLine($"Your points are {(snakeElements.Count()-6) * 5}");
                    return;
                }
                snakeElements.Enqueue(snakeNewHeadPosition);
                if (snakeNewHeadPosition.col == food.col && snakeNewHeadPosition.row == food.row)
                {
                    food = new Position(randomNumberGenerator.Next(0, Console.WindowHeight),
                randomNumberGenerator.Next(0, Console.WindowWidth));
                    sleepTime -= 4;
                }
                else
                {
                    snakeElements.Dequeue();
                }

                Console.Clear();

                foreach (Position position in snakeElements)
                {
                    Console.SetCursorPosition(position.col, position.row);
                    Console.Write("*");
                }
                
                Console.SetCursorPosition(food.col, food.row);
                Console.Write("@");

                Thread.Sleep(sleepTime);
            }
        }
    }
}
