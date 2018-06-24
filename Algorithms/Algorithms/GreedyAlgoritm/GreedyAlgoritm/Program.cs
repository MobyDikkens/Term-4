using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreedyAlgoritm
{
    class Program
    {
        static void Main(string[] args)
        {
            for(int i = 0; i < 15; i++)
            {
                Console.WriteLine(Helper.ResolveName(i));
            }
            Map map = new Map();
            Console.ReadKey();
        }
    }
}
