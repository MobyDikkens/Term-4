namespace MazeGenerator
{
    class Maze
    {
        #region Fields
        private int xLenth;
        private int yLenth;

        private Cell[,] maze;

        #endregion


        #region Constructor
        public Maze(int xLenth, int yLenth)
        {
            if(xLenth > 0 && yLenth > 0)
            {
                this.xLenth = xLenth;
                this.yLenth = yLenth;
                this.maze = new Cell[xLenth, yLenth];
                GenerateMaze();
            }
            else
            {
                throw new System.ArgumentException("Cannot create maze with zero or negative args");
            }
        }

        #endregion


        #region Methods

        public void Show()
        {
            System.Console.OutputEncoding = new System.Text.UnicodeEncoding();
            for(int i = 0; i < xLenth; i++)
            {
                for(int j = 0; j < yLenth; j++)
                {
                    if(maze[i,j].RightWall)
                    {
                        System.Console.Write(' ');
                    }
                    else
                    {
                        System.Console.Write('|');
                    }
                }
                
                if(i != xLenth - 1)
                {
                    System.Console.WriteLine();
                    for(int j = 0; j < yLenth; j++)
                    {
                        if(maze[i + 1,j].UpWall)
                        {
                            System.Console.Write(' ');
                        }
                        else
                        {
                            System.Console.Write('‾');
                        }
                    }
                }
                System.Console.WriteLine();
            }
        }

        #endregion


        #region Maze Generation Algorithm

        private void GenerateMaze()
        {
            int count = (xLenth - 1) * (yLenth - 1);

            //  current position
            Position current = new Position();
            current.X = 0;
            current.Y = 0;

            Position right = new Position();
            Position up = new Position();

            //  current line
            int currentLine = 0;            

            while(currentLine < xLenth)
            {
                current.X = currentLine;
                current.Y = 0;
                right = current;
                up = current;
                for(int i = 0; i < yLenth; i++)
                {
                    right = current;
                    up = current;
                    right.Y = current.Y + 1;
                    up.X = current.X - 1;

                    if(CanVisit(right))
                    {
                        maze[current.X,current.Y].RightWall = true;
                    }
                    else if(CanVisit(up))
                    {
                        maze[current.X,current.Y].UpWall = true;
                    }

                    current.Y++;
                }


                currentLine++;
            }

        }

        private bool CanVisit(Position position)
        {
            if(position.X >= 0 && position.Y >= 0 && position.X < xLenth && position.Y < yLenth)
            {
                return true;
            }
            return false;
        }


        #endregion


        #region Helpers


        #endregion



        #region Structures

        struct Position
        {
            public int X;
            public int Y;
        }

        struct Cell
        {
            public bool RightWall;
            public bool UpWall;
        }

        #endregion

    }

}