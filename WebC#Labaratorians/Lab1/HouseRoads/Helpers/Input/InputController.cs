using HouseRoads.Entities;
using System;
using System.Collections.Generic;

namespace HouseRoads.Helpers.Input
{
    /// <summary>
    /// Class provides a functionallity
    /// to interact with user and get input
    /// </summary>
    class InputController : IInput
    {
        int IInput.GetHousesCount()
        {
            int amount = 0;
            do
            {
                Console.WriteLine("Enter houses amount");
                string input = Console.ReadLine();
                //  Check if the input contains only digits
                if (!IsDigitsOnly(input))
                    continue;
                try
                {
                    amount = Convert.ToInt32(input);
                }
                catch
                {
                    continue;
                }
            }
            while (amount <= 0);

            return amount;
        }

        List<Road> IInput.GetRoads(int houseCount)
        {
            if (houseCount <= 0) throw new ArgumentException("houseCount must be bigger than 0");

            List<Road> roads = new List<Road>();

            Road tmp = new Road();

            int count = 1;

            string input = "";

            int house1 = 0;
            int house2 = 0;

            do
            {
                try
                {
                    Console.WriteLine("Road number {0}", count);

                    //  Get house number 1
                    Console.WriteLine("Enter House 1 number\nFrom 1 To {0}", houseCount);
                    input = Console.ReadLine();

                    //  Check for correct input and initialize temporary value
                    if (!IsDigitsOnly(input))
                        continue;

                    house1 = Convert.ToInt32(input);

                    if (house1 <= 0)
                        continue;

                    //  Get house number 2
                    Console.WriteLine("Enter House 2 number\nFrom 1 To {0}", houseCount);
                    input = Console.ReadLine();

                    //  Checks and initialize temporary value 
                    if (!IsDigitsOnly(input))
                        continue;

                    house2 = Convert.ToInt32(input);

                    if (house2 <= 0)
                        continue;

                    //  Input length
                    Console.WriteLine("Enter the road length(must be above 0)");
                    input = Console.ReadLine();

                    //  Check for correct input and inintialize
                    if (!IsDigitsOnly(input))
                        continue;


                    tmp.House1 = house1;
                    tmp.House2 = house2;
                    tmp.Length = Convert.ToInt32(input);

                    //  Add to the list and increase a road number
                    roads.Add(tmp);
                    count++;
                }
                catch
                {
                    continue;
                }

            }
            while (input.ToLower() != "exit");

            return roads;

        }

        /// <summary>
        /// Checks if the input contains only digits
        /// </summary>
        /// <param name="str">String to check</param>
        /// <returns>True if string contains only digits</returns>
        private bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
    }
}
