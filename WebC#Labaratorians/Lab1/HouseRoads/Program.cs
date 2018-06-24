using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Labaratorian number 1
/// Position in the group list 23
/// Variant 23
/// 
/// 
/// Sheludko Dima IP - 63
/// </summary>
using HouseRoads.Helpers.Input;

namespace HouseRoads
{
    class Program
    {
        static void Main(string[] args)
        {
            IInput input = new InputController();
            int amount = input.GetHousesCount();
            var roads = input.GetRoads(amount);
            HouseRoads.Entities.Map map = new Entities.Map(amount, roads);

            map.GetMinimalPosition();

            Console.ReadKey();
        }
    }
}
