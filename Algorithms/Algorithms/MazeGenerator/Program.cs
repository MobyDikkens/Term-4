using System;

namespace MazeGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Maze maze = new Maze(5,5);
            maze.Show();
        }
    }
}
