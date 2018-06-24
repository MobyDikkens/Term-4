using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GreedyAlgoritm
{
    class Helper
    {
        public static string ResolveName(int id)
        {
            string file = File.ReadAllText("towns.txt");
            string[] towns = file.Split('\n');

            foreach(var line in towns)
            {
                string[] paramameters = line.Split(',');

                if(paramameters.Length >= 2)
                {
                    try
                    {
                        int lineId = Convert.ToInt32(paramameters[1]);
                        if (lineId == id)
                            return paramameters[0];
                    }
                    catch { }
                }
            }
            throw new ArgumentException("not found");
        }
    }
}
