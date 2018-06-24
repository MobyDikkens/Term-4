using System;
using System.IO;

namespace Thailand.Helpers
{
    /// <summary>
    /// Read all routes from the
    /// resourses
    /// </summary>
    public static class RoutesManager
    {
        private const string DistancePath = "Resourses/distances.csv";

        private const string EvristicPath = "Resourses/evristics.csv";

        public const int INFINITY = 99999999;

        public static int Length { get; private set; }

        public static int[,] AdjacencyMatrix { get; set; }

        public static int[,] EvristicMatrix { get; private set; }

        public static int GetEvristic(int from, int to)
        {
            return EvristicMatrix[from, to];
        }

        static RoutesManager()
        {
            AdjacencyMatrix = GetAdjacencyMatrix();
            EvristicMatrix = GetEvristikMatrix();
        }


        /// <summary>
        /// Get adjacency matrix
        /// </summary>
        /// <returns></returns>
        private static int[,] GetAdjacencyMatrix()
        {
            //get all file content
            string file = File.ReadAllText(DistancePath);

            string[] lines = file.Split('\n');

            int length = GetNumberOfTowns();

            Length = length;

            //matrix
            int[,] adjacency = new int[length, length];

            //info about the route
            int from = 0;
            int to = 0;
            int distance = 0;

            string[] attributes = null;

            //state of converting
            bool state = true;

            for(int i = 0; i < lines.Length; i++)
            {
                //get info about the route
                attributes = lines[i].Split(',');

                //try to read all info about the road
                state = state && Int32.TryParse(attributes[0], out from);
                state = state && Int32.TryParse(attributes[1], out to);
                state = state && Int32.TryParse(attributes[2], out distance);

                //error has occured
                if(!state)
                {
                    throw new FileFormatException();
                }

                //set the values
                adjacency[from - 1, to - 1] = distance;
                adjacency[to - 1, from - 1] = distance;

            }

            return adjacency;
        }

        /// <summary>
        /// Returns the matrix
        /// that contain evristic
        /// distances between the towns
        /// </summary>
        /// <returns></returns>
        private static int[,] GetEvristikMatrix()
        {
            //get all file content
            string file = File.ReadAllText(EvristicPath);

            string[] lines = file.Split('\n');

            int length = GetNumberOfTowns();

            //matrix
            int[,] evristic = new int[length, length];

            //fill evristic infinitives
            for(int i = 0; i < length; i++)
            {
                for(int j = 0; j < length; j++)
                {
                    evristic[i, j] = INFINITY;
                }
                evristic[i, i] = 0;
            }

            //info about the route
            int from = 0;
            int to = 0;
            int evr = 0;

            string[] attributes = null;

            //state of converting
            bool state = true;

            for (int i = 0; i < lines.Length; i++)
            {
                //get info about the route
                attributes = lines[i].Split(',');

                //try to read all info about the road
                state = state && Int32.TryParse(attributes[0], out from);
                state = state && Int32.TryParse(attributes[1], out to);
                state = state && Int32.TryParse(attributes[2], out evr);

                //error has occured
                if (!state)
                {
                    throw new FileFormatException();
                }

                //set the values
                evristic[from - 1, to - 1] = evr;
                evristic[to - 1, from - 1] = evr;

            }

            FloidUorshall(length, ref evristic);

            return evristic;
        }

        /// <summary>
        /// Get general amount of the towns
        /// </summary>
        /// <returns></returns>
        private static int GetNumberOfTowns()
        {
            //get all file content
            string file = File.ReadAllText(DistancePath);

            string[] lines = file.Split('\n');

            //info about the route
            int from = 0;
            int to = 0;
            string[] attributes = null;

            //state of converting
            bool state = true;

            int max = 0;

            for (int i = 0; i < lines.Length; i++)
            {
                //get info about the route
                attributes = lines[i].Split(',');

                //try to read all info about the road
                state = state && Int32.TryParse(attributes[0], out from);
                state = state && Int32.TryParse(attributes[1], out to);

                if(max < from || max < to)
                {
                    max = from > to ? from : to;
                }

            }

            return max;

        }

        private static void FloidUorshall(int length, ref int[,] evristic)
        {
            for (int k = 0; k < length; ++k)
            {
                for (int i = 0; i < length; ++i)
                {
                    for (int j = 0; j < length; ++j)
                    {
                        if (evristic[i, k] < INFINITY && evristic[k, j] < INFINITY)
                        {
                            evristic[i, j] = Math.Min(evristic[i, j], evristic[i, k] + evristic[k, j]);
                        }
                    }
                }
            }
        }

    }
}
