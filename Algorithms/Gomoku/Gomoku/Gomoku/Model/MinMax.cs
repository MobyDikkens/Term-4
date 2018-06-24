using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;

namespace Gomoku.Model
{
    public static class MinMax
    {
        private const byte RecursionDeepth = 10;

        private const int Max = 500;
        private const int Min = 200;

       
        public static Coordinates GetCoordinates(byte[,] state, byte currentPlayer)
        {
            Coordinates res = new Coordinates();

            int bestHevristic = -10000;
            int tmpHevristic = 0;

            int tmpHevr2 = 0;

            int alpha = -1000;
            int beta = 1000;


            for(int i = 0; i < 19; i++)
            {
                for(int j = 0; j < 19; j++)
                {
                    //if the current element is empty
                    if(state[i, j] == PlayersInfo.Empty)
                    {
                        //copy matrix
                        byte[,] tmpState = MatrixCopy(state, 19);

                        //make a step
                        tmpState[i, j] = currentPlayer;

                        if (i == 8 && j == 9)
                            ;


                        //find hevristic for the oponent
                        tmpHevristic = CalculateHevristic(tmpState, PlayersInfo.Inverse(currentPlayer), 1);


                        Coordinates tmp = new Coordinates();
                        tmp.X = i;
                        tmp.Y = j;


                        if(Hevristic.OpositeNeighbour(state, tmp, PlayersInfo.Inverse(currentPlayer)))
                        {
                            if(currentPlayer == PlayersInfo.Player)
                            {
                                tmpHevristic -= 50;
                            }
                            else
                            {
                                tmpHevristic += 50;
                            }
                        }



                        //beta = beta > tmpHevristic ? tmpHevristic : beta;
                        //alpha = alpha < tmpHevristic ? tmpHevristic : alpha;

                        if(currentPlayer == PlayersInfo.AI && bestHevristic < tmpHevristic)
                        {
                            bestHevristic = tmpHevristic;
                            res.X = i;
                            res.Y = j;
                        }


                    }
                }
            }



            return res;
        }

        //calculate hevristic recursive
        private static int GetHevristic(byte[,] state, byte currentPlayer, int level)
        {
            if(level == RecursionDeepth)
            {
                int hevr = Hevristic.GetHevristic(state);
                return hevr;
            }
            


            int bestHevristic = 0;
            int tmpHevristic = 0;

            int min = 1000;
            int max = -1000;

            for(int i = 0; i < 19; i++)
            {

                for (int j = 0; j < 19; j++)
                {

                    if (state[i, j] == PlayersInfo.Empty)
                    {
                        byte[,] tmpState = MatrixCopy(state, 19);

                        //make a step
                        tmpState[i, j] = currentPlayer;

                        tmpHevristic = GetHevristic(tmpState, PlayersInfo.Inverse(currentPlayer), level + 1);


                        if (currentPlayer != PlayersInfo.AI)
                        {
                            bestHevristic = bestHevristic < max ? max : bestHevristic;
                        }
                        else
                        {
                            bestHevristic = bestHevristic > min ? min : bestHevristic;
                        }


                    }

                }
            }

            return bestHevristic;
        }


        //alpha beta
        private static int max = -1000;
        private static int min = 1000;

        private static int CalculateHevristic(byte[,] state, byte currentPlayer, int level)
        {
            //if max recursion deepth has reached
            //returns current state
            if(level == RecursionDeepth)
            {
                //reset
                max = -1000;
                min = 1000;

                return Hevristic.GetHevristic(state);
            }

            //best Hevristic result
            int bestHevristic = -1000;

            //current Hevristic value
            int currentHevristic = -1000;

            //hevristic in the current state
            int currentStateHevristic = 0;


            //field after step
            byte[,] nextState = null;


            //iterates each one of empty cells
            for(int i = 0; i < 19; i++)
            {
                for(int j = 0; j < 19; j++)
                {

                    //find empty position
                    //to make an step
                    if(state[i, j] == PlayersInfo.Empty)
                    {
                        //copy the field
                        nextState = MatrixCopy(state, 19);

                        //make an step
                        nextState[i, j] = currentPlayer;

                        //get the current state hevristic
                        currentStateHevristic = Hevristic.GetHevristic(state);

                        if(currentPlayer == PlayersInfo.AI)
                        {
                            min = min < currentStateHevristic ? min : currentStateHevristic;
                        }
                        else
                        {
                            max = max > currentStateHevristic ? max : currentStateHevristic;
                        }

                        if(max >= min)
                        {
                            max = -1000;
                            min = 1000;

                            return currentStateHevristic;
                        }


                        currentHevristic = CalculateHevristic(nextState, PlayersInfo.Inverse(currentPlayer), level + 1);



                        bestHevristic = bestHevristic > currentHevristic ? bestHevristic : currentHevristic;


                    }
                    
                }
            }

            return bestHevristic;


        }


        


        private static byte[,] MatrixCopy(byte[,] source, int length)
        {
            byte[,] res = new byte[length, length];


            for(int i = 0; i < length; i++)
            {
                for(int j = 0; j < length; j++)
                {
                    res[i, j] = source[i, j];
                }
            }

            return res;
        }



    }
}
