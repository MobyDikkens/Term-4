using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GreedyAlgoritm
{
    class Map
    {
        private Entity[,] _distance;

        public Map()
        {
            try
            {
                string file = File.ReadAllText("routes.txt");
                string[] routes = file.Split('\n');

                _distance = new Entity[routes.Length, routes.Length];

                foreach(var route in routes)
                {
                    try
                    {
                        string[] elements = route.Split(',');
                        int from = Convert.ToInt32(elements[0]);
                        int to = Convert.ToInt32(elements[1]);
                        int plane = Convert.ToInt32(elements[2]);
                        int car = Convert.ToInt32(elements[3]);

                        _distance[from, to].CarDistance = car;
                        _distance[from, to].FlyDistance = plane;

                        _distance[to, from].CarDistance = car;
                        _distance[to, from].FlyDistance = plane;

                    }
                    catch { }

                }


            }
            catch
            {
                throw;
            }
        }

        struct Entity
        {
            public int CarDistance;
            public int FlyDistance;
        }
    }
}
