namespace Gomoku.Model
{
    public static class Hevristic
    {

        public const int AIWin = 500;
        public const int PlayerWin = -500;
        public const int Undefined = 200;
        public const int Infinity = -1000;

        //Deepth of the recursion dive
        private const int RecursionDeepth = 8;

        /// <summary>
        /// Calculate the appropriative hevristic
        /// </summary>
        /// <returns></returns>
        public static int Calculate(byte[,] state)
        {
            int hevristic = Undefined;

            int length = 19;

            int max = 0;
            int tmpMax = 0;
            byte maxPlayer = 0;

            for (int i = 0; i < length; i++)
            {
                for(int j = 0; j < length; j++)
                {
                    if (state[i, j] != PlayersInfo.Empty)
                    {
                        //find the sequence of degree

                        //find current coordinates
                        Coordinates coordinates = new Coordinates();
                        coordinates.X = i;
                        coordinates.Y = j;

                        //if we can find sequence of degree
                        //from current coordinates
                        int maxRight = MaxRight(state, coordinates, state[i, j]);

                        int maxDown = MaxDown(state, coordinates, state[i, j]);


                        int maxDiag1 = MaxDiagonal1(state, coordinates, state[i, j]);

                        int maxDiag2 = MaxDiagonal2(state, coordinates, state[i, j]);




                        tmpMax = maxDown > maxRight ? maxDown : maxRight;
                        tmpMax = tmpMax > maxDiag1 ? tmpMax : maxDiag1;
                        tmpMax = tmpMax > maxDiag2 ? tmpMax : maxDiag2;

                        if (max == tmpMax)
                        {
                            if (state[i, j] == PlayersInfo.Player)
                            {
                                max = tmpMax;
                                maxPlayer = state[i, j];
                                //additionals = OpositeNeighbour(state, coordinates, PlayersInfo.Inverse(state[i, j]));
                            }
                        }
                        else if (max < tmpMax)
                        {
                            max = tmpMax;
                            maxPlayer = state[i, j];
                        }
                    }

                }
            }

            if (maxPlayer == PlayersInfo.AI)
            {
                hevristic = 100 * max;
                //if(additionals)
                //{
                //    hevristic += 50;
                //}
            }
            else
            {
                hevristic = -100 * max;
                //if (additionals)
                //{
                //    hevristic -= 50;
                //}
            }


            return hevristic;
        }


        #region Stuff



        private static bool ExploreRight(byte[,] source, int amount, Coordinates from, byte item)
        {
            int lenght = 19;

            for (int i = 1; i < amount; i++)
            {
                if (from.X + i < lenght && source[from.X + i, from.Y] != item)
                {
                    return false;
                }
                else
                {
                    break;
                }
            }

            return true;

        }

        private static int MaxDown(byte[,] source, Coordinates from, byte item)
        {

            int lenght = 19;

            int max = 1;

            for (int i = 1; i < 5; i++)
            {
                if (from.X + i < lenght && source[from.X + i, from.Y] == item)
                {
                    max++;
                }
                else
                {
                    break;
                }
            }

            return max;


        }

        private static int MaxRight(byte[,] source, Coordinates from, byte item)
        {
            int lenght = 19;

            int max = 1;

            for (int i = 1; i < 5; i++)
            {
                if (from.Y + i < lenght && source[from.X, from.Y + i] == item)
                {
                    max++;
                }
                else
                {
                    break;
                }
            }

            return max;



        }

        private static int MaxDiagonal1(byte[,] source, Coordinates from, byte item)
        {
            int max = 1;

            int lenght = 19;

            for (int i = 1; i < 5; i++)
            {
                if (from.Y + i < lenght && from.X + i < lenght
                    && source[from.X + i, from.Y + i] == item)
                {
                    max++;
                }
                else
                {
                    break;
                }
            }


            return max;
        }

        private static int MaxDiagonal2(byte[,] source, Coordinates from, byte item)
        {
            int max = 1;

            int lenght = 19;

            for (int i = 1; i < 5; i++)
            {
                if (from.Y - i >= 0 && from.X + i < lenght
                    && source[from.X + i, from.Y - i] == item)
                {
                    max++;
                }
                else
                {
                    break;
                }
            }


            return max;
        }






