using Thailand.Helpers;

namespace Thailand.Search
{
    public class Greedy
    {

        public static RouteInfo FindRoute(int from, int to)
        {
            //define the route
            RouteInfo route = new RouteInfo();

            route.Towns = new System.Collections.Generic.List<int>();
            

            int totalLength = 0;


            int start = from < to ? from : to;
            int end = from < to ? to : from;

            route.Towns.Add(start);

            int currentPosition = start;
            int minEvristic = RoutesManager.INFINITY;
            int stepTo = RoutesManager.INFINITY;
            int lengthToNext = 0;

            do
            {
                //interates on the adjacency matirx
                for(int i = 0; i < RoutesManager.Length; i++)
                {
                    //the route is exist
                    if(RoutesManager.AdjacencyMatrix[currentPosition, i] != 0)
                    {
                        if(minEvristic > RoutesManager.GetEvristic(i, end))
                        {
                            minEvristic = RoutesManager.GetEvristic(i, end);
                            stepTo = i;
                            lengthToNext = RoutesManager.AdjacencyMatrix[currentPosition, i];
                        }
                    }
                }

                //if we dont have a way
                if(stepTo == RoutesManager.INFINITY)
                {
                    throw new System.EntryPointNotFoundException("Route is not exist");
                }

                //add to the points
                route.Towns.Add(stepTo);

                //move to the next
                currentPosition = stepTo;
                stepTo = 0;
                totalLength += lengthToNext;
            }
            while (currentPosition != end);

            route.Length = totalLength;

            return route;

        }

    }
}
