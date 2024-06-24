using System;
using System.Collections.Generic;
using System.Threading;

namespace MatrixRain
{
    class Program
    {
        static Random random = new Random();
        static int width, height;
        static List<Chain> chains;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            width = Console.WindowWidth;
            height = Console.WindowHeight;

            chains = new List<Chain>();

            for (int i = 0; i < width; i++)
            {
                // Додаємо два ланцюжки для кожного стовпця
                chains.Add(new Chain(i, random.Next(0, height)));
                chains.Add(new Chain(i, random.Next(0, height)));
            }

            while (true)
            {
                Draw();
                Thread.Sleep(100);
            }
        }

        static void Draw()
        {
            Console.Clear();

            foreach (var chain in chains)
            {
                chain.Update();
                chain.Render();
            }
        }

        class Chain
        {
            private int column;
            private int length;
            private int position;
            private List<char> characters;
            private static Random random = new Random();

            public Chain(int column, int startPosition)
            {
                this.column = column;
                this.position = startPosition;
                this.length = random.Next(5, height / 2);
                this.characters = new List<char>();

                for (int i = 0; i < length; i++)
                {
                    characters.Add(GetRandomChar());
                }
            }

            public void Update()
            {
                if (position >= height)
                {
                    position = 0;
                    length = random.Next(5, height / 2);
                    characters.Clear();
                    for (int i = 0; i < length; i++)
                    {
                        characters.Add(GetRandomChar());
                    }
                }
                else
                {
                    characters.Insert(0, GetRandomChar());
                    if (characters.Count > length)
                    {
                        characters.RemoveAt(characters.Count - 1);
                    }
                    position++;
                }
            }

            public void Render()
            {
                for (int i = 0; i < characters.Count; i++)
                {
                    int renderPosition = position - i;
                    if (renderPosition < 0 || renderPosition >= height) continue;

                    Console.SetCursorPosition(column, renderPosition);
                    if (i == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (i == 1)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                    }
                    Console.Write(characters[i]);
                }
            }

            private char GetRandomChar()
            {
                return (char)random.Next(33, 126); // Printable ASCII characters
            }
        }
    }
}