        public static int GetHevristic(byte[,] state)
        {
            int hevristic = Infinity;
            int length = 19;

            int downAI = 0;
            int downPlayer = 0;
            int rightAI = 0;
            int rightPlayer = 0;

            byte downItem = PlayersInfo.Empty;
            byte rightItem = PlayersInfo.Empty;

            int tmpMax = Infinity;
            int maxItem = PlayersInfo.Empty;

            for (int i = 0; i < length; i++)
            {
                downAI = DownLine(state, i, PlayersInfo.AI);
                downPlayer = DownLine(state, i, PlayersInfo.Player);

                rightAI = RightLine(state, i, PlayersInfo.AI);
                rightPlayer = RightLine(state, i, PlayersInfo.Player);

                downItem = downAI > downPlayer ? PlayersInfo.AI : PlayersInfo.Player;

                rightItem = rightAI > rightPlayer ? PlayersInfo.AI : PlayersInfo.Player;


                if(downItem == PlayersInfo.AI)
                {
                    if(rightItem == PlayersInfo.AI)
                    {
                        tmpMax = tmpMax > (downAI > rightAI ? downAI : rightAI) ? tmpMax : (downAI > rightAI ? downAI : rightAI);
                        maxItem = PlayersInfo.AI;
                    }
                    else
                    {
                        tmpMax = tmpMax > (downAI > rightPlayer ? downAI : rightPlayer) ? tmpMax : (downAI > rightPlayer ? downAI : rightPlayer);
                        maxItem = downAI > rightPlayer ? PlayersInfo.AI : PlayersInfo.Player;
                    }
                }
                else
                {
                    if (rightItem == PlayersInfo.AI)
                    {
                        tmpMax = tmpMax > (downPlayer > rightAI ? downPlayer : rightAI) ? tmpMax : (downPlayer > rightAI ? downPlayer : rightAI);
                        maxItem = downPlayer > rightAI ? PlayersInfo.Player : PlayersInfo.AI;
                    }
                    else
                    {
                        tmpMax = tmpMax > (downPlayer > rightPlayer ? downPlayer : rightPlayer) ? tmpMax : (downPlayer > rightPlayer ? downPlayer : rightPlayer);
                        maxItem = PlayersInfo.Player;
                    }
                }


            }

            if(maxItem == PlayersInfo.AI)
            {
                hevristic = tmpMax;
            }
            else
            {
                hevristic = (-1) * tmpMax;
            }

            return hevristic;

        }



        ///Normal
        private static int RightLine(byte[,] state, int lineNumber, byte item)
        {
            int length = 19;
            int max = -1000;

            int curr = 0;

            //iteraties each one of row in the line
            for(int j = 0; j < length; j++)
            {
                if(state[lineNumber, j] == item)
                {
                    curr++;
                }
                else
                {
                    max = max < curr ? curr : max;
                    curr = 0;
                }
            }

            max = max < curr ? curr : max;

            return max;

        }

        private static int DownLine(byte[,] state, int rowNumber, byte item)
        {
            int length = 19;
            int max = -1000;

            int curr = 0;

            //iteraties each one of row in the line
            for (int j = 0; j < length; j++)
            {
                if (state[j, rowNumber] == item)
                {
                    curr++;
                }
                else
                {
                    max = max < curr ? curr : max;
                    curr = 0;
                }
            }

            max = max < curr ? curr : max;

            return max;

        }


        /// <summary>
        /// Returns true if there is an neighbour
        /// in the surrounded cells
        /// </summary>
        /// <param name="state"></param>
        /// <param name="from"></param>
        /// <param name="oponent"></param>
        /// <returns></returns>
        public static bool OpositeNeighbour(byte[,] state, Coordinates from, byte oponent)
        {
            int length = 19;

            if (from.X + 1 < length && state[from.X + 1, from.Y] == oponent)
            {
                return true;
            }
            else if (from.X - 1 >= 0 && state[from.X -1, from.Y] == oponent)
            {

            }
            else if (from.Y + 1 < length && state[from.X, from.Y + 1] == oponent)
            {
                return true;
            }
            else if (from.Y - 1 >= 0 && state[from.X, from.Y - 1] == oponent)
            {
                return true;
            }

            return false;

        }



        #endregion





    }
}
