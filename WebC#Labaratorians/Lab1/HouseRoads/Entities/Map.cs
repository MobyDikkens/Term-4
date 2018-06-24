using System.Collections.Generic;
using System;


namespace HouseRoads.Entities
{
    /// <summary>
    /// Contains houses and roudes being connected by
    /// Contains logic to find the smallest routes
    /// </summary>
    class Map
    {
        private const int Infinity = 999999;

        private int _housesAmount = 0;

        private List<Road> _roads = null;

        //  out point to meet
        private MeetPoint _meetPoint;

        //private Route[,] _routes = null;

        //  lenght matrix
        private int[,] _routes = null;

        public Map(int housesAmount, List<Road> roads)
        {
            if (housesAmount <= 0 || roads == null || roads.Capacity < 0)
                throw new ArgumentException("incorrect Map constructor paramethers");

            this._housesAmount = housesAmount;
            this._roads = roads;
        }

        //  find minimal position
        public void GetMinimalPosition()
        {
            BuildDistanceMatrix();

            int minimalSumm = Infinity;
            int tmpSumm = 0;
            int minimalHouse = 0;

            //  find all summs of each one house to other
            for(int i = 0; i < _housesAmount; i++)
            {
                for(int j = 0; j < _housesAmount; j++)
                {
                    tmpSumm += _routes[i, j];
                }
                if(minimalSumm > tmpSumm)
                {
                    minimalSumm = tmpSumm;
                    minimalHouse = i;
                }
                tmpSumm = 0;
            }

            //  find the best point to meet
            FindMeetPoint(minimalHouse);

            if (_meetPoint.FromHouse1ToPoint == 0)
            {
                Console.WriteLine("House number {0}", _meetPoint.House1 + 1);
            }
            else
            {
                Console.WriteLine("Meet Point");
                Console.WriteLine("House 1 : {0}", _meetPoint.House1 + 1);
                Console.WriteLine("House 2 : {0}", _meetPoint.House2 + 1);
                Console.WriteLine("From House 1 to Point : {0}", _meetPoint.FromHouse1ToPoint);
            }

        }

        #region Helpers

        //  find distance matrix
        private void BuildDistanceMatrix()
        {
            //  create length matrix
            _routes = new int[_housesAmount, _housesAmount];

            BuildAdjacencyMatrix(ref _routes, _roads.ToArray());

            //  build length matrix
            FloidUorshall(ref _routes);

            //  fill main diag
            for (int i = 0; i < _housesAmount; i++)
            {
                _routes[i, i] = 0;
            }


        }

