using System.Collections;
using Thailand.Helpers;

namespace Thailand.Search
{
    public class AStart
    {
        public static RouteInfo FindRoute(int start, int end)
        {
            //list of visited vertexes
            ArrayList closed = new ArrayList();

            //priority queue of not visited vertexes
            PQueue open = new PQueue();

            //resulted route
            RouteInfo route = new RouteInfo();
            route.Towns = new System.Collections.Generic.List<int>();
            route.Towns.Add(start);


            //put all start vertex neighbours to the open collection
            //put the start to the closed
            closed.Add(start);

            for(int i = 0; i < RoutesManager.Length; i++)
            {
                if(RoutesManager.AdjacencyMatrix[start, i] != 0)
                {
                    VertexInfo toPush = new VertexInfo();
                    toPush.Distance = RoutesManager.AdjacencyMatrix[start, i];
                    toPush.Priority = RoutesManager.EvristicMatrix[i, end] + toPush.Distance;
                    toPush.Town = i;

                    //push to the queue
                    open.Push(toPush);
                }

            }


            while(open.Capacity > 0)
            {
                //get the lowest evristic value
                var current = open.Pop();

                //the route has already found
                if(current.Town == end)
                {
                    //add the last node
                    route.Towns.Add(current.Town);
                    break;
                }

                //add town to the closed list
                closed.Add(current.Town);

                //new vertex to add to the open list
                VertexInfo toPush = new VertexInfo();

                

                //discovet the neighbours
                //that is not in closed list
                for (int i = 0; i < RoutesManager.Length; i++)
                {
                    if (RoutesManager.AdjacencyMatrix[current.Town, i] != 0 && !closed.Contains(i))
                    {
                        int tentative = RoutesManager.AdjacencyMatrix[current.Town, i] + current.Distance;

                        if (tentative > RoutesManager.AdjacencyMatrix[start, i])
                        {
                            //add distances to the current node and from current to i
                            toPush.Distance += RoutesManager.AdjacencyMatrix[current.Town, i];
                            toPush.Distance += current.Distance;


                            toPush.Priority = RoutesManager.EvristicMatrix[i, end];
                            toPush.Town = i;


                            //push to the queue
                            open.Push(toPush);

                        }
                    }
                }


                route.Towns.Add(current.Town);


            }



            //calculate length
            for(int i = 0; i < route.Towns.Count - 1; i++)
            {
                route.Length += RoutesManager.AdjacencyMatrix[route.Towns[i], route.Towns[i + 1]];
            }

            return route;

        }




        #region VertexInfo

        private struct VertexInfo
        {
            public int Town;
            public int Priority;
            public int Distance;
        }

        #endregion

        #region PQueue Class

        private class PQueue
        {
            private int _capacity;

            private int _position;

            private VertexInfo[] _queue;

            public int Capacity
            {
                get
                {
                    return _position + 1;
                }
            }

            public PQueue()
            {
                this._capacity = 5;
                this._queue = new VertexInfo[this._capacity];
                this._position = -1;
            }


            public void Push(VertexInfo vertex)
            {
                if(_position + 1 < _capacity)
                {
                    _position++;
                    _queue[_position] = vertex;
                }
                else
                {
                    Reallocate();
                    Push(vertex);
                }
            }

            private void Reallocate()
            {
                _capacity *= 2;
                VertexInfo[] newQueue = new VertexInfo[_capacity];

                for(int i = 0; i <= _position; i++)
                {
                    newQueue[i] = _queue[i];
                }

                _queue = newQueue;
            }

            public VertexInfo Pop()
            {

                int min = 0;

                for(int i = 0; i <= _position; i++)
                {
                    if(_queue[i].Priority < _queue[min].Priority)
                    {
                        min = i;
                    }
                }

                VertexInfo popped = _queue[min];

                for(int i = min; i <= _position - 1; i++)
                {
                    _queue[i] = _queue[i + 1];
                }

                _position--;

                return popped;

            }


        }

        #endregion


    }
}