        //  find meet point
        private void FindMeetPoint(int bestHouse)
        {
            //  create instance of Meet Point
            _meetPoint = new MeetPoint();

            _meetPoint.FromHouse1ToPoint = 0;
            _meetPoint.House1 = bestHouse;
            _meetPoint.House2 = bestHouse;

            //  difference between the biggest and the smalest way
            int delta = 0;

            //  summ of all ways
            int summ = 0;

            //  the biggest and the smallest numbers in the route
            int biggest = 0;
            int smallest = Infinity;

            for(int i = 0; i < _housesAmount; i++)
            {
                summ += _routes[bestHouse, i];

                if (_routes[bestHouse, i] > biggest)
                    biggest = _routes[bestHouse, i];

                if (smallest > _routes[bestHouse, i])
                    smallest = _routes[bestHouse, i];

            }

            //  claculate delta for this route
            delta = biggest - smallest;


            //  iterates each of roads and put point onto the road
            //  find the smallest routes to all houses
            //  find summ and delta
            //  if summ <= newSumm and delta < newDelta
            //  this route is better to meet
            //  feel the _meetPoint and continue interations

            //  roads array
            Road[] roads = _roads.ToArray();

            //  new recalculated route 
            //  from our point to the all houses
            int[] route = new int[_housesAmount];

            //  current point
            MeetPoint point = new MeetPoint();

            for(int i = 0; i < roads.Length; i++)
            {
                Road tmp = new Road();
                tmp = roads[i];

                //  iterates by the road
                for(int j = 1; j < tmp.Length; j++)
                {
                    //  initialize point
                    point.House1 = tmp.House1 - 1;
                    point.House2 = tmp.House2 - 1;
                    point.Length = tmp.Length;
                    point.FromHouse1ToPoint = point.Length - j;

                    //  distances from current point
                    //  to all houses
                    int[] distances = new int[_housesAmount];

                    //  set all to 0
                    for (int x = 0; x < _housesAmount; x++)
                    {
                        distances[x] = 0;
                    }

                    distances[point.House1] = point.FromHouse1ToPoint;
                    distances[point.House2] = point.Length - point.FromHouse1ToPoint;

                    //  initialize distances
                    for(int k = 0; k < distances.Length; k++)
                    {
                        if(k != point.House1 && k != point.House2)
                        {
                            //  distance from houses to k
                            int viaHouse1 = _routes[point.House1, k];
                            int viaHouse2 = _routes[point.House2, k];

                            //  find wich route is better
                            if(viaHouse1 >= viaHouse2)
                            {
                                distances[k] = point.Length - point.FromHouse1ToPoint + viaHouse2;
                            }
                            else
                            {
                                distances[k] = point.FromHouse1ToPoint + viaHouse1;
                            }
                        }
                    }

                    //  calculate summ and delta
                    int newSumm = 0;
                    int newDelta = 0;

                    //  the biggest and the smallest numbers in the route
                    int newBiggest = 0;
                    int newSmallest = Infinity;

                    for (int x = 0; x < _housesAmount; x++)
                    {
                        newSumm += _routes[bestHouse, x];

                        if (distances[x] > newBiggest)
                            newBiggest = distances[x];

                        if (newSmallest > distances[x])
                            newSmallest = distances[x];

                    }

                    //  claculate delta for this route
                    newDelta = newBiggest - newSmallest;

                    //  if the route we have found is better
                    //  swap
                    if(summ >= newSumm && delta > newDelta)
                    {
                        summ = newSumm;
                        delta = newDelta;

                        _meetPoint = point;
                    }


                }
            }


        }

        


        #region Algorithms

        //  build an adjacency matrix
        private void BuildAdjacencyMatrix(ref int[,] routes, Road[] roads)
        {

            //  set all lengths to the Infinity
            for (int i = 0; i < _housesAmount; i++)
            {
                for (int j = 0; j < _housesAmount; j++)
                {
                    _routes[i, j] = Infinity;
                }
            }

            //  initialize length matrix as adjacency matrix
            foreach (var road in _roads)
            {
                var i = road.House1 - 1;
                var j = road.House2 - 1;
                var length = road.Length;

                if (_routes[i, j] > length)
                    _routes[i, j] = length;

                if (_routes[j, i] > length)
                    _routes[j, i] = length;
            }
        }

        private void FloidUorshall(ref int[,] routes)
        {
            int lenght = _housesAmount;
            //  Floid - Uorshell
            for (int k = 0; k < lenght; k++)
            {
                for (int i = 0; i < lenght; i++)
                {
                    for (int j = 0; j < lenght; j++)
                    {
                        routes[i, j] = Math.Min(routes[i, j], routes[i, k] + routes[k, j]);
                    }
                }
            }
        }

        #endregion

        #endregion

        #region MeetPoint Struct

        private struct MeetPoint
        {
            public int House1;
            public int House2;
            public int Length;
            public int FromHouse1ToPoint;
        }

        #endregion

        #region Route Class

        private class Route
        {
            //  All houses numbers that is included in this route
            private List<int> _vertexes = null;

            //  Route Lenght
            public int Length { get; set; }

            //  Gets all vertexes
            public int[] Vertexes
            {
                get
                {
                    int[] vertexes = _vertexes.ToArray();
                    return vertexes;
                }
            }

            //  Add new vertex
            public void AddVertex(int house)
            {
                _vertexes.Add(house);
            }

            //  Default constructor
            public Route()
            {
                this._vertexes = new List<int>();
            }
        }


        #endregion


    }
}
